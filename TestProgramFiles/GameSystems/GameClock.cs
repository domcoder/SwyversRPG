namespace SwyversRPG.GameClock
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class GameClock
    {
        private static readonly string TimeFile = "time.txt"; // Text file for storing time data

        private static readonly string[] TimesOfDay = { "Dawn", "Noon", "Dusk", "Night" };

        private static readonly string[] Months = {
        "Dawnreach", "Suncrest", "Greenswell",
        "Highbloom", "Goldsun", "Duskhollow",
        "Emberdeep", "Frostwane", "Snowfall",
        "Stormcall", "Nightveil", "Darkturn"
    };

        private static readonly string[] Seasons = {
        "Earlyear", "Cropseed", "Harvesttide", "Whitestorm"
    };

        // Default options for if time.txt doesn't exist
        private int timeOfDayIndex = 0;
        private int currentDay = 5;
        private int currentMonth = 8;
        private int currentYear = 1666;

        private bool isRunning = true;

        public void Start()
        {
            LoadState();
            Task.Run(() => RunClock());
        }

        private void RunClock()
        {
            while (isRunning)
            {
                PrintGameTime();
                SaveState();

                for (int i = 0; i < 15; i++) // Sleep in 1-min intervals to check for shutdown
                {
                    if (!isRunning) return;
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                }

                if (!isRunning) return;

                timeOfDayIndex = (timeOfDayIndex + 1) % 4;
                if (timeOfDayIndex == 0)
                {
                    AdvanceDay();
                }
            }
        }

        private void AdvanceDay()
        {
            currentDay++;
            if (currentDay > 30)
            {
                currentDay = 1;
                currentMonth++;
                if (currentMonth >= 12)
                {
                    currentMonth = 0;
                    currentYear++;
                }
            }
        }

        private string GetDaySuffix(int day)
        {
            if (day >= 11 && day <= 13)
                return "th";

            return (day % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }

        private void PrintGameTime()
        {
            string timeOfDay = TimesOfDay[timeOfDayIndex];
            string dayWithSuffix = $"{currentDay}{GetDaySuffix(currentDay)}";
            string monthName = Months[currentMonth];
            string seasonName = Seasons[currentMonth / 3];

            Console.WriteLine($"{timeOfDay} | {dayWithSuffix} {monthName}, {currentYear}");
        }

        private void LoadState()
        {
            if (!File.Exists(TimeFile)) return;

            string[] lines = File.ReadAllLines(TimeFile); // Order: timeOfDayIndex, currentDay, currentMonth, currentYear
            if (lines.Length >= 4 &&
                int.TryParse(lines[0], out int savedTimeIndex) &&
                int.TryParse(lines[1], out int savedDay) &&
                int.TryParse(lines[2], out int savedMonth) &&
                int.TryParse(lines[3], out int savedYear))
            {
                timeOfDayIndex = Math.Clamp(savedTimeIndex, 0, 3);
                currentDay = Math.Clamp(savedDay, 1, 30);
                currentMonth = Math.Clamp(savedMonth, 0, 11);
                currentYear = Math.Max(1, savedYear);
            }
        }

        private void SaveState()
        {
            File.WriteAllLines(TimeFile, new[]
            {
            timeOfDayIndex.ToString(),
            currentDay.ToString(),
            currentMonth.ToString(),
            currentYear.ToString()
        });
        }

        public void Stop()
        {
            isRunning = false;
            SaveState();
        }
    }
}