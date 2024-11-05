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
    public static async Task ProcessFileLinesAsync(string directoryName, string filename, Func<string?, Task<int>> processLine, Func<int, Task> handleResult)
    {
        try
        {
            using StreamReader sr = new($"C:/Users/rryan/RiderProjects/AdventOfCode23/{directoryName}/{filename}");
            var line = await sr.ReadLineAsync();

            while (line != null)
            {
                var res = await processLine(line);
                await handleResult(res);
                line = await sr.ReadLineAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}