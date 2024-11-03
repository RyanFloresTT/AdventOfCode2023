using AdventOfCode23.DayOne;
using AdventOfCode23.DayTwo;

namespace AdventUnitTests;

public class Tests
{
    [Test]
    public void DayOne_PartOne()
    {
        var res = DayOne.PartOne("partone_testinput.txt");
        Assert.That(res, Is.EqualTo(142));
    }

    [Test]
    public void DayOne_PartTwo()
    {
        var res = DayOne.PartTwo("parttwo_testinput.txt");
        Assert.That(res, Is.EqualTo(281));
    }
    
    [Test]
    public void DayTwo_PartOne()
    {
        var res = DayTwo.PartOne("testinput.txt");
        Assert.That(res, Is.EqualTo(8));
    }
    
    [Test]
    public void DayTwo_PartTwo()
    {
        var res = DayTwo.PartTwo("testinput.txt");
        Assert.That(res, Is.EqualTo(2286));
    }
    
    [Test]
    public void DayThree_PartOne()
    {
        var res = DayThree.PartOne("testinput.txt");
        Assert.That(res, Is.EqualTo(4361));
    }
}