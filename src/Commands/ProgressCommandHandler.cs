namespace SystemCommandLineSpectre.Commands;

using Microsoft.Extensions.Logging;
using Spectre.Console;
using SystemCommandLineSpectre.Infrastructure;

/// <summary>
/// Handler interface for the Progress command.
/// Displays a progress bar demo with configurable duration.
/// </summary>
public interface IProgressCommandHandler : ICommandHandler
{
    /// <summary>
    /// Sets the duration for the progress bar in seconds.
    /// </summary>
    /// <param name="durationSeconds">The duration in seconds (must be positive).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when duration is less than or equal to 0.</exception>
    void SetDuration(int durationSeconds);
}

/// <summary>
/// Implements the Progress command handler.
/// Displays a progress bar with multiple tasks to demonstrate Spectre.Console capabilities.
/// </summary>
public class ProgressCommandHandler(ILogger<ProgressCommandHandler> logger) : IProgressCommandHandler
{
    private int _durationSeconds = 3;

    /// <summary>
    /// Sets the duration for the progress demonstration.
    /// </summary>
    /// <param name="durationSeconds">The duration in seconds (must be positive).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when duration is less than or equal to 0.</exception>
    public void SetDuration(int durationSeconds)
    {
        if (durationSeconds <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(durationSeconds), "Duration must be greater than 0.");
        }

        _durationSeconds = durationSeconds;
        logger.LogInformation("Progress duration set to {DurationSeconds} seconds", durationSeconds);
    }

    /// <summary>
    /// Executes the progress command and displays a progress bar with multiple tasks.
    /// </summary>
    /// <returns>Exit code (0 for success, 1 for failure).</returns>
    public async Task<int> ExecuteAsync()
    {
        try
        {
            logger.LogInformation("Executing progress command with duration {DurationSeconds}s", _durationSeconds);

            await AnsiConsole.Progress()
                .Columns(
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new SpinnerColumn()
                )
                .StartAsync(async ctx =>
                {
                    var task1 = ctx.AddTask("[green]Processing files[/]");
                    var task2 = ctx.AddTask("[yellow]Downloading data[/]");
                    var task3 = ctx.AddTask("[cyan]Building cache[/]");

                    var steps = _durationSeconds * 10;
                    for (int i = 0; i < steps; i++)
                    {
                        await Task.Delay(100);
                        task1.Increment(100.0 / steps);
                        
                        if (i > steps / 3)
                        {
                            task2.Increment(100.0 / steps);
                        }
                        
                        if (i > steps / 2)
                        {
                            task3.Increment(100.0 / steps);
                        }
                    }
                });

            AnsiConsole.MarkupLine("[bold green]✓ All tasks completed![/]");
            logger.LogInformation("Progress command completed successfully");

            return 0;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "Progress command was cancelled");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing progress command");
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            return 1;
        }
    }
}
