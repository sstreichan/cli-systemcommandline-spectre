namespace SystemCommandLineSpectre.Console.Infrastructure;

/// <summary>
/// Base class for command options with validation support.
/// Provides a contract for strongly-typed command parameters.
/// </summary>
/// <remarks>
/// All command options classes should inherit from this base class
/// and override the <see cref="Validate"/> method for custom validation logic.
/// This ensures consistent validation patterns across all commands.
/// </remarks>
/// <example>
/// <code>
/// public class MyCommandOptions : CommandOptions
/// {
///     public int Count { get; init; }
///     
///     public override void Validate()
///     {
///         if (Count &lt;= 0)
///             throw new ArgumentOutOfRangeException(nameof(Count), "Count must be positive");
///     }
/// }
/// </code>
/// </example>
public abstract class CommandOptions
{
    /// <summary>
    /// Validates the options. Override to add custom validation logic.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when a value is out of valid range.</exception>
    /// <remarks>
    /// This method is called automatically by <see cref="CommandHandler{TOptions}.SetOptions"/>
    /// before the options are stored. The base implementation does nothing, allowing
    /// options classes without validation requirements to skip the override.
    /// </remarks>
    public virtual void Validate() { }
}
