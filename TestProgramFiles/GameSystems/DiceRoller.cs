using System;
using System.Text.RegularExpressions;

namespace MyGame.GameSystems
{
    // Input in the form of XdY (e.g. 3d6)
    // Returns total, individual rolls
    public static class DiceRoller
    {
        private static readonly Random rng = new();
        public static (int Total, List<int> Rolls) Roll(string notation)
        {
            var match = Regex.Match(notation.ToLower(), @"^(\d+)d(\d+)$");
            if (!match.Success)
                throw new ArgumentException("Invalid dice notation. Use format XdY, e.g., 2d6.");

            int count = int.Parse(match.Groups[1].Value);
            int sides = int.Parse(match.Groups[2].Value);

            if (count <= 0 || sides <= 1)
                throw new ArgumentException("Dice count must be > 0 and sides > 1.");

            List<int> rolls = new();
            for (int i = 0; i < count; i++)
                rolls.Add(rng.Next(1, sides + 1));

            int total = rolls.Sum();
            return (total, rolls);
        }
    }
}