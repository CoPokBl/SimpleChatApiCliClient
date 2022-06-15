
using SimpleChatAppClient;

Console.WriteLine("Starting...");

// Ask for channel
Console.Write("Channel: ");
string channel = Console.ReadLine();

// Ask for username
Console.Write("Username: ");
string username = Console.ReadLine();

SimpleChatAppLibrary.SimpleChatAppClient client = new("http://chat.serble.net", username, channel);

if (!client.TestConnection()) {
    Console.WriteLine("Connection failed.");
    return 1;
}
Console.WriteLine("Connection successful.");

FullGui.Start(client);

return 0;