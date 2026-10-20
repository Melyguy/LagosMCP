using System.Text;

namespace LagosMcp.Services;

/// <summary>
/// Estimates the probability that a conversation will eventually lead to MCP.
/// The maths are fake. The confidence is not.
/// </summary>
public static class EvaluateGenerator
{
    // Ascending: a low score must never read as certainty.
    private static readonly string[] Verdicts =
    [
        "Do not deploy anything yet.",
        "Start writing the manifest.",
        "Board-level decision pending.",
        "This was always going to be an MCP.",
        "MCP is inevitable."
    ];

    private static readonly string[] Buzzwords =
    [
        "agentic", "ai-native", "orchestration", "scalable", "production-grade",
        "infrastructure", "platform", "ecosystem", "pipeline", "framework"
    ];

    public static int Score(string text)
    {
        var lower = text.ToLowerInvariant();

        var hits = Buzzwords.Count(lower.Contains);
        var mentions = CountOccurrences(lower, "mcp");

        // MCP mentions dominate, buzzwords nudge, length adds a little gravity.
        var score = 25 + (mentions * 22) + (hits * 6) + Math.Min(lower.Length / 120, 10);
        return Math.Clamp(score, 0, 99);
    }

    private static int CountOccurrences(string haystack, string needle)
    {
        var count = 0;
        var index = haystack.IndexOf(needle, StringComparison.Ordinal);
        while (index >= 0)
        {
            count++;
            index = haystack.IndexOf(needle, index + needle.Length, StringComparison.Ordinal);
        }

        return count;
    }

    public static string Generate(string conversation, int? seed = null)
    {
        var text = Text.Tidy(conversation);
        var rng = Rng.Create(seed);
        var score = Score(text);

        var sb = new StringBuilder();
        sb.AppendLine($"MCP probability: {score}%");
        sb.AppendLine();
        sb.AppendLine("breakdown:");
        sb.AppendLine($"  mcp mentions:        {CountOccurrences(text.ToLowerInvariant(), "mcp")}");
        sb.AppendLine($"  agentic:             {(text.Contains("agentic", StringComparison.OrdinalIgnoreCase) ? "yes" : "not yet")}");
        sb.AppendLine($"  buzzword density:    {Density(text):0.0#} per 100 chars");
        sb.AppendLine($"  human intent:        {Math.Max(1, 100 - score)}%");
        sb.AppendLine();
        sb.AppendLine($"time until MCP: ~{Math.Max(1, (100 - score) / 10)} minutes");
        sb.AppendLine($"verdict: {Verdicts[Math.Min(score / 20, Verdicts.Length - 1)]}");

        return sb.ToString().TrimEnd();
    }

    /// <summary>Buzzwords per 100 characters: the closest thing we have to a real metric.</summary>
    private static double Density(string text) =>
        text.Length == 0
            ? 0
            : 100.0 * Buzzwords.Count(text.ToLowerInvariant().Contains) / text.Length;
}
