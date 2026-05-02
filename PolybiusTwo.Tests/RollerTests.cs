namespace PolybiusTwo.Tests;

public class RollerTests
{
    [Test]
    public void FourDropLow_SixCount()
    {
        // Pull a result from FourDropLow and check it
        var result = CharGen.FourDropLow();
        Assert.That(result, Has.Count.EqualTo(6), "Should always roll 6 integers.");
    }

    [Test]
    public void FourDropLow_MinAndMax()
    {
        // Test a bunch of rolls
        for (int i = 0; i < 50; i++)
        {
            var result = CharGen.FourDropLow();
            foreach (int number in result)
            {
                Assert.That(number, Is.GreaterThanOrEqualTo(3), $"Roll {number} is below minimum");
                Assert.That(number, Is.LessThanOrEqualTo(18),   $"Roll {number} is above maximum");
            }
        }
    }

    [Test]
    public void ThreeInOrder_SixCount()
    {
        // Pull a result from FourDropLow and check it
        var result = CharGen.ThreeInOrder();
        Assert.That(result, Has.Count.EqualTo(6), "Should always roll 6 integers.");
    }

    [Test]
    public void ThreeInOrder_MinAndMax()
    {
        // Test a bunch of rolls
        for (int i = 0; i < 50; i++)
        {
            var result = CharGen.ThreeInOrder();
            foreach (int number in result)
            {
                Assert.That(number, Is.GreaterThanOrEqualTo(3), $"Roll {number} is below minimum");
                Assert.That(number, Is.LessThanOrEqualTo(18),   $"Roll {number} is above maximum");
            }
        }
    }
}