using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace SystemCommandLineSpectre.Console.Commands;

/// <summary>
/// Handler interface for the Info command.
/// Displays system information in a formatted table.
/// </summary>
public interface IInfoCommandHandler : ICommandHandler
{
}

/// <summary>
/// Implements the Info command handler.
/// Displays system information such as OS, runtime, machine name, and processor count.
/// </summary>
public class InfoCommandHandler(ILogger<InfoCommandHandler> logger) : IInfoCommandHandler
{
    /// <summary>
    /// Executes the info command and displays system information.
    /// </summary>
    /// <returns>Exit code (0 for success, 1 for failure).</returns>
    public async Task<int> ExecuteAsync()
    {
        try
        {
            logger.LogInformation(Messages.Log.InfoCommand_Executing);

            var panel = new Panel(
                new Markup(
                    $"[bold]{Messages.Ui.InfoPanel_Title}[/]\n\n" +
                    $"[yellow]{Messages.Ui.InfoPanel_OS}[/] {Environment.OSVersion}\n" +
                    $"[yellow]{Messages.Ui.InfoPanel_Runtime}[/] {Environment.Version}\n" +
                    $"[yellow]{Messages.Ui.InfoPanel_Machine}[/] {Environment.MachineName}\n" +
                    $"[yellow]{Messages.Ui.InfoPanel_User}[/] {Environment.UserName}\n" +
                    $"[yellow]{Messages.Ui.InfoPanel_64Bit}[/] {Environment.Is64BitOperatingSystem}\n" +
                    $"[yellow]{Messages.Ui.InfoPanel_Processors}[/] {Environment.ProcessorCount}"
                )
            )
            .Border(BoxBorder.Double)
            .BorderColor(Color.Cyan1)
            .Header($"[bold cyan]{Messages.Ui.InfoPanel_Header}[/]");

            AnsiConsole.Write(panel);
            logger.LogInformation(Messages.Log.InfoCommand_Completed);

            return await Task.FromResult(0);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Messages.Log.InfoCommand_ExecutionError);
            AnsiConsole.MarkupLine(string.Format(Messages.Error.Command_ExecutionError, ex.Message));
            return 1;
        }
    }
}
