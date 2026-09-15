using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using OpenAI.Chat;
using System.ClientModel;


//Define foundry Variable
var endpoint = Environment.GetEnvironmentVariable("Azure_OpenAI_EndPoint") ?? throw new InvalidOperationException("Endpoint is not set.");
var model = Environment.GetEnvironmentVariable("Azure_OpenAI_DeploymentName") ?? "gpt-5-mini";

//Create Agent
AIAgent agent = new AzureOpenAIClient(
    new Uri(endpoint),
    new AzureCliCredential()) //  this from cli
    //new ApiKeyCredential(key) // this is foundry api key
    .GetChatClient(model)
    .AsAIAgent(instructions: "You are a friendly assistant. Keep your answers brief");

//Invoke Agent
Console.WriteLine(agent.RunAsync("What is largest city in India."));
