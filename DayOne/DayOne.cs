namespace AdventOfCode23.DayOne;
using System.IO;

public static class DayOne
{
    public static void PrintSolution()
    {
        // PartOne();
        Console.WriteLine(PartTwo());
    }

    static void PartOne()
    {
        var sum = 0;
        
        try
        {
            StreamReader sr = new("C:/Users/rryan/RiderProjects/AdventOfCode23/DayOne/input.txt");
            var line = sr.ReadLine();
            while (line != null)
            {
                var twoDigits = string.Empty;
                
                // start on left, stopping when a number is reached
                for (var i = 0; i < line.Length; i++)
                {
                    if (!int.TryParse(line[i].ToString(), out _)) continue;
                    twoDigits += line[i].ToString();
                    break;
                }

                // start on right, stopping when a number is reached
                for (var i = line.Length - 1; i >= 0; i--)
                {
                    if (!int.TryParse(line[i].ToString(), out _)) continue;
                    twoDigits += line[i].ToString();
                    break;
                }
                sum += int.Parse(twoDigits);
                
                line = sr.ReadLine();
            }

            sr.Close();
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine($"Sum: {sum}");
        }
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
    
    public static int PartTwo(string filename = "input.txt")
    {
        var sum = 0;
    
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
    
        try
        {
            using StreamReader sr = new($"C:/Users/rryan/RiderProjects/AdventOfCode23/DayOne/{filename}");
            var line = sr.ReadLine();
            while (line != null)
            {
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

                    sum += lineTotal;
                }
            
                line = sr.ReadLine();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

        return sum;
    }

}