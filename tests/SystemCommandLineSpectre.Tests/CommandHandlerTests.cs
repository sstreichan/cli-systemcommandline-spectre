using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemCommandLineSpectre.Console.Tests;

public class DummyOptions : CommandOptions
{
    public int Value { get; init; }
    public override void Validate()
    {
        if (Value < 0) throw new ArgumentOutOfRangeException(nameof(Value));
    }
}

public class DummyHandler : CommandHandler<DummyOptions>
{
    public DummyHandler(ILogger<DummyHandler> logger) : base(logger) { }

    public override Task<int> ExecuteAsync() => Task.FromResult(0);
}

[TestClass]
public class CommandHandlerTests
{
    [TestMethod]
    public void SetOptions_StoresValidatedOptions()
    {
        var logger = Mock.Of<ILogger<DummyHandler>>();
        var handler = new DummyHandler(logger);

        var opts = new DummyOptions { Value = 1 };

        handler.SetOptions(opts);

        // Use reflection to access protected property for assertion
        var optionsProp = typeof(CommandHandler<DummyOptions>).GetProperty("Options", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public);
        optionsProp.Should().NotBeNull();
        var stored = optionsProp!.GetValue(handler) as DummyOptions;
        stored.Should().NotBeNull();
        stored!.Value.Should().Be(1);
    }

    [TestMethod]
    public void SetOptions_ValidatesOptions_ThrowsOnInvalid()
    {
        var logger = Mock.Of<ILogger<DummyHandler>>();
        var handler = new DummyHandler(logger);

        var opts = new DummyOptions { Value = -1 };

        Action act = () => handler.SetOptions(opts);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
