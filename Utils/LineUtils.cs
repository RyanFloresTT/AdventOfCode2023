namespace AdventOfCode23.Utils;
using static Utils.FilePathUtils;
public static class LineUtils
{
    /// <summary>
    /// Processes each line in a given file, applying a generic function to each line and handling the result.
    /// </summary>
    /// <typeparam name="T">The type of the result after processing each line.</typeparam>
    /// <param name="directoryName">The directory for the input file.</param>
    /// <param name="filename">The file's name.</param>
    /// <param name="processLine">The function to process each file line with.</param>
    /// <param name="handleResult">The function to handle the result of each processed line.</param>
    public static async Task ProcessFileLinesAsync<T>(string directoryName, string filename, Func<string?, Task<T>> processLine, Func<T, Task> handleResult)
    {
        try
        {
            using StreamReader sr = new($"{GetWorkingDirectory()}{directoryName}/{filename}");
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