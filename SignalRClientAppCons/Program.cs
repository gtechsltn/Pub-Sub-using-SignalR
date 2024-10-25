// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRClientAppCons.Data;
using SignalRClientAppCons.Models;
using SignalRClientAppCons.Repositorys;

Console.WriteLine("Hello, World!");
// Set up configuration to read from appsettings.json
var projectDirectory = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
var configuration = new ConfigurationBuilder()
    .SetBasePath(projectDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Initialize DapperContext and MessageRepository with configuration
var dapperContext = new DapperContext(configuration);

var repository = new MessageRepository(dapperContext);

// Set up SignalR connection
var hubUrl = configuration.GetSection("HubUrl");
var connection = new HubConnectionBuilder()
    .WithUrl(hubUrl.Value) // Update URL as needed
    .Build();

// On receiving a message, save it to the database
connection.On<string, string>("ReceiveMessage", async (user, message) =>
{
    var newMessage = new Message
    {
        Content = $"{user}: {message}",
        Timestamp = DateTime.Now
    };

    // Save the message to the database
    await repository.SaveMessageAsync(newMessage);
    Console.WriteLine($"Received and saved message: {newMessage.Content} at {newMessage.Timestamp}");
});

try
{
    // Start the SignalR connection
    await connection.StartAsync();
    Console.WriteLine("Connected to SignalR Hub.");
}
catch (Exception ex)
{
    Console.WriteLine("Connected to SignalR Hub."+ex.Message);
}

// Keep the console app running
Console.WriteLine("Listening for messages. Press Ctrl+C to exit.");
await Task.Delay(-1);