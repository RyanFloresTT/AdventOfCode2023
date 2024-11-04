using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode23.Day3;

public static class Day3
{
    private static string _symbols = string.Empty;

    static bool SurroundedBySymbol(int startingIndex, int endIndex, string? line)
    {
        if (string.IsNullOrEmpty(line)) return false;

        // Clamp indices within bounds of the line
        startingIndex = Math.Clamp(startingIndex, 0, line.Length - 1);
        endIndex = Math.Clamp(endIndex, 0, line.Length - 1);

        // Define a substring around the number's starting and ending positions
        var start = Math.Max(0, startingIndex - 1);  // Include one position to the left
        var length = Math.Min(line.Length, endIndex + 2) - start;  // Include one position to the right

        var substring = line.Substring(start, length);
        
        // Check if any symbol is present in the substring
        return _symbols.Any(symbol => substring.Contains(symbol));
    }

    public static async Task<int> PartOne(string filename = "input.txt")
    {
        _symbols = Day3Utils.GetSymbols();
        var lineTotal = 0;
        var lastLine = string.Empty;

        try
        {
            using StreamReader sr = new($"C:/Users/rcflo/RiderProjects/AdventOfCode2023/Day3/{filename}");
            var currentLine = await sr.ReadLineAsync();
            var nextLine = await sr.ReadLineAsync();

            while (currentLine != null)
            {
                // Iterate through characters in current line
                for (var i = 0; i < currentLine.Length; i++)
                {
                    if (!char.IsDigit(currentLine[i])) continue;

                    var digitCharacters = string.Empty;
                    var startingIndex = i;

                    // Collect all consecutive digit characters
                    while (i < currentLine.Length && char.IsDigit(currentLine[i]))
                    {
                        digitCharacters += currentLine[i];
                        i++;
                    }

                    if (digitCharacters.Length == 0) continue;

                    var endingIndex = i - 1;

                    // Check if the number is surrounded by symbols in any of the lines
                    if (SurroundedBySymbol(startingIndex, endingIndex, currentLine) ||
                        SurroundedBySymbol(startingIndex, endingIndex, lastLine) ||
                        (nextLine != null && SurroundedBySymbol(startingIndex, endingIndex, nextLine)))
                    {
                        lineTotal += int.Parse(digitCharacters);
                    }
                }

                // Move to the next set of lines
                lastLine = currentLine;
                currentLine = nextLine;
                nextLine = await sr.ReadLineAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}\n{e.Source}\n{e.StackTrace}");
        }

        return lineTotal;
    }
}
