namespace AdventOfCode23.Day4;

public static class Day4
{
    // format : Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    static Task<int> PartOneHelper(string? line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return Task.FromResult(0);

        var numbersSection = line[(line.IndexOf(':') + 1)..];
        var separatorIndex = numbersSection.IndexOf('|');

        if (separatorIndex == -1)
            return Task.FromResult(0);

        // extract and parse lottery numbers
        var lotteryNumberStrings = numbersSection[..separatorIndex].Trim();
        var lotteryNumbers = lotteryNumberStrings.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        // extract and parse my numbers
        var myNumberStrings = numbersSection[(separatorIndex + 1)..].Trim();
        var myNumbers = myNumberStrings.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToHashSet();

        // calculate the cumulative score with doubling rule
        var scoringNumbers = lotteryNumbers.Count(number => myNumbers.Contains(number));
        
        if (scoringNumbers <= 0) return Task.FromResult(0);
        
        var score = Math.Pow(2, scoringNumbers - 1);

        return Task.FromResult((int)score);
    }

    
    public static async Task<int> PartOne(string filename = "input.txt")
    {
        var points = 0;

        await LineUtils.ProcessFileLinesAsync("Day4", filename, PartOneHelper, async result => await Task.Run(() => points += result));
        
        return points;
    }
}