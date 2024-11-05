using AdventOfCode23.Day1;
using AdventOfCode23.Day2;
using AdventOfCode23.Day3;
using AdventOfCode23.Day4;

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
}