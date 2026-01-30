using System.CommandLine;
using SystemCommandLineSpectre.Commands;

var rootCommand = new RootCommand("Demo CLI using System.CommandLine + Spectre.Console (AOT-compatible)");

// Add commands to root
rootCommand.Add(InfoCommand.Create());
rootCommand.Add(ProgressCommand.Create());

var exitCode = rootCommand.Parse(args).Invoke();

Console.WriteLine("\nPress any key to continue...");
Console.ReadKey(true);

return exitCode;
