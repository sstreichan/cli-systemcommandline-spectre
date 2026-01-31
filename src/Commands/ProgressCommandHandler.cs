using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace SystemCommandLineSpectre.Console.Commands;

/// <summary>
/// Implements the Progress command handler.
/// Displays a progress bar with multiple tasks to demonstrate Spectre.Console capabilities.
/// </summary>
public class ProgressCommandHandler(ILogger<ProgressCommandHandler> logger) 
    : CommandHandler<ProgressCommandOptions>(logger)
{
    /// <summary>
    /// Executes the progress command and displays a progress bar with multiple tasks.
    /// </summary>
    /// <returns>Exit code (0 for success, 1 for failure).</returns>
    public override async Task<int> ExecuteAsync()
    {
        try
        {
            logger.LogInformation(Messages.Log.ProgressCommand_Executing, Options.DurationSeconds);

            await AnsiConsole.Progress()
                .Columns(
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new SpinnerColumn()
                )
                .StartAsync(async ctx =>
                {
                    var task1 = ctx.AddTask(Messages.Ui.Progress_TaskProcessingFiles);
                    var task2 = ctx.AddTask(Messages.Ui.Progress_TaskDownloadingData);
                    var task3 = ctx.AddTask(Messages.Ui.Progress_TaskBuildingCache);

                    var steps = Options.DurationSeconds * 10;
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

            AnsiConsole.MarkupLine($"[bold green]{Messages.Ui.Progress_AllTasksCompleted}[/]");
            logger.LogInformation(Messages.Log.ProgressCommand_Completed);

            return 0;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, Messages.Log.ProgressCommand_Cancelled);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Messages.Log.ProgressCommand_ExecutionError);
            AnsiConsole.MarkupLine($"[red]{string.Format(Messages.Error.Command_ExecutionError, ex.Message)}[/]");
            return 1;
        }
    }
}
