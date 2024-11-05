namespace AdventOfCode23.Day2;
using Utils;
public static class Day2
{
    static Task<int> PartOneHelper(string? line)
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
        var res = GameIsPossible(line) ? gameId : 0;
        return Task.FromResult(res);
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
    
    public static async Task<int> PartOne(string filename = "input.txt")
    {
        var possibleGames = 0;
        
        await LineUtils.ProcessFileLinesAsync("Day2", filename, PartOneHelper, async result => await Task.Run(() => possibleGames += result));
        
        return possibleGames;
    }

    static Task<int> PartTwoHelper(string? line)
    {
        var firstColonIndex = line.IndexOf(':');
        line = line[(firstColonIndex + 2)..];
        ColorCounts powerCounts = new()
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
                        powerCounts.Red.Count = value > powerCounts.Red.Count ? value : powerCounts.Red.Count;
                        break;
                    case "green":
                        powerCounts.Green.Count = value > powerCounts.Green.Count ? value : powerCounts.Green.Count;
                        break;
                    case "blue":
                        powerCounts.Blue.Count = value > powerCounts.Blue.Count ? value : powerCounts.Blue.Count;
                        break;
                }
            }
        }

        var power = powerCounts.Red.Count * powerCounts.Green.Count * powerCounts.Blue.Count;
        return Task.FromResult(power);
    }

    public static async Task<int> PartTwo(string filename = "input.txt")
    {
        var totalPower = 0;
        
        await LineUtils.ProcessFileLinesAsync("Day2", filename, PartTwoHelper, async result => await Task.Run(() => totalPower += result));

        return totalPower;
    }
}