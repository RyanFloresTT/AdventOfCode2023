namespace AdventOfCode23.Day4;

public static class Day4
{
    // format : Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    static Task<int> PartOneHelper(string? line)
    {
        if (!CleanCardLine(line, out var lotteryNumbers, out var myNumbers)) return Task.FromResult(0);

        // calculate the cumulative score with doubling rule
        var scoringNumbers = lotteryNumbers.Count(number => myNumbers.Contains(number));
        
        if (scoringNumbers <= 0) return Task.FromResult(0);
        
        var score = Math.Pow(2, scoringNumbers - 1);

        return Task.FromResult((int)score);
    }

    private static bool CleanCardLine(string? line, out List<int> lotteryNumbers, out HashSet<int> myNumbers)
    {
        myNumbers = new HashSet<int>();
        lotteryNumbers = new List<int>();

        if (string.IsNullOrWhiteSpace(line))
        {
            return true;
        }

        var numbersSection = line[(line.IndexOf(':') + 1)..];
        var separatorIndex = numbersSection.IndexOf('|');

        if (separatorIndex == -1)
        {
            return true;
        }

        // extract and parse lottery numbers
        var lotteryNumberStrings = numbersSection[..separatorIndex].Trim();
        lotteryNumbers = lotteryNumberStrings.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        // extract and parse my numbers
        var myNumberStrings = numbersSection[(separatorIndex + 1)..].Trim();
        myNumbers = myNumberStrings.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToHashSet();
        return false;
    }


    public static async Task<int> PartOne(string filename = "input.txt")
    {
        var points = 0;

        await LineUtils.ProcessFileLinesAsync("Day4", filename, PartOneHelper, async result => await Task.Run(() => points += result));
        
        return points;
    }

    private static readonly LinkedList<int> Multipliers = [];

    static Task<int> PartTwoHelper(string? line)
    {
        // add 1 to the multiplier LL for each line, this is because we are counting copies from previous wins PLUS original card
        if (Multipliers.First != null)
        {
            Multipliers.First.Value++;
        }
        
        // get count of winning numbers
        if (!CleanCardLine(line, out var lotteryNumbers, out var myNumbers)) return Task.FromResult(0);
        var winningNumberCount = lotteryNumbers.Count(number => myNumbers.Contains(number));
        
        // add 1 for x amount of winning numbers
        var currentNode = Multipliers.First?.Next;
        
        for (var i = 0; i < winningNumberCount && currentNode != null; i++)
        {
            currentNode.Value += 1;
            currentNode = currentNode.Next;
        }
        
        while (Multipliers.Count < winningNumberCount)
        {
            Multipliers.AddLast(1);
        }
        
        // call PartOneHelper and multiply result by the first from the deque
        var lineScore = PartOneHelper(line);
        
        var multiplier = Multipliers.First;
        if (Multipliers.Count > 0) Multipliers.RemoveFirst();
        
        return multiplier != null ? Task.FromResult(lineScore.Result * multiplier.Value) : lineScore;
    } 

    public static async Task<int> PartTwo(string filename = "input.txt")
    {
        var score = 0;
        
        await LineUtils.ProcessFileLinesAsync("Day4", filename, PartTwoHelper, async result => await Task.Run(() => score += result));

        return score;
    }
}