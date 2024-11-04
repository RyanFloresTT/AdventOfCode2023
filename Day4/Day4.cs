namespace AdventOfCode23.Day4;

public static class Day4
{
    static Task<int> PartOneHelper(string? line)
    {
        return Task.FromResult(0);
    }
    
    public static async Task<int> PartOne(string filename)
    {
        var points = 0;

        await LineUtils.ProcessFileLinesAsync("Day4", filename, PartOneHelper, async result => Task.Run(() => points += result));
        
        return points;
    }
}