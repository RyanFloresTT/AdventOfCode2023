namespace AdventOfCode23.Utils;

public static class FilePathUtils
{
    public static string GetWorkingDirectory()
    {
        var projectDirectoryName = "AdventOfCode2023";
        var workingDirectory = Directory.GetCurrentDirectory()[..(Directory.GetCurrentDirectory().IndexOf(projectDirectoryName, StringComparison.Ordinal) + projectDirectoryName.Length + 1)];
        workingDirectory = workingDirectory.Replace("\\", "/");
        return workingDirectory;
    }
}