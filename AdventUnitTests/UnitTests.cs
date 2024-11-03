using AdventOfCode23.DayOne;
using AdventOfCode23.DayTwo;

namespace AdventUnitTests;

public class Tests
{

    [Test]
    public void DayOne_PartTwo_Test()
    {
        var res = DayOne.PartTwo("testinput.txt");
        Assert.That(res, Is.EqualTo(281));
    }
    
    [Test]
    public void DayTwo_PartOne_Test()
    {
        var res = DayTwo.PartOne("testinput.txt");
        Assert.That(res, Is.EqualTo(8));
    }
}