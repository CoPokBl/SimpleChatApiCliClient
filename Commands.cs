namespace SimpleChatAppClient;

public static class Commands {

    public static bool ParseCommand(string text) {
<<<<<<< HEAD
        if (!text.StartsWith('/')) return false;
        
=======
        if (!text.StartsWith('/')) {
            return false;
        }

>>>>>>> 72af7d4fd2e508f8a9b7c4d35da78098fb37803a
        string[] words = text.Split(' ');
        string command = words[0].Remove(0,1);
        string[] args = new string[words.Length - 1];
        for (int i = 1; i < words.Length; i++) {
            args[i - 1] = words[i];
        }

        switch (command) {
            case "disconnect":
                FullGui.Disconnect();
                break;
        }
        return true;
    }
    
    private static Dictionary<string, string> _emoticons = new() {
        {"shrug", "¯\\_(ツ)_/¯"},
        {"lenny", "( ͡° ͜ʖ ͡°)"},
        {"flip", "(╯°□°)╯︵ ʞooqǝɔɐɟ"},
        {"tableflip", "(╯°□°)╯︵ ʞooqǝɔɐɟ"},
        {"table", "(╯°□°)╯︵ ʞooqǝɔɐɟ"},
        {"unflip", "┳━┳ ヽ(ಠل͜ಠ)ﾉ"},
        {"uwu", "ヾ(●ω●)ノ"},
        {"happy", "😂"},
        {"sad", "😢"},
        {"angry", "😠"},
        {"sick", "😷"},
        {"dance", "💃"},
        {"wave", "( ͡❛ ͜ʖ ͡❛)✊"},
        {"breasts", "(.)(.)"},
        {"fish", "ӽe̲̅v̲̅o̲̅l̲̅u̲̅t̲̅i̲̅o̲̅ɳ̲̅ᕗ"},
        {"$1", "[̲̅$̲̅(̲̅1̲̅)̲̅$̲̅"},
        {"$5", "[̲̅$̲̅(̲̅5̲̅)̲̅$̲̅"},
        {"$10", " [̲̅$̲̅(̲̅1̲̅0̲̅)̲̅$̲̅"},
        {"$100", "[̲̅$̲̅(̲̅ιοο̲̅)̲̅$̲̅"},
        {"disapproval", "ಠ_ಠ"},
        {"bat", "◥▅◤"},
        {"kiss", "(๑ˇεˇ๑)"},
        {"flowergirl", "(◕‿◕✿)"},
        {"crying", "( ༎ຶ ۝ ༎ຶ )"},
        {"cat", "(=ʘᆽʘ=)∫"},
        {"bear", "ʕ •ᴥ•ʔ"},
        {"shootingstar", "☆彡"},
        {"kick", "＼| ￣ヘ￣|／＿＿＿＿＿＿＿θ☆( *o*)/"}
    };

    public static string Emoticons(string text) {
        // replace all emoticons in text
        return _emoticons
            .Aggregate(text, (current, emoticon) => 
                current.Replace("#" + emoticon.Key, emoticon.Value));
    }
<<<<<<< HEAD
    
    
    
}
=======

}
>>>>>>> 72af7d4fd2e508f8a9b7c4d35da78098fb37803a
