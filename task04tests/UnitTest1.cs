using task04;
using Xunit;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Cruiser_MoveForward_ShouldIncreasePosition()
    {
        var cruiser = new Cruiser();
        var initialPosition = cruiser.Position;
        cruiser.MoveForward();
        Assert.Equal(initialPosition + cruiser.Speed, cruiser.Position);
    }

    [Fact]
    public void Cruiser_Rotate_ShouldChangeAngle()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(90);
        Assert.Equal(90, cruiser.Angle);
    }

    [Fact]
    public void Cruiser_Fire_ShouldReduceAmmo()
    {
        var cruiser = new Cruiser();
        var initialAmmo = cruiser.Ammo;
        cruiser.Fire();
        Assert.Equal(initialAmmo - 1, cruiser.Ammo);
    }

    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        var fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(20, fighter.FirePower);
    }

    [Fact]
    public void Fighter_MoveForward_ShouldIncreasePosition()
    {
        var fighter = new Fighter();
        var initialPosition = fighter.Position;
        fighter.MoveForward();
        Assert.Equal(initialPosition + fighter.Speed, fighter.Position);
    }

    [Fact]
    public void Fighter_Rotate_ShouldChangeAngle()
    {
        var fighter = new Fighter();
        fighter.Rotate(45);
        Assert.Equal(45, fighter.Angle);
    }

    [Fact]
    public void Fighter_Fire_ShouldReduceAmmo()
    {
        var fighter = new Fighter();
        var initialAmmo = fighter.Ammo;

        fighter.Fire();

        Assert.Equal(initialAmmo - 1, fighter.Ammo);
    }
}
