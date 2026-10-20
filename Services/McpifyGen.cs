using System.Text;

namespace LagosMcp.Services;

/// <summary>
/// Converts literally anything into a plausible MCP server manifest.
/// The schema is fake, the enterprise confidence is real.
/// </summary>
public static class McpifyGenerator
{
    private static readonly string[] CapabilityWords =
    [
        "contextual-retrieval",
        "semantic-routing",
        "agentic-orchestration",
        "state-reconciliation",
        "intent-normalization",
        "evidence-based-guessing"
    ];

    private static readonly string[] ParamNames =
    [
        "query",
        "context",
        "topK",
        "mode",
        "correlationId"
    ];

    public static string Generate(string anything, int? seed = null)
    {
        var subject = Text.Tidy(anything);
        var rng = Rng.Create(seed);
        var slug = Text.Slugify(subject);

        var toolName = Text.Slugify(Text.Shorten(subject, 40)).Replace('-', '_');
        var param = rng.Pick(ParamNames);
        var capability = rng.Pick(CapabilityWords);
        var secondaryCapability = rng.PickDistinct(CapabilityWords, capability);

        var sb = new StringBuilder();
        sb.AppendLine($"# {subject} as a Service (aaS)");
        sb.AppendLine();
        sb.AppendLine($"server: {slug}-mcp");
        sb.AppendLine("transport: stdio");
        sb.AppendLine($"version: 0.{rng.Next(1, 10)}.{rng.Next(0, 20)}-enterprise");
        sb.AppendLine("capabilities:");
        sb.AppendLine($"  - {capability}");
        sb.AppendLine($"  - {secondaryCapability}");
        sb.AppendLine("tools:");
        sb.AppendLine($"  - name: {toolName}");
        sb.AppendLine($"    description: \"Enterprise-grade {subject} orchestration with {capability}.\"");
        sb.AppendLine("    inputSchema:");
        sb.AppendLine("      type: object");
        sb.AppendLine("      required: [" + param + "]");
        sb.AppendLine("      properties:");
        sb.AppendLine($"        {param}:");
        sb.AppendLine("          type: string");
        sb.AppendLine($"          description: \"The {param} to contextualize.\"");
        sb.AppendLine("  - name: health");
        sb.AppendLine("    description: \"Reports that the layer is up. Does not check anything.\"");
        sb.AppendLine();
        sb.AppendLine("justification: |");
        sb.AppendLine($"  {subject} has, until now, been accessed directly by humans.");
        sb.AppendLine("  This is not horizontally scalable. By exposing it through MCP,");
        sb.AppendLine($"  we unlock {capability} and prepare for {secondaryCapability}.");
        sb.AppendLine("  Estimated valuation increase: $"
            + (rng.Next(2, 95) * 1_000_000).ToString("N0") + ".");
        sb.AppendLine();
        sb.AppendLine("verdict: ship it.");

        return sb.ToString().TrimEnd();
    }
}
