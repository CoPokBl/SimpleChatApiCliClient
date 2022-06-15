using System.Text.Json;

namespace SimpleChatAppClient; 

public class Prefs {
    private static Dictionary<string, string>? _prefs;

    public static string? GetString(string key) {
        if (_prefs == null) {
            // Load prefs from disk
            Load();
        }

        return !_prefs.ContainsKey(key) ? null : _prefs[key];
    }

    public static string GetString(string key, string defaultValue) => GetString(key) ?? defaultValue;
    
    public static void SetString(string key, string value) {
        if (_prefs == null) {
            // Load prefs from disk
            Load();
        }

        _prefs[key] = value;
    }
    
    // Load prefs function
    private static void Load() {
        if (!File.Exists("prefs.json")) {
            _prefs = new Dictionary<string, string>();
            return;
        }
        
        // Load prefs from disk
        _prefs = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("prefs.json"));
        
    }
    
    // Save prefs function
    public static void Save() {
        if (_prefs == null) {
            // Nothing to save
            return;
        }

        // Save prefs to disk
        File.WriteAllText("prefs.json", JsonSerializer.Serialize(_prefs));
    }

}