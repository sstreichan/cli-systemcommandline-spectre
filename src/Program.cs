using System.CommandLine;
using System.CommandLine.Parsing;
using Spectre.Console;

var rootCommand = new RootCommand("Demo CLI using System.CommandLine + Spectre.Console (AOT-compatible)");

// --- GREET COMMAND ---
var greetCommand = new Command("greet", "Greet someone with a fancy table");
var nameOption = new Option<string>("--name")
{
    Description = "The name to greet",
    DefaultValueFactory = _ => "World"
};
var countOption = new Option<int>("--count")
{
    Description = "Number of times to greet",
    DefaultValueFactory = _ => 1
};

greetCommand.Add(nameOption);
greetCommand.Add(countOption);

greetCommand.SetAction(parseResult =>
{
    var name = parseResult.GetValue(nameOption);
    var count = parseResult.GetValue(countOption);

    var table = new Table()
        .Border(TableBorder.Rounded)
        .AddColumn(new TableColumn("[bold yellow]Property[/]").Centered())
        .AddColumn(new TableColumn("[bold cyan]Value[/]").Centered());

    table.AddRow("Name", $"[green]{name}[/]");
    table.AddRow("Count", $"[blue]{count}[/]");
    table.AddRow("Timestamp", $"[grey]{DateTime.Now:yyyy-MM-dd HH:mm:ss}[/]");

    AnsiConsole.Write(table);

    AnsiConsole.MarkupLine("");
    for (int i = 0; i < count; i++)
    {
        AnsiConsole.MarkupLine($"[bold green]Hello, {name}![/] (#{i + 1})");
    }
});

// --- INFO COMMAND ---
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
    .Header("[bold cyan]📊 System Info[/]");

    AnsiConsole.Write(panel);
});

// --- LIST COMMAND ---
var listCommand = new Command("list", "Display a list of items");
var itemsOption = new Option<string[]>("--items")
{
    Description = "Items to display",
    AllowMultipleArgumentsPerToken = true,
    DefaultValueFactory = _ => new[] { "Apple", "Banana", "Cherry", "Date", "Elderberry" }
};

listCommand.Add(itemsOption);

listCommand.SetAction(parseResult =>
{
    var items = parseResult.GetValue(itemsOption);

    var rule = new Rule("[bold yellow]📋 Item List[/]")
    {
        Justification = Justify.Left
    };
    AnsiConsole.Write(rule);

    var list = new List<string>();
    if (items != null)
    {
        foreach (var item in items)
        {
            list.Add($"[cyan]•[/] [white]{item}[/]");
        }
    }

    AnsiConsole.MarkupLine(string.Join("\n", list));
});

// --- PROGRESS COMMAND ---
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

// Add commands to root
rootCommand.Add(greetCommand);
rootCommand.Add(infoCommand);
rootCommand.Add(listCommand);
rootCommand.Add(progressCommand);

return rootCommand.Parse(args).Invoke();
