namespace SystemCommandLineSpectre.Console.Commands;

using SystemCommandLineSpectre.Console.Infrastructure;
using SystemCommandLineSpectre.Console.Resources;

/// <summary>
/// Options for the Progress command.
/// Encapsulates configuration for the progress demonstration.
/// </summary>
/// <remarks>
/// <para>
/// This class uses the init-only property pattern for immutability,
/// ensuring that options cannot be modified after construction.
/// </para>
/// <para>
/// Validation is performed in the <see cref="Validate"/> method,
/// which is automatically called by the <see cref="CommandHandler{TOptions}"/>
/// base class before the options are used.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var options = new ProgressCommandOptions { DurationSeconds = 5 };
/// handler.SetOptions(options); // Validates automatically
/// </code>
/// </example>
public sealed class ProgressCommandOptions : CommandOptions
{
    /// <summary>
    /// Gets the duration in seconds for the progress demonstration.
    /// </summary>
    /// <value>
    /// A positive integer representing the number of seconds to run the progress demo.
    /// Must be greater than 0.
    /// </value>
    /// <remarks>
    /// This property uses init-only accessor to ensure immutability after construction.
    /// The value is validated in the <see cref="Validate"/> method.
    /// </remarks>
    public int DurationSeconds { get; init; }
    
    /// <summary>
    /// Validates the progress command options.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <see cref="DurationSeconds"/> is less than or equal to 0.
    /// </exception>
    /// <remarks>
    /// <para>
    /// This method is called automatically by <see cref="CommandHandler{TOptions}.SetOptions"/>
    /// before the options are stored, ensuring all options are valid before execution.
    /// </para>
    /// <para>
    /// The validation enforces that the duration must be a positive integer,
    /// preventing invalid states and ensuring predictable behavior during command execution.
    /// </para>
    /// </remarks>
    public override void Validate()
    {
        if (DurationSeconds <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(DurationSeconds), 
                DurationSeconds,
                Messages.Error.ProgressCommand_InvalidDuration);
        }
    }
}
