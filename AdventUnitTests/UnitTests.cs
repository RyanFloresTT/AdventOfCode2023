using AdventOfCode23.DayOne;
using AdventOfCode23.DayTwo;
using AdventOfCode23.DayThree;

namespace AdventUnitTests;

public class Tests
{
    [Test]
    public void DayOne_PartOne()
    {
        var res = DayOne.PartOne("part_one_example.txt").Result;
        Assert.That(res, Is.EqualTo(142));
    }

    [Test]
    public void DayOne_PartTwo()
    {
        var res = DayOne.PartTwo("part_two_example.txt").Result;
        Assert.That(res, Is.EqualTo(281));
    }
    
    [Test]
    public void DayTwo_PartOne()
    {
        var res = DayTwo.PartOne("example.txt").Result;
        Assert.That(res, Is.EqualTo(8));
    }
    
    [Test]
    public void DayTwo_PartTwo()
    {
        var res = DayTwo.PartTwo("example.txt").Result;
        Assert.That(res, Is.EqualTo(2286));
    }
    
    [Test]
    public void DayThree_PartOne()
    {
        var res = DayThree.PartOne("example.txt").Result;
        Assert.That(res, Is.EqualTo(4361));
    }
}