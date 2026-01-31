namespace SystemCommandLineSpectre.Console.Infrastructure;

using Microsoft.Extensions.Logging;

/// <summary>
/// Generic base class for command handlers with strongly-typed options.
/// Implements the Command Handler pattern with dependency injection support.
/// </summary>
/// <typeparam name="TOptions">The options type for this command, must inherit from <see cref="CommandOptions"/>.</typeparam>
/// <remarks>
/// <para>
/// This base class provides:
/// <list type="bullet">
/// <item>Strongly-typed options management with automatic validation</item>
/// <item>Centralized logger access for structured logging</item>
/// <item>Consistent error handling patterns</item>
/// <item>AOT-compatible dependency injection</item>
/// </list>
/// </para>
/// <para>
/// The options are set via <see cref="SetOptions"/> which validates them before storage.
/// Handlers must implement <see cref="ExecuteAsync"/> to provide command-specific logic.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// public class MyCommandOptions : CommandOptions
/// {
///     public int Duration { get; init; }
///     
///     public override void Validate()
///     {
///         if (Duration &lt;= 0)
///             throw new ArgumentOutOfRangeException(nameof(Duration));
///     }
/// }
/// 
/// public class MyCommandHandler(ILogger&lt;MyCommandHandler&gt; logger) 
///     : CommandHandler&lt;MyCommandOptions&gt;(logger), IMyCommandHandler
/// {
///     public override async Task&lt;int&gt; ExecuteAsync()
///     {
///         Logger.LogInformation("Executing with duration: {0}", Options.Duration);
///         // Command logic here
///         return 0;
///     }
/// }
/// </code>
/// </example>
public abstract class CommandHandler<TOptions> : ICommandHandler
    where TOptions : CommandOptions
{
    /// <summary>
    /// Gets the validated options for this command.
    /// </summary>
    /// <value>
    /// The strongly-typed options instance set via <see cref="SetOptions"/>.
    /// </value>
    /// <remarks>
    /// Options are validated during <see cref="SetOptions"/> and are guaranteed
    /// to be valid before <see cref="ExecuteAsync"/> is called.
    /// Accessing this property before options are set will result in a null reference.
    /// </remarks>
    protected TOptions Options { get; private set; } = default!;
    
    /// <summary>
    /// Gets the logger instance for this command handler.
    /// </summary>
    /// <value>
    /// The ILogger instance for structured logging.
    /// </value>
    /// <remarks>
    /// Use this logger for all logging within the command handler to ensure
    /// consistent structured logging with proper category names.
    /// </remarks>
    protected ILogger Logger { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandler{TOptions}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for structured logging.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> is null.</exception>
    protected CommandHandler(ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        Logger = logger;
    }
    
    /// <summary>
    /// Sets and validates the command options.
    /// </summary>
    /// <param name="options">The options to set for this command.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="options"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when options validation fails.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an option value is out of valid range.</exception>
    /// <remarks>
    /// This method calls <see cref="CommandOptions.Validate"/> on the options instance
    /// before storing it. If validation fails, the options are not stored and an exception is thrown.
    /// This ensures that <see cref="Options"/> always contains valid data when accessed in <see cref="ExecuteAsync"/>.
    /// </remarks>
    public void SetOptions(TOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        options.Validate();
        Options = options;
    }
    
    /// <summary>
    /// Executes the command asynchronously with the configured options.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the exit code (0 for success, non-zero for failure).
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled by the user.</exception>
    /// <exception cref="CommandException">Thrown when the command execution fails.</exception>
    /// <remarks>
    /// Implementations should:
    /// <list type="bullet">
    /// <item>Return 0 for successful execution</item>
    /// <item>Return non-zero exit codes for errors</item>
    /// <item>Use <see cref="Logger"/> for all logging</item>
    /// <item>Access validated options via <see cref="Options"/> property</item>
    /// <item>Throw <see cref="OperationCanceledException"/> for cancellation</item>
    /// <item>Handle exceptions gracefully and log errors</item>
    /// </list>
    /// </remarks>
    public abstract Task<int> ExecuteAsync();
}
