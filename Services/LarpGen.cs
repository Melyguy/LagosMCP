namespace LagosMcp.Services;

/// <summary>
/// Tech-larp: describing an ordinary activity the way it would appear on a
/// conference stage or a Series A deck.
/// </summary>
public static class LarpGenerator
{
    private static readonly string[] Roles =
    [
        "platform engineer",
        "staff infrastructure architect",
        "principal agentic strategist",
        "head of developer experience",
        "founding AI engineer"
    ];

    private static readonly string[] Closers =
    [
        "We're basically redefining the category.",
        "This is a paradigm shift, not a feature.",
        "Honestly, the hard part was the org chart.",
        "We're not building a product, we're building a movement.",
        "The tech is easy. The alignment is the moat."
    ];

    public static string Generate(string activity, string? role = null, int? seed = null)
    {
        var subject = Text.Tidy(activity);
        var rng = Rng.Create(seed);
        var title = string.IsNullOrWhiteSpace(role)
            ? rng.Pick(Roles)
            : Text.Tidy(role);

        // "a senior ... architect" / "an ... engineer": pick the article from the title.
        var article = "aeiou".Contains(char.ToLowerInvariant(title[0])) ? "an" : "a";

        return $"As {article} {title}, I'm currently architecting a production-grade " +
               $"{subject} ecosystem using modular infrastructure, agentic workflows, " +
               $"horizontal scalability and real-time orchestration. {rng.Pick(Closers)}";
    }

    public static string GenerateLinkedIn(string activity, int? seed = null)
    {
        var subject = Text.Tidy(activity);
        var rng = Rng.Create(seed);
        var funding = rng.Next(2, 40) * 1_000_000;
        var valuation = funding * rng.Next(4, 12);
        var headcount = rng.Next(3, 60);

        return $"""
            Excited to announce that we've raised ${funding:N0} in seed funding
            to reinvent {subject} as an agentic, MCP-native orchestration layer.

            We started with a simple observation: {subject} is broken.
            We're fixing it with {headcount} people, 47 microservices and
            an unwavering commitment to not shipping.

            Valuation: ${valuation:N0}. Revenue: TBD.
            The future of {subject} is interoperable.

            #blessed #agentic #mcp #buildinpublic #stealth
            """;
    }
}
