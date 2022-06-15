using System.Text;
using SimpleChatAppLibrary;

namespace SimpleChatAppClient; 

public static class FullGui {
    
    private static string _currentlyTypedText = "";
    private static IEnumerable<SimpleChatAppMessage>? _lastMessages;
    private static SimpleChatAppLibrary.SimpleChatAppClient _client;

    public static void Start(SimpleChatAppLibrary.SimpleChatAppClient client) {
        _client = client;
        new Thread(ReaderThread).Start();
        
        while (true) {
            string msg = PasswordTyping();
            _currentlyTypedText = "";
            try {
                _client.SendMessage(msg);
            }
            catch (Exception e) {
                Console.WriteLine("Error sending message: " + e.Message);
            }
        }
        
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
        // Get messages from server
        IEnumerable<SimpleChatAppMessage> messages = client.GetMessages(15);
        // If there are no new messages, return
        if (_lastMessages != null && MessagesEqual(_lastMessages, messages)) {
            return;
        }
        _lastMessages = messages;
        Console.Clear();
        foreach (SimpleChatAppMessage msg in messages) {
            Console.WriteLine($"{DateTime.FromBinary(msg.createdAt).ToLocalTime()}\n" +
                              $"{msg.creatorName}: {msg.text}\n");
        }
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
    
    
}