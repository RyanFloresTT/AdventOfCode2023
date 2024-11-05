namespace AdventOfCode23.Day4;
using Utils;
public static class Day4
{
    //                     Lottery               My Numbers
    // format : Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    static Task<int> PartOneHelper(string? line)
    {
        if (!CleanCardLine(line, out var scoringNumberCount)) return Task.FromResult(0);
        
        // calculate the cumulative score with doubling rule
        
        if (scoringNumberCount <= 0) return Task.FromResult(0);
        
        var score = Math.Pow(2, scoringNumberCount - 1);

        return Task.FromResult((int)score);
    }

    private static bool CleanCardLine(string? line, out int scoringNumberCount)
    {
        var myNumbers = new HashSet<int>();
        var lotteryNumbers = new List<int>();
        scoringNumberCount = 0;

        if (string.IsNullOrWhiteSpace(line))
        {
            return false;
        }

        var numbersSection = line[(line.IndexOf(':') + 1)..];
        var separatorIndex = numbersSection.IndexOf('|');

        if (separatorIndex == -1)
        {
            return false;
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
        
        scoringNumberCount = lotteryNumbers.Count(number => myNumbers.Contains(number));
        
        return true;
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
        if (Multipliers.First != null)
        {
            Multipliers.First.Value++;
        }
        else
        {
            Multipliers.AddFirst(1);
        }
    
        if (!CleanCardLine(line, out var scoringNumberCount)) return Task.FromResult(0);
    
        var currentNode = Multipliers.First;
        var currentMultiplier = currentNode.Value;
        var nextNode = currentNode.Next;

        if (nextNode == null)
        {
            for (var i = 0; i < scoringNumberCount; i++)
            {
                Multipliers.AddLast(currentMultiplier);
            }
        }
        else
        {
            for (var i = 0; i < scoringNumberCount; ++i)
            {
                if (nextNode != null)
                {
                    nextNode.Value += currentMultiplier;
                    nextNode = nextNode.Next;
                }
                else
                {
                    Multipliers.AddLast(currentMultiplier);
                }
            }
        }
    
        Multipliers.RemoveFirst();
        return Task.FromResult(currentNode.Value);
    }

    public static async Task<int> PartTwo(string filename = "input.txt")
    {
        var score = 0;
        
        await LineUtils.ProcessFileLinesAsync("Day4", filename, PartTwoHelper, async result => await Task.Run(() => score += result));

        return score;
    }
}