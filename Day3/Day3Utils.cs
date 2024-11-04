namespace AdventOfCode23.Day3;

public static class Day3Utils
{
    public static string GetSymbols()
    {
        var symbols = string.Empty;
        // Read the entire file content
        const string filePath = "C:/Users/rcflo/RiderProjects/AdventOfCode2023/Day3/input.txt";
        var content = File.ReadAllText(filePath);

        // Remove all digits and periods
        var filteredContent = new string(content
            .Where(c => !char.IsDigit(c) && c != '.')
            .ToArray()).ToHashSet<char>();

        symbols = filteredContent.Aggregate(symbols, (current, c) => current + c);
        return symbols;
    }
}