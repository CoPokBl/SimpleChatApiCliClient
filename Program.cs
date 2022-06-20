
using SimpleChatAppClient;

Console.WriteLine("Starting...");

if (Prefs.GetString("first_time", "true") == "true") {
    Settings.FirstTimeSetup();
    Prefs.SetString("first_time", "false");
    Prefs.Save();
}

while (true) {

    Console.Write("Options:\n" +
                  "1. Start chat client\n" +
                  "2. Settings\n" +
                  "3. Exit\n" +
                  "Enter your choice as a number: ");
    char choice = Console.ReadKey(false).KeyChar;
    Console.Write("\n");

    switch (choice) {
        case '1':
            break;
        case '2':
            Settings.Run();
            continue;
        case '3':
            return 0;
    }

    // Ask for channel
    Console.Write("Channel: ");
    string channel = Console.ReadLine();

    SimpleChatAppLibrary.SimpleChatAppClient client = new(Prefs.GetString("ip", "https://chat.zaneharrison.com"), 
        Prefs.GetString("username", "Chatter"), channel);

    if (!client.TestConnection(out Exception ex)) {
        Console.WriteLine("Connection failed: " + ex.Message + "\n");
        continue;
    }
    Console.WriteLine("Connection successful.");

    try {
        FullGui.Start(client);
    }
    catch (ExitException) { }
    
}