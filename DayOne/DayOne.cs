using AdventOfCode23.DayTwo;

namespace AdventOfCode23.DayOne;
using System.IO;

public static class DayOne
{

    static Task<int> PartOneHelper(string? line)
    {
        var lineSum = 0;

        // Find the first numeric character from the left
        for (var i = 0; i < line.Length; i++)
        {
            if (!char.IsDigit(line[i])) continue;
            lineSum += int.Parse(line[i].ToString()) * 10;
            break;
        }

        // Find the first numeric character from the right
        for (var i = line.Length - 1; i >= 0; i--)
        {
            if (!char.IsDigit(line[i])) continue;
            lineSum += int.Parse(line[i].ToString());
            break;
        }

        Console.WriteLine($"Line Sum = {lineSum}");
        return Task.FromResult(lineSum);
    }

    public static async Task<int> PartOne(string filename = "input.txt")
    {
        var sum = 0;

        await LineUtils.ProcessFileLinesAsync("DayOne", filename, PartOneHelper, async result => await Task.Run(() => sum += result));        
        
        return sum;
    }


    struct NumberWord
    {
        public NumberWord(string word, int number, int position)
        {
            Word = word;
            Number = number;
            Position = position;
        }

        public string Word { get; set; }
        public int Number { get; set; }
        public int Position { get; set; }
    }

    static Task<int> PartTwoHelper(string? line)
    {
        List<NumberWord> numberWords = new List<NumberWord>
        {
            new("one", 1, 0), 
            new("two", 2, 0), 
            new("three", 3, 0), 
            new("four", 4, 0), 
            new("five", 5, 0), 
            new("six", 6, 0), 
            new("seven", 7, 0), 
            new("eight", 8, 0), 
            new("nine", 9, 0)
        };
    
        var lineTotal = 0;

        List<NumberWord> foundWords = new();
            
        foreach (var word in numberWords)
        {
            var startPos = 0;
            while ((startPos = line.IndexOf(word.Word, startPos, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                foundWords.Add(new(word.Word, word.Number, startPos));
                startPos += word.Word.Length;
            }
        }

        for (var i = 0; i < line.Length; i++)
        {
            if (int.TryParse(line[i].ToString(), out var digit))
            {
                foundWords.Add(new("", digit, i));
            }
        }
            
        if (foundWords.Count != 0)
        {
            var ordered = foundWords.OrderBy(x => x.Position);
            var firstNumberWord = ordered.First().Number;
            var lastNumberWord = ordered.Last().Number;
                
            lineTotal += firstNumberWord * 10;
            lineTotal += lastNumberWord;

        }
        return Task.FromResult(lineTotal);
    }
    
    public static async Task<int> PartTwo(string filename = "input.txt")
    {
        var sum = 0;
    
        await LineUtils.ProcessFileLinesAsync("DayOne", filename, PartTwoHelper, async result => await Task.Run(() => sum += result));

        return sum;
    }

}