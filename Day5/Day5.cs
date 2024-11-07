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
            return seeds.Select(seed => maps.Convert(seed)).Prepend(location).Min();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message + e.StackTrace);
        }
        
        return location;
    }
    
    public static async Task<long> PartTwo(string filename = "input.txt")
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

            var locationTasks = GenerateSeedRanges(seeds)
                .AsParallel()
                .Select(async seedRange => await Task.Run(() => {
                        var minLocation = long.MaxValue;

                        for (long i = seedRange.Start; i < seedRange.Start + seedRange.Length; i++)
                        {
                            // Calculate converted value for each map sequence
                            var convertedValue = maps.Convert(i);

                            // Update minLocation directly if the value is lower
                            if (convertedValue < minLocation)
                            {
                                minLocation = convertedValue;
                            }
                        }

                        return minLocation;
                    })
            );
            
            /*soil-to-fertilizer map:
            0 15 37
            37 52 2
            39 0 15*/
            
            var results = await Task.WhenAll(locationTasks);
            location = results.Min();
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
    public static long Convert(this List<GardenMap> maps, long input)
    {
        var result = input;
        
        foreach (var map in maps)
        {
            result = map.Convert(result);
        }

        return result;
    } 
}
