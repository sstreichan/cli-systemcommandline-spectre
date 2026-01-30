using System.CommandLine;
using Spectre.Console;

namespace SystemCommandLineSpectre.Commands;

public static class InfoCommand
{
    public static Command Create()
    {
        var infoCommand = new Command("info", "Display system information");

        infoCommand.SetAction(_ =>
        {
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
        });

        return infoCommand;
    }
}
