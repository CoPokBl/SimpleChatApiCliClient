namespace SimpleChatAppClient; 

public static class Settings {

    private static readonly Dictionary<char, (string, ValueType)> _settings = new() {
        { '1', ("username", ValueType.String) },
        { '2', ("ip", ValueType.String) },
        { '3', ("online_status", ValueType.Bool) }
    };

    public static void FirstTimeSetup() {
        Console.WriteLine("Welcome to SimpleChatAppClient!");
        Console.Write("Please enter your username: ");
        string username = Console.ReadLine();

        const string defaultServerIp = "https://chat.zaneharrison.com";
        Console.Write($"Enter server ip (default: {defaultServerIp}): ");
        string serverIp = Console.ReadLine();
        if (serverIp == "") {
            serverIp = defaultServerIp;
        }
        
        Console.Write("Would you like to appear online (y/n)? ");
        string onlineStatus = Console.ReadKey().KeyChar.ToString();
        Console.WriteLine();

        onlineStatus = onlineStatus switch {
            "y" => "true",
            "n" => "false",
            _ => "true"
        };
        
        // Set all prefs
        Prefs.SetString("username", username);
        Prefs.SetString("ip", serverIp);
        Prefs.SetString("online_status", onlineStatus);
        Prefs.Save();
        
        Console.WriteLine("Saved prefs. Press enter to continue.\n");
        Console.ReadKey(true);
    }

    public static void Run() {
        while (true) {
            Console.Clear();

            Console.Write("Settings:\n" +
                          $"1. Username ({Prefs.GetString("username")})\n" +
                          $"2. Server IP ({Prefs.GetString("ip")})\n" +
                          $"3. Display Online Status ({Prefs.GetString("online_status")})\n" +
                          $"Enter what setting you want to change or 'e' to exit: ");
        
            char input = Console.ReadKey(false).KeyChar;
            Console.Write("\n");

            if (input == 'e') {
                break;
            }

            if (_settings.ContainsKey(input)) {
                ChangeSetting(input);
                continue;
            }
            
            Console.WriteLine("\nInvalid input. Press enter to continue.");
            Console.ReadKey(false);
        }
    }
    
    private static void ChangeSetting(char input) {

        bool validInput = false;
        string newValue = "";
        while (!validInput) {
            Console.Write($"Enter new value ({_settings[input].Item2.ToString()}): ");
            newValue = Console.ReadLine();

            if (newValue == "/q") {
                return;
            }

            switch (_settings[input].Item2) {
                case ValueType.String:
                    validInput = true;
                    continue;
                case ValueType.Bool: {
                    if (newValue == "true" || newValue == "false") {
                        validInput = true;
                    }
                    continue;
                }
                case ValueType.Int when int.TryParse(newValue, out _):
                    validInput = true;
                    continue;
                default:
                    Console.WriteLine("\nInvalid Input! Enter /q to exit back to settings.");
                    break;
            }
        }
        
        
        
        Prefs.SetString(_settings[input].Item1, newValue);
        Prefs.Save();
        Console.WriteLine("Setting changed. Press enter to continue.");
        Console.ReadKey(false);
    }
    
    private enum ValueType {
        String,
        Int,
        Bool
    }

}