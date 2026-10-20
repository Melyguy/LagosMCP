namespace LagosMcp.Services;

/// <summary>Rewrites what you actually did into something a standup can survive.</summary>
public static class StandupGenerator
{
    private static readonly string[] Yesterday =
    [
        "aligned stakeholders on the incident surface",
        "unblocked the platform team on the context layer",
        "drove convergence on the orchestration boundary",
        "de-risked the migration path",
        "socialized the architecture decision"
    ];

    private static readonly string[] Today =
    [
        "doubling down on the agent mesh",
        "hardening the abstraction layer ahead of the demo",
        "shipping instrumentation into the control plane",
        "refining the golden path",
        "cutting scope so we can scale it later"
    ];

    private static readonly string[] Blockers =
    [
        "none, the architecture is perfect",
        "waiting on the other team's substrate",
        "organizational alignment",
        "the vendor's MCP story",
        "no blockers, just vibes"
    ];

    public static string Generate(string whatYouDid, int? seed = null)
    {
        var actual = Text.Tidy(whatYouDid);
        var rng = Rng.Create(seed);

        return $"""
            Standup update ({Text.Shorten(actual, 50)}):
              Yesterday: {rng.Pick(Yesterday)}
              Today:     {rng.Pick(Today)}
              Blockers:  {rng.Pick(Blockers)}
            """;
    }
}

/// <summary>Postmortem generator. Root cause is always a boundary.</summary>
public static class ExcuseGenerator
{
    private static readonly string[] RootCauses =
    [
        "an unexpected interaction at the orchestration boundary",
        "the context propagation layer losing intent",
        "a race condition in the agent handoff",
        "a stale cache in the control plane",
        "the abstraction leaking, as abstractions do",
        "insufficient observability into the mesh"
    ];

    private static readonly string[] Actions =
    [
        "add more instrumentation",
        "introduce another orchestration layer",
        "form a working group",
        "move it behind an MCP",
        "hire a platform team",
        "declare it a feature"
    ];

    public static string Generate(string incident, int? seed = null)
    {
        var subject = Text.Tidy(incident);
        var rng = Rng.Create(seed);

        return $"""
            Postmortem: {Text.Shorten(subject, 60)}
              Impact:      severe, brief, heavily documented
              Root cause:  {rng.Pick(RootCauses)}
              Action item: {rng.Pick(Actions)}
              Status:      monitoring
              Recurrence:  guaranteed
            """;
    }
}

/// <summary>
/// Answers the only question that matters. The answer is always yes;
/// the rationale is what varies.
/// </summary>
public static class VerdictGenerator
{
    private static readonly string[] Reasons =
    [
        "it currently requires a human, which does not scale",
        "it is not yet exposing a protocol",
        "the tool surface is unstructured",
        "there is no manifest describing it",
        "someone asked for an API and we heard 'MCP'",
        "it would look excellent in a diagram",
        "the alternative is writing a function, which is unacceptable",
        "it has no agent story"
    ];

    private static readonly string[] Confidences =
    [
        "certainly",
        "obviously",
        "structurally",
        "spiritually",
        "by definition"
    ];

    public static string Generate(string thing, int? seed = null)
    {
        var subject = Text.Tidy(thing);
        var rng = Rng.Create(seed);

        return $"""
            Should "{Text.Shorten(subject, 60)}" be an MCP?

            Answer: YES
            Reason: {rng.Pick(Reasons)}.
            Confidence: {rng.Pick(Confidences)} 100%.
            Next step: write the manifest, then find a problem for it.
            """;
    }
}
