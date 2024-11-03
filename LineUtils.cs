namespace AdventOfCode23;

public static class LineUtils
{
    /// <summary>
    /// Performs a given func onto each line in a given file and directory name.
    /// </summary>
    /// <param name="directoryName">The directory for the input file.</param>
    /// <param name="filename">The file's name.</param>
    /// <param name="processLine">The func to process each file line with.</param>
    /// <param name="handleResult">The action to handle the result of each processed line.</param>
    public static void ProcessFileLines(string directoryName, string filename, Func<string?, int> processLine, Action<int> handleResult)
    {
        try
        {
            using StreamReader sr = new($"C:/Users/rryan/RiderProjects/AdventOfCode23/{directoryName}/{filename}");
            var line = sr.ReadLine();
        
            while (line != null)
            {
                var res = processLine(line);
                handleResult(res);
                line = sr.ReadLine();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}