namespace SystemCommandLineSpectre.Commands;

using Microsoft.Extensions.Logging;
using Spectre.Console;
using SystemCommandLineSpectre.Infrastructure;

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
            logger.LogInformation("Executing info command");

            var panel = new Panel(
                new Markup(
                    $"[bold]System Information[/]\n\n" +
                    $"[yellow]OS:[/] {Environment.OSVersion}\n" +
                    $"[yellow]Runtime:[/] {Environment.Version}\n" +
                    $"[yellow]Machine:[/] {Environment.MachineName}\n" +
                    $"[yellow]User:[/] {Environment.UserName}\n" +
                    $"[yellow]64-bit:[/] {Environment.Is64BitOperatingSystem}\n" +
                    $"[yellow]Processors:[/] {Environment.ProcessorCount}"
                )
            )
            .Border(BoxBorder.Double)
            .BorderColor(Color.Cyan1)
            .Header("[bold cyan]📊 System Info [/]");

            AnsiConsole.Write(panel);
            logger.LogInformation("Info command completed successfully");

            return await Task.FromResult(0);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing info command");
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            return 1;
        }
    }
}
