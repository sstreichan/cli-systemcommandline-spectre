using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CommandFactoryTests
{
    [TestMethod]
    public void CreateCommands_ShouldReturn_NonNullCommands()
    {
        var infoHandler = Mock.Of<IInfoCommandHandler>(h => h.ExecuteAsync() == Task.FromResult(0));
        var progressHandler = new ProgressCommandHandler(Mock.Of<ILogger<ProgressCommandHandler>>());

        var factory = new CommandFactory(infoHandler, progressHandler);

        var info = factory.CreateInfoCommand();
        var progress = factory.CreateProgressCommand();

        info.Should().NotBeNull();
        progress.Should().NotBeNull();
    }
}
