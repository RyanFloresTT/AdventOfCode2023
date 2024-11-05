namespace AdventOfCode23.Day3;
using static Utils.FilePathUtils;

public static class Day3Utils
{
    public static string GetSymbols()
    {
        var symbols = string.Empty;

        // Read the entire file content
        var filePath = $"{GetWorkingDirectory()}Day3/input.txt";
        var content = File.ReadAllText(filePath);

        // Remove all digits and periods
        var filteredContent = new string(content
            .Where(c => !char.IsDigit(c) && c != '.')
            .ToArray()).ToHashSet<char>();

        symbols = filteredContent.Aggregate(symbols, (current, c) => current + c);
        return symbols;
    }
}
