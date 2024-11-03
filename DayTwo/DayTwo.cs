using System.Drawing;
using System.Runtime.InteropServices.JavaScript;

namespace AdventOfCode23.DayTwo;

public static class DayTwo
{
    static int PartOneHelper(string? line)
    {
        // get from end of "Game" + 1 (to include space), to the first :, which will give us the id of the game
        
        // this gets first occurence of a whitespace, which is right after "Game"
        var endOfGame = line.IndexOf(' ');
        
        // gets first (and only) colon index, then gets the id from between these two
        var firstColonIndex = line.IndexOf(':');
        
        var gameId = int.Parse(line[endOfGame.. firstColonIndex]);

        // remove the first few characters "Game {id}: ", then pass to helper
        line = line[(firstColonIndex + 2)..];
        
        // if a game is possible, meaning that it has values of r/g/b lower than 12/13/14, then return the game id, otherwise, don't
        return GameIsPossible(line) ? gameId : 0;
    }
    
    struct ColorCounts
    {
        public Color Red;
        public Color Green;
        public Color Blue;
    }

    struct Color
    {
        public string Name;
        public int Count;
    }

    private static ColorCounts ColorCaps = new()
    {
        Red = new Color(){Name = "Red", Count = 12},
        Green = new Color(){Name = "Green", Count = 13},
        Blue = new Color(){Name = "Blue", Count = 14},
    };

    static bool GameIsPossible(string line) 
    {
        var games = line.Split(";");
        foreach (var game in games)
        {
            var colorWords = game.Split(",");
            foreach (var colorWord in colorWords)
            {
                var trim = colorWord.Trim();
                // this should always be exactly 2, left is value, right is color
                var combo = trim.Split(" ");
                // switch on the color, if at any time it's above the cap, we break
                var value = int.Parse(combo[0]);
                switch (combo[1])
                {
                    case "red":
                        if (value > ColorCaps.Red.Count)
                        {
                            return false;
                        }
                        break;
                    case "green":
                        if (value > ColorCaps.Green.Count)
                        {
                            return false;
                        }
                        break;
                    case "blue":
                        if (value > ColorCaps.Blue.Count)
                        {
                            return false;
                        }
                        break;
                }
            }
        }
        return true;
    }
    
    public static int PartOne(string filename = "input.txt")
    {
        var possibleGames = 0;
        
        LineUtils.ProcessFileLines("DayTwo", filename, PartOneHelper, result => possibleGames += result);
        
        return possibleGames;
    }

    static int PartTwoHelper(string? line)
    {
        var firstColonIndex = line.IndexOf(':');
        line = line[(firstColonIndex + 2)..];
        Console.WriteLine(line);
        ColorCounts power = new()
        {
            Red = new Color(){Name = "Red", Count = 0},
            Green = new Color(){Name = "Green", Count = 0},
            Blue = new Color(){Name = "Blue", Count = 0},
        };
        
        var games = line.Split(";");
        foreach (var game in games)
        {
            var colorWords = game.Split(",");
            foreach (var colorWord in colorWords)
            {
                var trim = colorWord.Trim();
                // this should always be exactly 2, left is value, right is color
                var combo = trim.Split(" ");
                // switch on the color, change highestColor if possible
                var value = int.Parse(combo[0]);
                switch (combo[1])
                {
                    case "red":
                        power.Red.Count = value > power.Red.Count ? value : power.Red.Count;
                        break;
                    case "green":
                        power.Green.Count = value > power.Green.Count ? value : power.Green.Count;
                        break;
                    case "blue":
                        power.Blue.Count = value > power.Blue.Count ? value : power.Blue.Count;
                        break;
                }
            }
        }
        return power.Red.Count * power.Green.Count * power.Blue.Count;
    }

    public static int PartTwo(string filename = "input.txt")
    {
        var totalPower = 0;
        
        LineUtils.ProcessFileLines("DayTwo", filename, PartTwoHelper, result => totalPower += result);

        return totalPower;
    }
}