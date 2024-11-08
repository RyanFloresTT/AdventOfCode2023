using AdventOfCode23.Day1;
using AdventOfCode23.Day2;
using AdventOfCode23.Day3;
using AdventOfCode23.Day4;
using AdventOfCode23.Day5;
using AdventOfCode23.Day6;
using AdventOfCode23.Day7;
using AdventOfCode23.Day8;

namespace AdventUnitTests;

public class Tests
{
    [Test]
    public async Task Day1_Part1()
    {
        var res = await Day1.PartOne("part_one_example.txt");
        Assert.That(res, Is.EqualTo(142));
    }

    [Test]
    public async Task Day1_Part2()
    {
        var res = await Day1.PartTwo("part_two_example.txt");
        Assert.That(res, Is.EqualTo(281));
    }
    
    [Test]
    public async Task Day2_Part1()
    {
        var res = await Day2.PartOne("example.txt");
        Assert.That(res, Is.EqualTo(8));
    }
    
    [Test]
    public async Task Day2_Part2()
    {
        var res = await Day2.PartTwo("example.txt");
        Assert.That(res, Is.EqualTo(2286));
    }
    
    [Test]
    public async Task Day3_Part1()
    {
        var res = await Day3.PartOne("example.txt");
        Assert.That(res, Is.EqualTo(4361));
    }
    [Test]
    public async Task Day3_Part2()
    {
        var res = await Day3.PartTwo("example.txt");
        Assert.That(res, Is.EqualTo(467835));
    }
    [Test]
    public async Task Day4_Part1()
    {
        var res = await Day4.PartOne("example.txt");
        Assert.That(res, Is.EqualTo(13));
    }
    [Test]
    public async Task Day4_Part2()
    {
        var res = await Day4.PartTwo("example.txt");
        Assert.That(res, Is.EqualTo(30));
    }
    [Test]
    public async Task Day5_Part1()
    {
        var res = await Day5.PartOne("example.txt");
        Assert.That(res, Is.EqualTo(35));
    }
    
    [Test]
    public async Task Day5_Part2()
    {
        var res = await Day5.PartTwo("example.txt");
        Assert.That(res, Is.EqualTo(46));
    }
    [Test]
    public async Task Day6_Part1()
    {
        var res = await Day6.PartOne("example.txt");
        Assert.That(res, Is.EqualTo(288));
    }
    [Test]
    public async Task Day6_Part2()
    {
        var res = await Day6.PartTwo("example.txt");
        Assert.That(res, Is.EqualTo(71503));
    }
    [Test]
    public async Task Day7_Part1()
    {
        var res = await Day7.PartOne("example.txt");
        Assert.That(res, Is.EqualTo(6440));
    }
    [Test]
    public async Task Day7_Part2()
    {
        var res = await Day7.PartTwo("example.txt");
        Console.WriteLine(res);
        Assert.That(res, Is.EqualTo(5905));
    }
    [Test]
    public async Task Day8_Part1_Example1()
    {
        var res = await Day8.PartOne("example1.txt");
        Console.WriteLine(res);
        Assert.That(res, Is.EqualTo(2));
    }
    [Test]
    public async Task Day8_Part1_Example2()
    {
        var res = await Day8.PartOne("example2.txt");
        Console.WriteLine(res);
        Assert.That(res, Is.EqualTo(6));
    }
}