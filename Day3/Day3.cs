namespace AdventOfCode23.Day3;

public static class Day3
{
    static string symbols = "*/=%$&@#";

    static bool SurroundedBySymbol(string line)
    {
        return false;
    }
    
    public static async Task<int> PartOne(string filename = "example.txt")
    {

        try
        {
            using StreamReader sr = new($"C:/Users/rcflo/RiderProjects/AdventOfCode2023/Day3/{filename}");
            var line = await sr.ReadLineAsync();

            var digits = new List<int>();
            
            while (line != null)
            {
                // look at each line
                // if there is a digit, continue until we run out of digits
                // grab the length of the digits, then search around for any symbols, if there is one, add the number to the sum
                for (var i = 0; i < line.Length; i++)
                {
                    var character = line[i];
                    var digitCharacters = string.Empty;
                    if (char.IsDigit(character))
                    {
                        while (char.IsDigit(character))
                        {
                            digitCharacters += character;
                            i++;
                            character = line[i];
                        }
                        var startingIndex = line.IndexOf(digitCharacters, StringComparison.Ordinal);
                        var endingIndex = line.IndexOf(digitCharacters, StringComparison.Ordinal) + digitCharacters.Length;
                        var range = (startingIndex, endingIndex);

                        if (SurroundedBySymbol(line.Substring(startingIndex, endingIndex - startingIndex)))
                        {
                            
                        }
                    }
                }

                line = await sr.ReadLineAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        return 0;
    }
}