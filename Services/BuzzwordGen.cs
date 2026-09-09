namespace LagosMcp.Services;

public static class BuzzwordGenerator
{
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
        "real-time"
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
        "abstraction layer"
    ];

    public static string Generate(string topic)
    {
        string adjective1 = Adjectives[Random.Shared.Next(Adjectives.Length)];
        string adjective2 = Adjectives[Random.Shared.Next(Adjectives.Length)];

        string noun = Nouns[Random.Shared.Next(Nouns.Length)];

        return
            $"What if {topic} was actually a {adjective1}, " +
            $"{adjective2} {noun} exposed through MCP?";
    }
}