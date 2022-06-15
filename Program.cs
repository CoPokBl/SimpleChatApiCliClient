
using SimpleChatAppClient;

Console.WriteLine("Starting...");

// Ask for server ip
string defaultServerIp = Prefs.GetString("ip", "https://chat.zaneharrison.com");
Console.Write($"Enter server ip (default: {defaultServerIp}): ");
string serverIp = Console.ReadLine();
if (serverIp == "") {
    serverIp = defaultServerIp;
} else {
    Prefs.SetString("ip", serverIp);
    Prefs.Save();
}

// Ask for channel
Console.Write("Channel: ");
string channel = Console.ReadLine();

// Ask for username
Console.Write("Username: ");
string username = Console.ReadLine();

SimpleChatAppLibrary.SimpleChatAppClient client = new(serverIp, username, channel);

if (!client.TestConnection(out Exception ex)) {
    Console.WriteLine("Connection failed: " + ex.Message);
    return 1;
}
Console.WriteLine("Connection successful.");

FullGui.Start(client);

return 0;