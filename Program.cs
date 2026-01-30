using System.CommandLine;
using Spectre.Console;

var rootCommand = new RootCommand("Demo CLI using System.CommandLine + Spectre.Console (AOT-compatible)");

// --- GREET COMMAND ---
var greetCommand = new Command("greet", "Greet someone with a fancy table");
var nameOption = new Option<string>(
    name: "--name",
    description: "The name to greet",
    getDefaultValue: () => "World"
);
var countOption = new Option<int>(
    name: "--count",
    description: "Number of times to greet",
    getDefaultValue: () => 1
);

greetCommand.AddOption(nameOption);
greetCommand.AddOption(countOption);

greetCommand.SetHandler((string name, int count) =>
{
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
}, nameOption, countOption);

// --- INFO COMMAND ---
var infoCommand = new Command("info", "Display system information");

infoCommand.SetHandler(() =>
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
var itemsOption = new Option<string[]>(
    name: "--items",
    description: "Items to display (comma-separated)"
) { AllowMultipleArgumentsPerToken = true };

listCommand.AddOption(itemsOption);

listCommand.SetHandler((string[] items) =>
{
    if (items == null || items.Length == 0)
    {
        items = new[] { "Apple", "Banana", "Cherry", "Date", "Elderberry" };
    }

    var rule = new Rule("[bold yellow]📋 Item List[/]")
    {
        Justification = Justify.Left
    };
    AnsiConsole.Write(rule);

    var list = new List<string>();
    foreach (var item in items)
    {
        list.Add($"[cyan]•[/] [white]{item}[/]");
    }

    AnsiConsole.MarkupLine(string.Join("\n", list));
}, itemsOption);

// --- PROGRESS COMMAND ---
var progressCommand = new Command("progress", "Show a progress bar demo");
var durationOption = new Option<int>(
    name: "--duration",
    description: "Duration in seconds",
    getDefaultValue: () => 3
);

progressCommand.AddOption(durationOption);

progressCommand.SetHandler(async (int duration) =>
{
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
}, durationOption);

// Add commands to root
rootCommand.AddCommand(greetCommand);
rootCommand.AddCommand(infoCommand);
rootCommand.AddCommand(listCommand);
rootCommand.AddCommand(progressCommand);

return await rootCommand.InvokeAsync(args);