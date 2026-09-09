using ModelContextProtocol.Server;
using System.ComponentModel;
using LagosMcp.Services;

namespace LagosMcp.Tools;

[McpServerToolType]
public static class FriendTools
{
    
    [McpServerTool]
    [Description(
        "Turns a normal subject into an unnecessarily technical buzzword-filled concept."
    )]
    public static string Buzzword(
        [Description("The subject currently being discussed.")]
        string topic,
        [Description("Buzzword intensity from 1 to 5.")]
        int intensity = 3)
    {
        return BuzzwordGenerator.Generate(topic);
    }
        public static string LagosSpecial([Description("The subject currently being discussed.")]
      string topic)
    {
      return
        $"EY gutta gutta hva med: {topic} MCP?";
        
    }

    public static string Larp([Description("The ordinary activity being described.")] 
    string topic)
{
    return
        $"I'm currently architecting a production-grade {topic} " +
        $"ecosystem using modular infrastructure, agentic workflows, " +
        $"horizontal scalability and real-time orchestration.";
}

}