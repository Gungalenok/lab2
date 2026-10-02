using Xunit;

namespace lab2.Tests;

public class PlayerTests
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(10, 10)]
    [InlineData(11, 1)]
    [InlineData(-1, 9)]
    public void First_move_sets_wrapped_location(int steps, int expected)
    {
        Cat cat = new Cat(10);

        cat.Move(steps);

        Assert.Equal(expected, cat.Location);
        Assert.Equal(State.Playing, cat.State);
        Assert.Equal(0, cat.DistanceTraveled);
    }

    [Fact]
    public void Move_wraps_around_and_counts_distance()
    {
        Cat cat = new Cat(10);
        cat.Move(9);

        cat.Move(3);
        cat.Move(-2);

        Assert.Equal(10, cat.Location);
        Assert.Equal(5, cat.DistanceTraveled);
    }

    [Fact]
    public void DistanceTo_is_absolute_difference_between_cells()
    {
        Cat cat = new Cat(20);
        Mouse mouse = new Mouse(20);
        cat.Move(17);
        mouse.Move(6);

        Assert.Equal(11, cat.DistanceTo(mouse));
    }
}
