using LagosMcp.Services;
using LagosMcp.Tools;
using Xunit;

namespace LagosMcp.Tests;

public class LagosSpecialTests
{
    [Fact]
    public void UsesTheCanonicalPhrase()
    {
        var result = FriendTools.LagosSpecial("rust");
        Assert.Equal("EY gutta gutta hva med: rust MCP?", result);
    }

    [Fact]
    public void TidiesTrailingPunctuation()
    {
        Assert.Equal("EY gutta gutta hva med: rust MCP?", FriendTools.LagosSpecial("rust."));
    }

    [Fact]
    public void RejectsBlankTopic()
    {
        Assert.ThrowsAny<ArgumentException>(() => FriendTools.LagosSpecial(""));
    }
}

public class LarpTests
{
    [Fact]
    public void DefaultLarpMentionsArchitecting()
    {
        var result = LarpGenerator.Generate("making scrambled eggs", seed: 5);
        Assert.Contains("architecting", result);
        Assert.Contains("making scrambled eggs", result);
    }

    [Fact]
    public void ExplicitRoleIsUsed()
    {
        var result = LarpGenerator.Generate("brewing coffee", role: "distinguished engineer", seed: 5);
        Assert.Contains("distinguished engineer", result);
    }

    [Fact]
    public void LinkedInVariantAnnouncesFunding()
    {
        var result = LarpGenerator.GenerateLinkedIn("walking the dog", seed: 9);
        Assert.Contains("seed funding", result);
        Assert.Contains("#mcp", result);
    }

    [Fact]
    public void LinkedInVariantIsSeeded()
    {
        var a = LarpGenerator.GenerateLinkedIn("walking the dog", seed: 3);
        var b = LarpGenerator.GenerateLinkedIn("walking the dog", seed: 3);
        Assert.Equal(a, b);
    }

    [Fact]
    public void ToolRoutesToLinkedInVariant()
    {
        var result = FriendTools.Larp("walking the dog", linkedin: true, seed: 1);
        Assert.Contains("seed funding", result);
    }

    [Fact]
    public void ToolDefaultsToStandardLarp()
    {
        var result = FriendTools.Larp("walking the dog", seed: 1);
        Assert.Contains("architecting", result);
    }
}

public class McpifyTests
{
    [Fact]
    public void ProducesAValidLookingManifest()
    {
        var result = McpifyGenerator.Generate("feeding the cat", seed: 4);
        Assert.Contains("server:", result);
        Assert.Contains("transport: stdio", result);
        Assert.Contains("inputSchema:", result);
        Assert.Contains("justification:", result);
        Assert.Contains("verdict: ship it.", result);
    }

    [Fact]
    public void ToolNameIsDerivedFromSubject()
    {
        var result = McpifyGenerator.Generate("feeding the cat", seed: 4);
        Assert.Contains("feeding_the_cat", result);
    }

    [Fact]
    public void EveryManifestHasExactlyOneToolName()
    {
        var result = McpifyGenerator.Generate("doing laundry", seed: 11);
        Assert.Contains("name: doing_laundry", result);
    }

    [Fact]
    public void IsSeeded()
    {
        Assert.Equal(
            McpifyGenerator.Generate("doing laundry", seed: 11),
            McpifyGenerator.Generate("doing laundry", seed: 11));
    }

    [Fact]
    public void SurvivesAwkwardInput()
    {
        var result = McpifyGenerator.Generate("!!!", seed: 2);
        Assert.Contains("server: thing-mcp", result);
    }
}
