using System.CommandLine.Parsing;
using Spectre.Console;

namespace SystemCommandLineSpectre.Console.Commands;

/// <summary>
/// Factory class for creating the Progress command.
/// Displays a progress bar demonstration with configurable duration.
/// </summary>
public static class ProgressCommand
{
    /// <summary>
    /// Creates and configures the Progress command with its handler and options.
    /// </summary>
    /// <param name="handler">The command handler responsible for executing the progress command.</param>
    /// <returns>A configured Command instance for progress bar demonstration.</returns>
    /// <exception cref="ArgumentNullException">Thrown when handler is null.</exception>
    public static Command Create(CommandHandler<ProgressCommandOptions> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        var progressCommand = new Command("progress", "Show a progress bar demo");

        var durationOption = new Option<int>("--duration")
        {
            Description = "Duration in seconds for the progress demonstration",
            DefaultValueFactory = _ => 3
        };

        progressCommand.Add(durationOption);

        progressCommand.SetAction(async parseResult =>
        {
            try
            {
                var duration = parseResult.GetValue(durationOption);
                
                var options = new ProgressCommandOptions { DurationSeconds = duration };
                handler.SetOptions(options); // Validates automatically

                var exitCode = await handler.ExecuteAsync();
                Environment.ExitCode = exitCode;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                AnsiConsole.MarkupLine($"[red]{string.Format(Messages.Error.Command_InvalidArgument, ex.Message)}[/]");
                Environment.ExitCode = 1;
            }
            catch (OperationCanceledException)
            {
                AnsiConsole.MarkupLine($"[yellow]{Messages.Error.ProgressCommand_Cancelled}[/]");
                Environment.ExitCode = 1;
            }
            catch (ArgumentException ex)
            {
                AnsiConsole.MarkupLine($"[red]{string.Format(Messages.Error.Command_InvalidArgument, ex.Message)}[/]");
                Environment.ExitCode = 1;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{string.Format(Messages.Error.Command_ExecutionError, ex.Message)}[/]");
                Environment.ExitCode = 1;
            }
        });

        return progressCommand;
    }
}
