using Microsoft.Extensions.Logging;
using Spectre.Console;

/// <summary>
/// Main entry point for the System.CommandLine + Spectre.Console CLI application.
/// 
/// This application demonstrates best practices for building AOT-compatible command-line interfaces
/// using System.CommandLine for argument parsing and Spectre.Console for rich terminal UI.
/// </summary>
try
{
    var serviceProvider = new ServiceCollection()
        .AddLogging(builder => builder.AddConsole())
        .AddCommandHandlers()
        .BuildServiceProvider();

    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    var commandFactory = serviceProvider.GetRequiredService<ICommandFactory>();
    
    var rootCommand = new RootCommand("Demo CLI using System.CommandLine + Spectre.Console (AOT-compatible)")
    {
        TreatUnmatchedTokensAsErrors = true
    };

    // Add commands using factory pattern
    rootCommand.Add(commandFactory.CreateInfoCommand());
    rootCommand.Add(commandFactory.CreateProgressCommand());

    var exitCode = rootCommand.Parse(args).Invoke();

    AnsiConsole.WriteLine($"\n{Messages.Ui.Common_PressKeyToContinue}");
    Console.ReadKey(true);

    return exitCode;
}
catch (OperationCanceledException)
{
    AnsiConsole.MarkupLine(Messages.Ui.Common_OperationCancelled);
    return 1;
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine(string.Format(Messages.Ui.Common_FatalError, ex.Message));
    return 1;
}
