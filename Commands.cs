namespace SimpleChatAppClient; 

public class Commands {

    public static bool ParseCommand(string text) {
        if (!text.StartsWith('/')) {
            return false;
        }
        
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

    public static string Emoticons(string text) {
        return text switch {
            "#shrug" => "¯\\_(ツ)_/¯",
            "#lenny" => "( ͡° ͜ʖ ͡°)",
            "#flip" => "(╯°□°)╯︵ ʞooqǝɔɐɟ",
            "#tableflip" => "(╯°□°)╯︵ ʞooqǝɔɐɟ",
            "#table" => "(╯°□°)╯︵ ʞooqǝɔɐɟ",
            "#unflip" => "┳━┳ ヽ(ಠل͜ಠ)ﾉ",
            "#uwu" => "ヾ(●ω●)ノ",
            "#happy" => "😂",
            "#sad" => "😢",
            "#angry" => "😠",
            "#sick" => "😷",
            "#dance" => "💃",
            "#wave" => "( ͡❛ ͜ʖ ͡❛)✊",
            "#breasts" => "(.)(.)",
            "#fish" => "ӽe̲̅v̲̅o̲̅l̲̅u̲̅t̲̅i̲̅o̲̅ɳ̲̅ᕗ",
            "#$1" => "[̲̅$̲̅(̲̅1̲̅)̲̅$̲̅]",
            "#$5" => "[̲̅$̲̅(̲̅5̲̅)̲̅$̲̅]",
            "#$10" => " [̲̅$̲̅(̲̅1̲̅0̲̅)̲̅$̲̅]",
            "#$100" => "[̲̅$̲̅(̲̅ιοο̲̅)̲̅$̲̅]",
            "#disapproval" => "ಠ_ಠ",
            "#bat" => "◥▅◤",
            "#kiss" => "(๑ˇεˇ๑)",
            "#flowergirl" => "(◕‿◕✿)",
            "#crying" => "( ༎ຶ ۝ ༎ຶ )",
            "#cat" => "(=ʘᆽʘ=)∫",
            "#bear" => "ʕ •ᴥ•ʔ",
            "#shootingstar" => "☆彡",
            "#kick" => "＼| ￣ヘ￣|／＿＿＿＿＿＿＿θ☆( *o*)/",
            _ => text
        };
    }
    
}