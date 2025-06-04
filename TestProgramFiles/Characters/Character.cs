namespace SwyversRPG.Characters
{
    public class Character
    {
        public string Name { get; set; }
        public string Special { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int HitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public bool Literate { get; set; }
        public Stats Stats { get; set; }
        public Money Money { get; set; }
        public List<string> Equipment { get; set; }
        public List<string> Spells { get; set; }
        public List<string> Fences { get; set; }
        public List<string> CriminalRecord { get; set; }
        public Conditions Conditions { get; set; }
    }
}