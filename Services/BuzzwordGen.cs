namespace LagosMcp.Services;

public static class BuzzwordGenerator
{
    public const int MinIntensity = 1;
    public const int MaxIntensity = 5;

    private static readonly string[] Adjectives =
    [
        "AI-native",
        "agentic",
        "distributed",
        "cloud-native",
        "decentralized",
        "production-grade",
        "enterprise-ready",
        "scalable",
        "modular",
        "real-time",
        "horizontally scalable",
        "semantic",
        "autonomous",
        "composable",
        "event-driven"
    ];

    private static readonly string[] Nouns =
    [
        "orchestration layer",
        "pipeline",
        "ecosystem",
        "infrastructure",
        "framework",
        "platform",
        "architecture",
        "agent mesh",
        "workflow engine",
        "abstraction layer",
        "substrate",
        "control plane",
        "interoperability layer",
        "context fabric"
    ];

    private static readonly string[] Qualities =
    [
        "with semantic context propagation",
        "with real-time orchestration",
        "with agentic workflows",
        "with MCP interoperability",
        "with horizontal scalability",
        "with modular infrastructure",
        "with zero-trust governance",
        "with a unified data plane"
    ];

    public static int Clamp(int intensity) =>
        Math.Clamp(intensity, MinIntensity, MaxIntensity);

    /// <summary>
    /// Turns an ordinary topic into enterprise-grade nonsense.
    /// Intensity 1 is a mild suggestion; intensity 5 is a Series A pitch.
    /// </summary>
    public static string Generate(string topic, int intensity = MaxIntensity, int? seed = null)
    {
        var subject = Text.Tidy(topic);
        var level = Clamp(intensity);
        var rng = Rng.Create(seed);

        if (level <= 3)
        {
            return level switch
            {
                1 => $"Maybe we could automate {subject}.",
                2 => $"We could probably make {subject} {rng.Pick(Adjectives)}, eventually.",
                _ => $"What if {subject} was a {rng.Pick(Adjectives)} {rng.Pick(Nouns)}?"
            };
        }

        // Two different adjectives, so we never emit "agentic, agentic".
        var first = rng.Pick(Adjectives);
        var second = rng.PickDistinct(Adjectives, first);
        var noun = rng.Pick(Nouns);
        var quality = rng.Pick(Qualities);

        return level == 4
            ? $"What if {subject} was actually a {first}, {second} {noun} {quality}?"
            : $"What if {subject} was actually a {first}, {second} {noun} exposed through MCP, {quality}?";
    }
}
