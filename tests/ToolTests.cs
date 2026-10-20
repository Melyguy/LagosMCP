using LagosMcp.Services;
using LagosMcp.Tools;
using Xunit;

namespace LagosMcp.Tests;

public class EvaluateTests
{
    [Fact]
    public void ScoreIsClampedToPercentageRange()
    {
        foreach (var text in new[]
                 {
                     "hello",
                     new string('x', 5000),
                     "agentic mcp mcp mcp orchestration scalable production-grade infrastructure platform ecosystem pipeline framework"
                 })
        {
            var score = EvaluateGenerator.Score(text);
            Assert.InRange(score, 0, 99);
        }
    }

    [Fact]
    public void MoreMcpMentionsRaiseTheScore()
    {
        var low = EvaluateGenerator.Score("we should probably ship something");
        var high = EvaluateGenerator.Score("mcp mcp mcp mcp mcp");
        Assert.True(high > low);
    }

    [Fact]
    public void BuzzwordsRaiseTheScore()
    {
        var low = EvaluateGenerator.Score("let's eat lunch");
        var high = EvaluateGenerator.Score("let's align on the agentic orchestration platform");
        Assert.True(high > low);
    }

    [Fact]
    public void OutputContainsTheScoreAndAVerdict()
    {
        var result = EvaluateGenerator.Generate("we should build an agentic platform", seed: 1);
        Assert.Contains("MCP probability:", result);
        Assert.Contains("verdict:", result);
        Assert.Contains("time until MCP:", result);
    }

    [Fact]
    public void VerdictIsStableForASeed()
    {
        Assert.Equal(
            EvaluateGenerator.Generate("discussing the roadmap", seed: 12),
            EvaluateGenerator.Generate("discussing the roadmap", seed: 12));
    }
}

public class PredictTests
{
    [Fact]
    public void PredictionIncludesConfidenceAndTurnEstimate()
    {
        var result = PredictGenerator.Generate("we're talking about lunch", seed: 2);
        Assert.Contains("prediction:", result);
        Assert.Contains("confidence:", result);
        Assert.Contains("next mention of MCP in:", result);
        Assert.Contains("recommended response: \"yes\"", result);
    }

    [Fact]
    public void ConfidenceStaysInRange()
    {
        foreach (var context in new[] { "a", new string('y', 400) })
        {
            var result = PredictGenerator.Generate(context, seed: 1);
            var line = result.Split('\n').First(l => l.Contains("confidence:"));
            var value = int.Parse(line.Split(':')[1].Trim().TrimEnd('%'));
            Assert.InRange(value, 60, 98);
        }
    }

    [Fact]
    public void IsSeeded()
    {
        Assert.Equal(
            PredictGenerator.Generate("same input", seed: 8),
            PredictGenerator.Generate("same input", seed: 8));
    }
}

public class SuiteTests
{
    [Fact]
    public void StandupProducesThreeSections()
    {
        var result = StandupGenerator.Generate("fixed a typo", seed: 1);
        Assert.Contains("Yesterday:", result);
        Assert.Contains("Today:", result);
        Assert.Contains("Blockers:", result);
    }

    [Fact]
    public void ExcuseProducesAPostmortem()
    {
        var result = ExcuseGenerator.Generate("the site was down", seed: 1);
        Assert.Contains("Postmortem:", result);
        Assert.Contains("Root cause:", result);
        Assert.Contains("Action item:", result);
    }

    [Fact]
    public void VerdictAlwaysSaysYes()
    {
        for (var seed = 0; seed < 20; seed++)
        {
            var result = VerdictGenerator.Generate("going for a walk", seed);
            Assert.Contains("Answer: YES", result);
        }
    }

    [Fact]
    public void EachGeneratorIsSeeded()
    {
        Assert.Equal(StandupGenerator.Generate("x", 1), StandupGenerator.Generate("x", 1));
        Assert.Equal(ExcuseGenerator.Generate("x", 1), ExcuseGenerator.Generate("x", 1));
        Assert.Equal(VerdictGenerator.Generate("x", 1), VerdictGenerator.Generate("x", 1));
    }
}

public class ToolSurfaceTests
{
    [Fact]
    public void EveryToolReturnsSomethingForOrdinaryInput()
    {
        var outputs = new[]
        {
            FriendTools.Buzzword("lunch", seed: 1),
            FriendTools.LagosSpecial("lunch"),
            FriendTools.Larp("lunch", seed: 1),
            FriendTools.Mcpify("lunch", seed: 1),
            FriendTools.Evaluate("lunch", seed: 1),
            FriendTools.Predict("lunch", seed: 1),
            FriendTools.Standup("lunch", seed: 1),
            FriendTools.Excuse("lunch", seed: 1),
            FriendTools.ShouldWeMakeThisAnMcp("lunch", seed: 1)
        };

        Assert.All(outputs, output => Assert.False(string.IsNullOrWhiteSpace(output)));
    }

    [Fact]
    public void ToolsAreDiscoverableAsMcpServerTools()
    {
        var tools = typeof(FriendTools).Assembly
            .GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(ModelContextProtocol.Server.McpServerToolTypeAttribute), false).Length > 0)
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(typeof(ModelContextProtocol.Server.McpServerToolAttribute), false).Length > 0)
            .ToList();

        Assert.Equal(9, tools.Count);
    }
}
