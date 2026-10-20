namespace LagosMcp.Services;

/// <summary>
/// Predicts what Lagos is about to say. Accuracy is not the product;
/// confidence is.
/// </summary>
public static class PredictGenerator
{
    // Whole templates, so the grammar survives every combination.
    private static readonly string[] Templates =
    [
        "bro, what if we turn {0} into an MCP?",
        "okay hear me out: {0}, but as a service?",
        "so basically we abstract {0} behind an agent mesh?",
        "but what if instead of {0} we had an orchestration layer for it?",
        "yo, imagine {0} with MCP interoperability?",
        "what if {0} was a distributed pipeline?",
        "hear me out, {0} but agentic?"
    ];

    public static string Generate(string conversation, int? seed = null)
    {
        var context = Text.Tidy(conversation);
        var rng = Rng.Create(seed);
        var subject = Text.Shorten(context, 60);

        var prediction = string.Format(rng.Pick(Templates), subject);

        var confidence = Math.Clamp(60 + context.Length % 38, 60, 98);

        // Higher confidence means fewer turns until he says it.
        var turns = Math.Max(1, (100 - confidence) / 10);

        return $"""
            prediction:  "{prediction}"
            confidence:  {confidence}%
            next mention of MCP in: {turns} turn(s)
            recommended response: "yes"
            """;
    }
}
