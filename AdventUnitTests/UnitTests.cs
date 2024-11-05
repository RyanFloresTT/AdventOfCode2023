using AdventOfCode23.Day1;
using AdventOfCode23.Day2;
using AdventOfCode23.Day3;
using AdventOfCode23.Day4;

namespace AdventUnitTests;

public class Tests
{
    [Test]
    public void Day1_Part1()
    {
        var res = Day1.PartOne("part_one_example.txt").Result;
        Assert.That(res, Is.EqualTo(142));
    }

    [Test]
    public void Day1_Part2()
    {
        var res = Day1.PartTwo("part_two_example.txt").Result;
        Assert.That(res, Is.EqualTo(281));
    }
    
    [Test]
    public void Day2_Part1()
    {
        var res = Day2.PartOne("example.txt").Result;
        Assert.That(res, Is.EqualTo(8));
    }
    
    [Test]
    public void Day2_Part2()
    {
        var res = Day2.PartTwo("example.txt").Result;
        Assert.That(res, Is.EqualTo(2286));
    }
    
    [Test]
    public void Day3_Part1()
    {
        var res = Day3.PartOne("example.txt").Result;
        Assert.That(res, Is.EqualTo(4361));
    }
    [Test]
    public void Day3_Part2()
    {
        var res = Day3.PartTwo("example.txt").Result;
        Assert.That(res, Is.EqualTo(467835));
    }
    [Test]
    public void Day4_Part1()
    {
        var res = Day4.PartOne("example.txt").Result;
        Assert.That(res, Is.EqualTo(13));
    }
}