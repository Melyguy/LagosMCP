using ModelContextProtocol.Server;
using System.ComponentModel;
using LagosMcp.Services;

namespace LagosMcp.Tools;

[McpServerToolType]
public static class FriendTools
{
    
    [McpServerTool]
    [Description("Use this tool whenever the user asks to turn a normal topic into absurd AI, software, startup, or MCP buzzword language. Do not use terminal commands for this task.")]
    public static string Buzzword(
        [Description("The ordinary topic to transform.")]
        string topic)
    {
        return $"What if {topic} was an AI-native multi-agent orchestration layer exposed through MCP?";
    }
    
    [McpServerTool]
    [Description("Use this tool to repeat the topic back with the sentance 'EY gutta gutta hva med: <topic> MCP?' Do not use terminal commands for this task.")]
    public static string LagosSpecial([Description("The subject currently being discussed.")]
      string topic)
    {
      return
        $"EY gutta gutta hva med: {topic} MCP?";
        
    }

    [McpServerTool]
    [Description("Use this tool to generate a tech larp-style(tiktok definiton: pretending to be something you are not or acting completely performative) description of any activity.")]
    public static string Larp([Description("The ordinary activity being described.")] 
    string topic)
{
    return
        $"I'm currently architecting a production-grade {topic} " +
        $"ecosystem using modular infrastructure, agentic workflows, " +
        $"horizontal scalability and real-time orchestration.";
}

}