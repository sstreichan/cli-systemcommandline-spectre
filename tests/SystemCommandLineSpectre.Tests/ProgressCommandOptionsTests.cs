using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemCommandLineSpectre.Console.Tests;

[TestClass]
public class ProgressCommandOptionsTests
{
    [TestMethod]
    public void Validate_ShouldThrow_WhenDurationIsNonPositive()
    {
        // Arrange
        var opts = new ProgressCommandOptions { DurationSeconds = 0 };

        // Act
        Action act = () => opts.Validate();

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>()
            .Which.ParamName.Should().Be(nameof(ProgressCommandOptions.DurationSeconds));
    }

    [TestMethod]
    public void Validate_ShouldNotThrow_WhenDurationIsPositive()
    {
        // Arrange
        var opts = new ProgressCommandOptions { DurationSeconds = 5 };

        // Act / Assert
        Action act = () => opts.Validate();
        act.Should().NotThrow();
    }
}
