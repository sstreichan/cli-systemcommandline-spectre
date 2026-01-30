using System.CommandLine;
using System.CommandLine.Parsing;
using Spectre.Console;

namespace SystemCommandLineSpectre.Commands;

public static class ProgressCommand
{
    public static Command Create()
    {
        var progressCommand = new Command("progress", "Show a progress bar demo");
        
        var durationOption = new Option<int>("--duration")
        {
            Description = "Duration in seconds",
            DefaultValueFactory = _ => 3
        };

        progressCommand.Add(durationOption);

        progressCommand.SetAction(async parseResult =>
        {
            var duration = parseResult.GetValue(durationOption);

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

                    var steps = duration * 10;
                    for (int i = 0; i < steps; i++)
                    {
                        await Task.Delay(100);
                        task1.Increment(100.0 / steps);
                        if (i > steps / 3)
                            task2.Increment(100.0 / steps);
                        if (i > steps / 2)
                            task3.Increment(100.0 / steps);
                    }
                });

            AnsiConsole.MarkupLine("[bold green]✓ All tasks completed![/]");
        });

        return progressCommand;
    }
}
