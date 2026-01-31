namespace SystemCommandLineSpectre.Infrastructure;

/// <summary>
/// Base interface for all command handlers in the CLI application.
/// Defines the contract for executing commands with proper error handling and logging.
/// </summary>
public interface ICommandHandler
{
    /// <summary>
    /// Executes the command asynchronously.
    /// </summary>
    /// <returns>Exit code indicating the result of the command execution (0 for success).</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled by the user.</exception>
    /// <exception cref="CommandException">Thrown when the command execution fails.</exception>
    Task<int> ExecuteAsync();
}
