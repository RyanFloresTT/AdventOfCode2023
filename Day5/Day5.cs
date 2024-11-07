using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode23.Day5;
using static Utils.FilePathUtils;

public static class Day5
{
    public static async Task<long> PartOne(string filename = "input.txt")
    {
        List<long> seeds = [];
        var location = long.MaxValue;
        List<GardenMap> maps = new();
        
        try
        {
            using StreamReader sr = new($"{GetWorkingDirectory()}Day5/{filename}");
            string? line = await sr.ReadLineAsync();

            if (line != null)
            {
                // get seeds
                seeds = line[6..].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            }
            
            await FillMapsAsync(sr, maps);

            /*Console.WriteLine($"Total Maps: {maps.Count}.");
            foreach (var map in maps)
            {
                Console.WriteLine($"Name: {map.Name}.");
                foreach (var data in map.Map)
                {
                    Console.WriteLine($"\t{data.Destination} -> {data.Source} -> {data.Length}.");
                }
            }*/
            
            // Process each map and compute results as needed
            return seeds.Select(seed => maps.ConvertAll(seed)).Prepend(location).Min();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message + e.StackTrace);
        }
        
        return location;
    }
    
    public static async Task<long> PartTwo(string filename = "example.txt")
    {        
        List<long> seeds = [];
        var location = long.MaxValue;
        List<GardenMap> maps = new();
        
        try
        {
            using StreamReader sr = new($"{GetWorkingDirectory()}Day5/{filename}");
            var line = await sr.ReadLineAsync();

            if (line != null)
            {
                // get seeds
                seeds = line[6..].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            }
            
            // get seed pairs

            if (seeds.Count % 2 == 1) {throw new Exception("Seeds must have pairs!");}
            
            // Start reading maps
            await FillMapsAsync(sr, maps);

            /*Console.WriteLine($"Total Maps: {maps.Count}.");
            foreach (var map in maps)
            {
                Console.WriteLine($"Name: {map.Name}.");
                foreach (var data in map.Map)
                {
                    Console.WriteLine($"\t{data.Destination} -> {data.Source} -> {data.Length}.");
                }
            }*/


            foreach (var seedRange in GenerateSeedRanges(seeds))
            {
                for (var seed = seedRange.Start; seed < seedRange.Start + seedRange.Length; seed++)
                {
                    // somehow say "oh... this seed will lead to a higher value, exit out of this loop"
                    var res = maps.ConvertAll(seed);
                    location = res < location ? res : location;
                }
            }
            
            /*soil-to-fertilizer map:
            0 15 37
            37 52 2
            39 0 15*/
            
            
            // Process each map and compute results as needed
            return location;
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message + e.StackTrace);
        }
        
        return location;
    }

    private static IEnumerable<SeedRange> GenerateSeedRanges(List<long> seeds)
    {
        for (var i = 1; i <= seeds.Count; i += 2)
        {
            yield return new SeedRange
            {
                Start = seeds[i - 1],
                Length = seeds[i]
            };
        }
    }
    
    struct SeedRange
    {
        public long Start { get; set; }
        public long Length { get; set; }
    }

    private static async Task FillMapsAsync(StreamReader sr, List<GardenMap> maps)
    {
        string? line;
        while ((line = await sr.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // skip initial blank lines

            if (!line.Contains("map")) continue;
            // Create a new map for each "map" section
            var map = new GardenMap { Name = line.Trim() };
                    
            // Parse lines within the current map section
            while ((line = await sr.ReadLineAsync()) != null && !string.IsNullOrWhiteSpace(line))
            {
                var data = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();

                if (data.Length != 3)
                    throw new Exception("Data line must have exactly 3 elements");

                map.Map.Add(new ConversionData
                {
                    Destination = data[0],
                    Source = data[1],
                    Length = data[2]
                });
            }
                    
            maps.Add(map);
        }
    }
}
public struct ConversionData
{
    public long Destination { get; set; }
    public long Source { get; set; }
    public long Length { get; set; }
}
class GardenMap
{
    public List<ConversionData> Map { get; set; }
    public string Name { get; set; }

    public GardenMap()
    {
        Map = new();
    }

    public long Convert(long input)
    {
        foreach (var conversion in Map)
        {
            if (input <= conversion.Source + conversion.Length - 1 && input > conversion.Source)
            {
                return input + (conversion.Destination - conversion.Source);
            }
        }
        return input;
    }
}

static class MapHelper
{
    public static long ConvertAll(this List<GardenMap> maps, long input) =>
        maps.Aggregate(input, (current, map) => map.Convert(current));
}
