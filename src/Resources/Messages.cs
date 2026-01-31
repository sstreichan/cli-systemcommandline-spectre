namespace SystemCommandLineSpectre.Console.Resources;

/// <summary>
/// Centralized messages for the application.
/// All messages are compile-time constants for AOT compatibility.
/// </summary>
/// <remarks>
/// This class uses nested static classes to organize messages by category:
/// <list type="bullet">
/// <item><see cref="Log"/> - Structured logging messages for ILogger</item>
/// <item><see cref="Error"/> - Error messages for exceptions and validation</item>
/// <item><see cref="Ui"/> - User interface messages for Spectre.Console output</item>
/// </list>
/// All strings are const fields to ensure zero runtime overhead and full AOT compatibility.
/// </remarks>
public static class Messages
{
    /// <summary>
    /// Structured logging messages for use with ILogger.
    /// </summary>
    /// <remarks>
    /// Messages may contain format placeholders (e.g., {0}, {1}) for structured logging.
    /// Use with logger.LogInformation(Messages.Log.SomeMessage, param1, param2).
    /// </remarks>
    public static class Log
    {
        /// <summary>
        /// Log message when info command execution starts.
        /// </summary>
        public const string InfoCommand_Executing = "Executing info command";
        
        /// <summary>
        /// Log message when info command completes successfully.
        /// </summary>
        public const string InfoCommand_Completed = "Info command completed successfully";
        
        /// <summary>
        /// Log error message when info command execution fails.
        /// </summary>
        public const string InfoCommand_ExecutionError = "Error executing info command";
        
        /// <summary>
        /// Log message when progress command execution starts.
        /// Format: {0} = duration in seconds.
        /// </summary>
        public const string ProgressCommand_Executing = "Executing progress command with duration {0}s";
        
        /// <summary>
        /// Log message when progress command completes successfully.
        /// </summary>
        public const string ProgressCommand_Completed = "Progress command completed successfully";
        
        /// <summary>
        /// Log message when progress duration is set.
        /// Format: {0} = duration in seconds.
        /// </summary>
        public const string ProgressCommand_DurationSet = "Progress duration set to {0} seconds";
        
        /// <summary>
        /// Log warning when progress command is cancelled.
        /// </summary>
        public const string ProgressCommand_Cancelled = "Progress command was cancelled";
        
        /// <summary>
        /// Log error message when progress command execution fails.
        /// </summary>
        public const string ProgressCommand_ExecutionError = "Error executing progress command";
    }
    
    /// <summary>
    /// Error messages for exceptions and validation failures.
    /// </summary>
    /// <remarks>
    /// These messages are used in exception constructors and validation logic.
    /// They should be clear, actionable, and user-friendly.
    /// </remarks>
    public static class Error
    {
        /// <summary>
        /// Error message when progress duration is invalid (used in exceptions).
        /// </summary>
        public const string ProgressCommand_InvalidDuration = "Duration must be greater than 0.";
        
        /// <summary>
        /// Error message displayed to user when progress duration validation fails.
        /// </summary>
        public const string ProgressCommand_DurationOutOfRange = "Error: Duration must be greater than 0 seconds.";
        
        /// <summary>
        /// Error message when an operation is cancelled by the user.
        /// </summary>
        public const string Command_OperationCancelled = "Operation cancelled by user.";
        
        /// <summary>
        /// Error message when info command is cancelled.
        /// </summary>
        public const string InfoCommand_Cancelled = "Info command cancelled.";
        
        /// <summary>
        /// Error message when progress command is cancelled.
        /// </summary>
        public const string ProgressCommand_Cancelled = "Progress command cancelled.";
        
        /// <summary>
        /// Generic command execution error message.
        /// Format: {0} = exception message.
        /// </summary>
        public const string Command_ExecutionError = "Error: {0}";
        
        /// <summary>
        /// Invalid argument error message.
        /// Format: {0} = exception message.
        /// </summary>
        public const string Command_InvalidArgument = "Invalid argument: {0}";
    }
    
    /// <summary>
    /// User interface messages for Spectre.Console output.
    /// </summary>
    /// <remarks>
    /// These messages are displayed directly to the user in the terminal.
    /// They may include markup for Spectre.Console formatting (e.g., [bold], [green]).
    /// </remarks>
    public static class Ui
    {
        /// <summary>
        /// Header text for system info panel (includes emoji).
        /// </summary>
        public const string InfoPanel_Header = "📊 System Info";
        
        /// <summary>
        /// Title for system information panel.
        /// </summary>
        public const string InfoPanel_Title = "System Information";
        
        /// <summary>
        /// Label for operating system in info panel.
        /// </summary>
        public const string InfoPanel_OS = "OS:";
        
        /// <summary>
        /// Label for runtime version in info panel.
        /// </summary>
        public const string InfoPanel_Runtime = "Runtime:";
        
        /// <summary>
        /// Label for machine name in info panel.
        /// </summary>
        public const string InfoPanel_Machine = "Machine:";
        
        /// <summary>
        /// Label for username in info panel.
        /// </summary>
        public const string InfoPanel_User = "User:";
        
        /// <summary>
        /// Label for 64-bit status in info panel.
        /// </summary>
        public const string InfoPanel_64Bit = "64-bit:";
        
        /// <summary>
        /// Label for processor count in info panel.
        /// </summary>
        public const string InfoPanel_Processors = "Processors:";
        
        /// <summary>
        /// Completion message for all tasks (includes checkmark).
        /// </summary>
        public const string Progress_AllTasksCompleted = "✓ All tasks completed!";
        
        /// <summary>
        /// Progress task description for file processing (includes markup).
        /// </summary>
        public const string Progress_TaskProcessingFiles = "[green]Processing files[/]";
        
        /// <summary>
        /// Progress task description for data downloading (includes markup).
        /// </summary>
        public const string Progress_TaskDownloadingData = "[yellow]Downloading data[/]";
        
        /// <summary>
        /// Progress task description for cache building (includes markup).
        /// </summary>
        public const string Progress_TaskBuildingCache = "[cyan]Building cache[/]";
        
        /// <summary>
        /// Message prompting user to continue (includes markup).
        /// </summary>
        public const string Common_PressKeyToContinue = "[dim]Press any key to continue...[/]";
        
        /// <summary>
        /// Fatal error message format (includes markup).
        /// Format: {0} = exception message.
        /// </summary>
        public const string Common_FatalError = "[red]Fatal error: {0}[/]";
        
        /// <summary>
        /// Operation cancelled message (includes markup).
        /// </summary>
        public const string Common_OperationCancelled = "[yellow]Operation cancelled by user.[/]";
    }
}
