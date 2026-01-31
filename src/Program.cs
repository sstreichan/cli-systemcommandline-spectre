using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using SystemCommandLineSpectre.Commands;
using SystemCommandLineSpectre.Infrastructure;

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
    
    var rootCommand = new RootCommand("Demo CLI using System.CommandLine + Spectre.Console (AOT-compatible)")
    {
        TreatUnmatchedTokensAsErrors = true
    };

    // Add commands to root - using primary constructor syntax with DI
    rootCommand.Add(InfoCommand.Create(serviceProvider.GetRequiredService<IInfoCommandHandler>()));
    rootCommand.Add(ProgressCommand.Create(serviceProvider.GetRequiredService<IProgressCommandHandler>()));

    var exitCode = rootCommand.Parse(args).Invoke();

    AnsiConsole.WriteLine("\n[dim]Press any key to continue...[/]");
    Console.ReadKey(true);

    return exitCode;
}
catch (OperationCanceledException)
{
    AnsiConsole.MarkupLine("[yellow]Operation cancelled by user.[/]");
    return 1;
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]Fatal error: {ex.Message}[/]");
    return 1;
}
