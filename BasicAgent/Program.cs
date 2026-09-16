using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI.Chat;
using System.ClientModel;


//Define foundry Variable
var endpoint = Environment.GetEnvironmentVariable("Azure_OpenAI_EndPoint") ?? throw new InvalidOperationException("Endpoint is not set.");
var model = Environment.GetEnvironmentVariable("Azure_OpenAI_DeploymentName") ?? "gpt-5-mini";

IChatClient chatClient = new AzureOpenAIClient(
     new Uri(endpoint),
    new AzureCliCredential()) //  this from cli
    .GetChatClient(model)
    .AsIChatClient();

AIAgent agent = chatClient.AsAIAgent(
    name: "NetworkSupport",
    instructions: "You are tier 1 support agent. Your answer must be concise, professional and limited strict."
    );
Console.WriteLine($"Agent '{agent.Name}' is online!");

string userIssue = "I am getting DNS resolution error when connecting to corporate vpn from coffee shop";

//non streaming response 
AgentResponse agentResponse = await agent.RunAsync(userIssue);
Console.WriteLine(agentResponse.Text); //The entire response is availabe at once

//streaming response
await foreach (AgentResponseUpdate update in agent.RunStreamingAsync(userIssue))
{
    Console.Write(update.Text);
}

    
