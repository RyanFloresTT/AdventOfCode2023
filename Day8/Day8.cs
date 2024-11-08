using System.Diagnostics;
using AdventOfCode23.Utils;

namespace AdventOfCode23.Day8;

public static class Day8
{
    static Dictionary<string, Node> nodes = new();
    static string directions = string.Empty;
    struct Node(string left, string right)
    {
        public string Left = left;
        public string Right = right;
    }

    static async Task<KeyValuePair<string, Node>> ParseNodes(string? line)
    {
        if (!string.IsNullOrWhiteSpace(line) && !line.Contains('='))
        {
            directions = line.Trim();
            return default;
        }
        if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
        {
            return default;
        }
        var parts = line.Split('=');
        var name = parts[0].Trim();
        
        var values = parts[1].Trim().Trim('(', ')').Split(',');
        
        if (values.Length != 2)
        {
            throw new FormatException("Line format is incorrect. Expected format: 'AAA = (BBB, CCC)'");
        }
        
        var leftValue = values[0].Trim();
        var rightValue = values[1].Trim();
        
        var node = new Node(leftValue, rightValue);
        return new KeyValuePair<string, Node>(name, node);
    }
    
    public static async Task<int> PartOne(string filename = "input.txt")
    {
        var timesTraveled = 0;
        var stopwatch = Stopwatch.StartNew();
        await GenerateNodesAndDirectionsAsync(filename);
        stopwatch.Stop();
        
        // go through dictionary, looking at the next left or right, counting each step
        
        var currentNodeName = nodes.Keys.First();
        Console.WriteLine($"First Node: {currentNodeName}");
        Console.WriteLine($"Node Count: {nodes.Count}");
        Console.WriteLine($"Directions: {directions}");
        
        var visitedNodes = new HashSet<string>();
        
        var i = 0;
        while (currentNodeName != "ZZZ")
        {
            if (visitedNodes.Contains(currentNodeName))
            {
                Console.WriteLine($"Cycle detected at node {currentNodeName}. Exiting to prevent infinite loop.");
                break;
            }
            
            var direction = directions[i % directions.Length];
            var nextNode = direction == 'L' ? nodes[currentNodeName].Left : nodes[currentNodeName].Right;
           
            currentNodeName = nextNode;

            timesTraveled++;
            i++;
        }
        
        Console.WriteLine($"Finished in {stopwatch.Elapsed}ms.");
        
        return timesTraveled;
    }
    public static async Task<int> PartTwo(string filename = "example1.txt")
    {
        await GenerateNodesAndDirectionsAsync(filename);
        
        return 0;
    }

    static async Task GenerateNodesAndDirectionsAsync(string filename)
    {
        if (nodes.Count != 0) nodes.Clear();
        await LineUtils.ProcessFileLinesAsync("Day8", filename, ParseNodes,
            async result => await Task.Run(() => {
                if (!result.Equals(default(KeyValuePair<string, Node>)))
                    nodes.Add(result.Key, result.Value);
            })
        );
    }
}