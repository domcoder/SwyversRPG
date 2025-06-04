using System.Text.Json;

public class Character
{
    // Basic info
    public string Name { get; set; }
    public required string Special { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    // Hitpoints
    public int HitPoints { get; set; }
    public int MaxHitPoints { get; set; }

    // Attributes
    public bool Literate { get; set; }
    public int Strength { get; set; }
    public int MaxStrength { get; set; }
    public int Dexterity { get; set; }
    public int MaxDexterity { get; set; }
    public int Constitution { get; set; }
    public int MaxConstitution { get; set; }

    // Skills
    public struct Skills
    {
        public string Skill;
        public int Value;
    }

    public Skills Skill { get; set; }

    // Money
    public struct Money
    {
        public int Pounds;
        public int Shillings;
        public int Pence;
    }
    public Money Purse { get; set; }

    // Equipment by location
    public Dictionary<string, List<string>> Equipment { get; set; } = new()
    {
        { "belt", new List<string>() },
        { "backpack", new List<string>() },
        { "backpackexternal", new List<string>() },
        { "armour", new List<string>() },
        { "clothes", new List<string>() },
        { "sack", new List<string>() }
    };

    // Spells
    public List<string> Spells { get; set; } = new();

    // Fences
    public struct Fence
    {
        public string Name;
        public string Type; // "standard" or "occult"
    }
    public List<Fence> Fences { get; set; } = new();

    // Criminal records
    public struct CriminalRecord
    {
        public string Crime;
        public string Penalty;
        public bool Evaded;
    }
    public List<CriminalRecord> CriminalRecords { get; set; } = new();

    // Conditions
    public List<string> Prosthetics { get; set; } = new();
    public List<string> Afflictions { get; set; } = new();
    public List<string> Diseases { get; set; } = new();

    // Constructor
    public Character(string name)
    {
        Name = name;
        Level = 1;
        Experience = 0;
        Strength = MaxStrength = 5;
        Dexterity = MaxDexterity = 5;
        Constitution = MaxConstitution = HitPoints = MaxHitPoints = 5;
        Literate = false;
    }

    // Healing method
    public void Heal(int amount)
    {
        HitPoints = Math.Min(HitPoints + amount, MaxHitPoints);
    }

    // Damage method
    public void TakeDamage(int amount)
    {
        HitPoints -= amount;
        if (HitPoints < 0) HitPoints = 0;
    }

    // Check if dead
    public bool IsDead()
    {
        return HitPoints <= 0;
    }

    // Gain XP and level up using the custom table
    public void GainExperience(int amount)
    {
        Experience += amount;
        while (Experience >= ExperienceToNextLevel(Level))
        {
            Level++;
            OnLevelUp();
        }
    }

    // Experience required for next level
    private int ExperienceToNextLevel(int level)
    {
        if (level == 0) return 300;
        else
        {
            int previous = ExperienceToNextLevel(level - 1);
            return previous * 2;
        }
    }

    // Level-up bonus handler
    private void OnLevelUp()
    {
        // When levelling, the Swyvers gains TWO of the following - 
        // 1. +1 to Fighting Skill (to a maximum of +6 total).
        // 2. +1 HP (to a maximum of 20 total).
        // 3. +1 in an existing or new skill — may be chosen twice.
        Console.WriteLine($"{Name} achieved level {Level}.");
    }

    // Save to JSON
    public void SaveToFile(string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };
        File.WriteAllText(filePath, JsonSerializer.Serialize(this, options));
    }

    // Load from JSON
    public static Character LoadFromFile(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var options = new JsonSerializerOptions
        {
            IncludeFields = true
        };
        var character = JsonSerializer.Deserialize<Character>(json, options);
    
        if (character == null)
            throw new InvalidDataException("Failed to deserialize Character from file: " + filePath);

        return character;
    }
}