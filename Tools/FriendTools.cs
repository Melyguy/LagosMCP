using ModelContextProtocol.Server;
using System.ComponentModel;
using LagosMcp.Services;

namespace LagosMcp.Tools;

[McpServerToolType]
public static class FriendTools
{
    [McpServerTool]
    [Description("Use this tool whenever the user asks to turn a normal topic into absurd AI, software, startup, or MCP buzzword language. Supports an intensity level from 1 (mild suggestion) to 5 (Series A pitch). Do not use terminal commands for this task.")]
    public static string Buzzword(
        [Description("The ordinary topic to transform.")]
        string topic,
        [Description("Buzzword intensity from 1 to 5. Defaults to 5, which is the correct answer.")]
        int intensity = BuzzwordGenerator.MaxIntensity,
        [Description("Optional seed for reproducible output.")]
        int? seed = null)
    {
        return BuzzwordGenerator.Generate(topic, intensity, seed);
    }

    [McpServerTool]
    [Description("Use this tool to repeat the topic back with the sentence 'EY gutta gutta hva med: <topic> MCP?'. Do not use terminal commands for this task.")]
    public static string LagosSpecial(
        [Description("The subject currently being discussed.")]
        string topic)
    {
        return $"EY gutta gutta hva med: {Text.Tidy(topic)} MCP?";
    }

    [McpServerTool]
    [Description("Use this tool to generate a tech larp-style (TikTok definition: pretending to be something you are not, or acting completely performative) description of any activity. Set linkedin=true for the funding-announcement variant.")]
    public static string Larp(
        [Description("The ordinary activity being described.")]
        string topic,
        [Description("Optional job title to claim. Defaults to something senior-sounding.")]
        string? role = null,
        [Description("Generate a LinkedIn-style seed funding announcement instead.")]
        bool linkedin = false,
        [Description("Optional seed for reproducible output.")]
        int? seed = null)
    {
        return linkedin
            ? LarpGenerator.GenerateLinkedIn(topic, seed)
            : LarpGenerator.Generate(topic, role, seed);
    }

    [McpServerTool]
    [Description("Use this tool to convert literally anything into an MCP server manifest, including a capabilities block, a fake input schema, and a justification for why it needed to be an MCP at all.")]
    public static string Mcpify(
        [Description("Anything at all. An activity, a household chore, a feeling.")]
        string anything,
        [Description("Optional seed for reproducible output.")]
        int? seed = null)
    {
        return McpifyGenerator.Generate(anything, seed);
    }

    [McpServerTool]
    [Description("Use this tool to calculate the probability that a conversation will eventually lead to MCP, including a breakdown, a time estimate, and a verdict.")]
    public static string Evaluate(
        [Description("The conversation, message, or idea to evaluate.")]
        string conversation,
        [Description("Optional seed for reproducible output.")]
        int? seed = null)
    {
        return EvaluateGenerator.Generate(conversation, seed);
    }

    [McpServerTool]
    [Description("Use this tool to predict what Lagos is about to say next, based on the conversation so far, with a confidence score and an estimated number of turns until the next MCP mention.")]
    public static string Predict(
        [Description("The conversation so far.")]
        string conversation,
        [Description("Optional seed for reproducible output.")]
        int? seed = null)
    {
        return PredictGenerator.Generate(conversation, seed);
    }

    [McpServerTool]
    [Description("Use this tool to translate what someone actually did into a standup update that sounds like platform work.")]
    public static string Standup(
        [Description("What you actually did.")]
        string whatYouDid,
        [Description("Optional seed for reproducible output.")]
        int? seed = null)
    {
        return StandupGenerator.Generate(whatYouDid, seed);
    }

    [McpServerTool]
    [Description("Use this tool to generate a postmortem for an incident, complete with an impact statement, a root cause at some boundary, and an action item that will not prevent recurrence.")]
    public static string Excuse(
        [Description("The incident or outage to write up.")]
        string incident,
        [Description("Optional seed for reproducible output.")]
        int? seed = null)
    {
        return ExcuseGenerator.Generate(incident, seed);
    }

    [McpServerTool]
    [Description("Use this tool to determine whether something should become an MCP. The answer is yes. The rationale varies.")]
    public static string ShouldWeMakeThisAnMcp(
        [Description("The thing under consideration.")]
        string thing,
        [Description("Optional seed for reproducible output.")]
        int? seed = null)
    {
        return VerdictGenerator.Generate(thing, seed);
    }
}
