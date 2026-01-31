using Spectre.Console;

namespace SystemCommandLineSpectre.Console.Commands;

/// <summary>
/// Factory class for creating the Info command.
/// Displays system information including OS, runtime, machine name, and processor count.
/// </summary>
public static class InfoCommand
{
    /// <summary>
    /// Creates and configures the Info command with its handler.
    /// </summary>
    /// <param name="handler">The command handler responsible for executing the info command.</param>
    /// <returns>A configured Command instance for system info display.</returns>
    /// <exception cref="ArgumentNullException">Thrown when handler is null.</exception>
    public static Command Create(IInfoCommandHandler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        var infoCommand = new Command("info", "Display system information");

        infoCommand.SetAction(async _ =>
        {
            try
            {
                var exitCode = await handler.ExecuteAsync();
                Environment.ExitCode = exitCode;
            }
            catch (OperationCanceledException)
            {
                AnsiConsole.MarkupLine($"[yellow]{Messages.Error.InfoCommand_Cancelled}[/]");
                Environment.ExitCode = 1;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{string.Format(Messages.Error.Command_ExecutionError, ex.Message)}[/]");
                Environment.ExitCode = 1;
            }
        });

        return infoCommand;
    }
}
