namespace AdventOfCode23.Utils;

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
        var adventDirectoryName = "AdventOfCode2023";
        var workingDirectory = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().LastIndexOf(adventDirectoryName) + adventDirectoryName.Length + 1);
        workingDirectory = workingDirectory.Replace("\\", "/");
        try
        {
            using StreamReader sr = new($"{workingDirectory}{directoryName}/{filename}");
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

// "C:\Users\rcflo\RiderProjects\AdventOfCode2023\Day1\part_one_example.txt"