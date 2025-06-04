using System;
using System.IO;
using SwyversRPG.Characters;
using SwyversRPG.GameClock;
using SwyversRPG.Engine;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Fantasy Text Adventure";

        // Load or create player character
        Character player = LoadOrCreateCharacter();

        // Initialize the game clock
        GameClock clock = new GameClock();
        clock.Start();

        // Initialize backend with player and clock
        GameBackend backend = new GameBackend(player, clock);

        // Start frontend with backend
        GameFrontend frontend = new GameFrontend(backend);
        frontend.Run();

        // Save character on exit (in case loop breaks in future updates)
        SaveCharacter(player);
        clock.Stop();
    }

    static Character LoadOrCreateCharacter()
    {
        const string characterFile = "player.json";
        if (File.Exists(characterFile))
        {
            string json = File.ReadAllText(characterFile);
            return System.Text.Json.JsonSerializer.Deserialize<Character>(json);
        }

        // For now, create a placeholder character
        var newChar = new Character
        {
            Name = "Player",
            Race = "Human",
            Class = "Adventurer",
            Level = 1,
            HitPoints = 10,
            MaxHitPoints = 10,
            Stats = new Stats { Strength = 10, Dexterity = 10, Constitution = 10, Intelligence = 10, Wisdom = 10, Charisma = 10 },
            Money = new Money { Gold = 10, Silver = 5, Copper = 20 },
            Equipment = new List<string> { "Wooden Sword" },
            Spells = new List<string>(),
            Status = new Status()
        };

        SaveCharacter(newChar);
        return newChar;
    }

    static void SaveCharacter(Character character)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(character, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("player.json", json);
    }
}