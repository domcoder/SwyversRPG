    public class Equipment
    {
        // Weapons go on belt in a scabbard, armour + clothing need their own
        public Dictionary<string, string> Belt { get; set; } = new();
        public Dictionary<string, string> Backpack { get; set; } = new();
        public Dictionary<string, string> BackpackExternal { get; set; } = new();
        public Dictionary<string, string> Sack { get; set; } = new();
    }