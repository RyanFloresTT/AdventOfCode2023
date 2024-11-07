namespace AdventOfCode23.Day6;
using static Utils.FilePathUtils;
public static class Day6
{
    public static async Task<int> PartOne(string filename = "input.txt")
    {
        try
        {
            using StreamReader sr = new($"{GetWorkingDirectory()}Day6/{filename}");
            
            var timeLine = await sr.ReadLineAsync();
            var distanceLine = await sr.ReadLineAsync();
            
            var cleanTimeLine = timeLine[(timeLine.IndexOf(':') + 1)..];
            var times = cleanTimeLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            
            var cleanDistanceLine = distanceLine[(distanceLine.IndexOf(':') + 1)..];
            var distances = cleanDistanceLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            if (distances.Count != times.Count) throw new Exception("Distance/time count mismatch");

            var games = distances.Select((t, i) => new GameStats { RaceTimeInMs = times[i], RecordDistance = t }).ToList();

            var viableGames = new List<List<int>>();

            foreach (var game in games)
            {
                var potenialGames = new List<int>();
                for (var i = 0; i < game.RaceTimeInMs; i++)
                {
                    var underThreshold = (float)game.RecordDistance / game.RaceTimeInMs;
                    if (i < underThreshold) continue;
                    
                    var timeHeld = game.RaceTimeInMs - i;
                    if (!(timeHeld * i > game.RecordDistance)) continue;
                    potenialGames.Add(i);
                }
                viableGames.Add(potenialGames);
            }
            
            return viableGames.Aggregate(1, (acc, list) => acc * list.Count);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message + e.StackTrace);
        }

        return 0;
    }
    
    public static async Task<long> PartTwo(string filename = "input.txt")
    {
        try
        {
            using StreamReader sr = new($"{GetWorkingDirectory()}Day6/{filename}");
            
            var timeLine = await sr.ReadLineAsync();
            var distanceLine = await sr.ReadLineAsync();
            
            var cleanTimeLine = timeLine[(timeLine.IndexOf(':') + 1)..];
            var time = long.Parse(string.Concat(cleanTimeLine.Where(char.IsDigit)));
            
            var cleanDistanceLine = distanceLine[(distanceLine.IndexOf(':') + 1)..];
            var distance = long.Parse(string.Concat(cleanDistanceLine.Where(char.IsDigit)));


            var game = new GameStats { RaceTimeInMs = time, RecordDistance = distance };

            var nonGames = 0;
            var underThreshold = (float)game.RecordDistance / game.RaceTimeInMs;

            var potenialGames = new List<int>();
            for (var i = 0; i < game.RaceTimeInMs; i++)
            {
                if (i < underThreshold) continue;
                    
                var timeHeld = game.RaceTimeInMs - i;
                if (!(timeHeld * i > game.RecordDistance)) continue;
                potenialGames.Add(i);
            }

            return potenialGames.Count;
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message + e.StackTrace);
        }

        return 0;
    }
}

struct GameStats
{
    public long RaceTimeInMs { get; set; }
    public long RecordDistance { get; set; }
}