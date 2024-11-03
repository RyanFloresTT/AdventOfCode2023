namespace AdventOfCode23.DayTwo;

public static class DayTwo
{
    public static int PartOne(string filename = "input.txt")
    {
        try
        {
            using StreamReader sr = new($"C:/Users/rryan/RiderProjects/AdventOfCode23/DayTwo/{filename}.txt");
            var line = sr.ReadLine();
            while (line != null)
            {
                Console.ReadLine();
                line = sr.ReadLine();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

        return 0;
    }
}