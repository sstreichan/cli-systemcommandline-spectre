namespace SystemCommandLineSpectre.Console.Infrastructure;

using System.CommandLine;

/// <summary>
/// Factory interface for creating System.CommandLine Command instances.
/// Abstracts command creation to support dependency injection and testability.
/// </summary>
/// <remarks>
/// <para>
/// The factory pattern is used here to:
/// <list type="bullet">
/// <item>Separate command configuration from Program.cs</item>
/// <item>Enable easier testing of command creation</item>
/// <item>Support future extensibility (plugins, dynamic commands)</item>
/// <item>Maintain AOT compatibility with explicit registration</item>
/// </list>
/// </para>
/// <para>
/// Commands are created with their associated handlers pre-configured via
/// dependency injection. This ensures all dependencies are resolved at startup
/// and the command tree is fully AOT-compatible.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var factory = serviceProvider.GetRequiredService&lt;ICommandFactory&gt;();
/// var rootCommand = new RootCommand();
/// rootCommand.Add(factory.CreateInfoCommand());
/// rootCommand.Add(factory.CreateProgressCommand());
/// </code>
/// </example>
public interface ICommandFactory
{
    /// <summary>
    /// Creates the Info command with its configured handler.
    /// </summary>
    /// <returns>A configured <see cref="Command"/> instance for displaying system information.</returns>
    /// <remarks>
    /// The returned command is fully configured with its handler and ready to be added
    /// to a root command. The handler is resolved via dependency injection.
    /// </remarks>
    Command CreateInfoCommand();
    
    /// <summary>
    /// Creates the Progress command with its configured handler.
    /// </summary>
    /// <returns>A configured <see cref="Command"/> instance for progress demonstration.</returns>
    /// <remarks>
    /// The returned command is fully configured with its handler, options, and ready to be added
    /// to a root command. The handler is resolved via dependency injection.
    /// </remarks>
    Command CreateProgressCommand();
}
