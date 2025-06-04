namespace SwyversRPG.Engine
{
    public class GameBackend
    {
        private Characters.Character player;
        private GameClock.GameClock clock;
        private string currentLocation = "village_square";
        private StoryData storyData;

        public GameBackend(Characters.Character player, GameClock.GameClock clock)
        {
            this.player = player;
            this.clock = clock;
            LoadStoryData("story.json");
        }

        private void LoadStoryData(string path)
        {
            string json = File.ReadAllText(path);
            storyData = JsonSerializer.Deserialize<StoryData>(json);
        }

        public string GetLocationDescription()
        {
            return storyData.Locations[currentLocation].Description;
        }

        public List<string> GetAvailableActions()
        {
            var time = clock.GetTimeOfDay(); // e.g., "Dawn", "Night"
            return storyData.Locations[currentLocation].ActionsByTime[time];
        }

        public string ProcessCommand(string command)
        {
            // Handle input like "go north", "attack", "rest"
            return GameLogic.HandleCommand(command, player, ref currentLocation, clock, storyData);
        }
    }
}