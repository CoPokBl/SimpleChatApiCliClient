using System.Text;
using SimpleChatAppLibrary;

namespace SimpleChatAppClient;

public static class FullGui {

    private static string _currentlyTypedText = "";
    private static IEnumerable<SimpleChatAppMessage>? _lastMessages;
    private static IEnumerable<string>? _lastUsers;
    private static SimpleChatAppLibrary.SimpleChatAppClient _client;
    private static Thread _renderingThread;
    private static bool _run = true;

    public static void Start(SimpleChatAppLibrary.SimpleChatAppClient client) {
        _client = client;
        _renderingThread = new Thread(ReaderThread);
        _renderingThread.Start();

        while (true) {
            string msg = PasswordTyping();
            msg = Commands.Emoticons(msg);
            _currentlyTypedText = "";
            try {
                _client.SendMessage(msg);
            }
            catch (Exception e) {
                Console.WriteLine("Error sending message: " + e.Message);
            }
        }

    }

    public static void Disconnect() {
        // stop the rendering thread
        _renderingThread.Interrupt();
        _run = false;
    }

    private static void ReaderThread() {
        Console.Title = "Simple Chat App - Message Reader";
        while (true) {
            try {
                Cycle(_client);
            }
            catch (Exception e) {
                Console.Clear();
                Console.WriteLine("Cycle Failed: " + e.Message);
                Console.WriteLine("trying again in 10 seconds...");
                Thread.Sleep(TimeSpan.FromSeconds(9));
            }
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
        }
    }

    private static void Cycle(SimpleChatAppLibrary.SimpleChatAppClient client) {

        if (!_run) {
            throw new ExitException();
        }
        
        // Get messages from server
        IEnumerable<SimpleChatAppMessage> messages = client.GetMessages(15, 0, 
            bool.Parse(Prefs.GetString("online_status", "true")));
        IEnumerable<string> users = client.GetOnlineUsers();
        // If there are no new messages, return
        if (_lastMessages != null && MessagesEqual(_lastMessages, messages) && _lastUsers != null && MessagesEqual(_lastUsers, users)) {
            return;
        }
        _lastMessages = messages;
        _lastUsers = users;
        Console.Clear();
        foreach (SimpleChatAppMessage msg in messages) {
            Console.WriteLine($"{DateTime.FromBinary(msg.createdAt).ToLocalTime()}\n" +
                              $"{msg.creatorName}: {msg.text}\n");
        }

        IEnumerable<string> onlineUsers = client.GetOnlineUsers();
        // turn online users into a string
        string onlineUsersString = onlineUsers.Aggregate("", (current, user) => current + (user + ", "));
        if (onlineUsers.Count() != 0) {
            onlineUsersString = onlineUsersString[..^2];
        }
        Console.WriteLine("Users Online: " + onlineUsersString);
        Console.Write("Message: " + _currentlyTypedText);
    }

    private static string PasswordTyping() {
        StringBuilder pass = new ();

        while (true) {
            ConsoleKeyInfo ki = Console.ReadKey(true);

            if (ki.Key == ConsoleKey.Enter) break;
            if (ki.Key == ConsoleKey.Backspace) {
                if (pass.Length < 1) continue;

                pass.Remove(pass.Length - 1, 1);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(" ");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                _currentlyTypedText = pass.ToString();
                continue;
            }

            Console.Write(ki.KeyChar);

            pass.Append(ki.KeyChar);
            _currentlyTypedText = pass.ToString();
        }
        Console.Write("\n");

        return pass.ToString();
    }

    private static bool MessagesEqual(IEnumerable<SimpleChatAppMessage> a, IEnumerable<SimpleChatAppMessage> b) {
        if (a.Count() != b.Count()) return false;
        for (int i = 0; i < a.Count(); i++) {
            if (!a.ElementAt(i).text.Equals(b.ElementAt(i).text)) return false;
        }
        return true;
    }
    
    // function to compare two lists of strings
    private static bool MessagesEqual(IEnumerable<string> a, IEnumerable<string> b) {
        if (a.Count() != b.Count()) return false;
        for (int i = 0; i < a.Count(); i++) {
            if (!a.ElementAt(i).Equals(b.ElementAt(i))) return false;
        }
        return true;
    }


}

public class ExitException : Exception { }
