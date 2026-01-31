namespace SystemCommandLineSpectre.Console.Infrastructure;

using System.CommandLine;
using SystemCommandLineSpectre.Console.Commands;

/// <summary>
/// Default implementation of the command factory.
/// Creates System.CommandLine commands with their associated handlers via dependency injection.
/// </summary>
/// <remarks>
/// <para>
/// This class uses primary constructor syntax for dependency injection,
/// demonstrating modern C# patterns while maintaining AOT compatibility.
/// All dependencies are constructor-injected and validated for null.
/// </para>
/// <para>
/// The factory creates commands using static factory methods from command classes
/// (e.g., <see cref="InfoCommand.Create"/>) and injects pre-configured handlers.
/// This ensures all command configuration is centralized and testable.
/// </para>
/// </remarks>
/// <param name="infoHandler">The handler for the Info command.</param>
/// <param name="progressHandler">The handler for the Progress command.</param>
/// <exception cref="ArgumentNullException">Thrown when any handler parameter is null.</exception>
/// <example>
/// <code>
/// // Register in DI container
/// services.AddSingleton&lt;ICommandFactory, CommandFactory&gt;();
/// 
/// // Use in application
/// var factory = serviceProvider.GetRequiredService&lt;ICommandFactory&gt;();
/// var rootCommand = new RootCommand();
/// rootCommand.Add(factory.CreateInfoCommand());
/// rootCommand.Add(factory.CreateProgressCommand());
/// </code>
/// </example>
public sealed class CommandFactory(
    IInfoCommandHandler infoHandler,
    CommandHandler<ProgressCommandOptions> progressHandler) : ICommandFactory
{
    private readonly IInfoCommandHandler _infoHandler = infoHandler 
        ?? throw new ArgumentNullException(nameof(infoHandler));
    private readonly CommandHandler<ProgressCommandOptions> _progressHandler = progressHandler 
        ?? throw new ArgumentNullException(nameof(progressHandler));

    /// <summary>
    /// Creates the Info command with its configured handler.
    /// </summary>
    /// <returns>A configured Command instance for system info display.</returns>
    /// <remarks>
    /// Delegates to <see cref="InfoCommand.Create"/> with the injected handler.
    /// </remarks>
    public Command CreateInfoCommand() 
        => InfoCommand.Create(_infoHandler);
    
    /// <summary>
    /// Creates the Progress command with its configured handler.
    /// </summary>
    /// <returns>A configured Command instance for progress demonstration.</returns>
    /// <remarks>
    /// Delegates to <see cref="ProgressCommand.Create"/> with the injected handler.
    /// </remarks>
    public Command CreateProgressCommand() 
        => ProgressCommand.Create(_progressHandler);
}
