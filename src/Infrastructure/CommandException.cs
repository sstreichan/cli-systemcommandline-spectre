namespace SystemCommandLineSpectre.Console.Infrastructure;

/// <summary>
/// Custom exception for command execution errors.
/// Used to distinguish command-specific failures from system errors.
/// </summary>
public class CommandException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandException"/> class.
    /// </summary>
    /// <param name="message">The error message describing the command failure.</param>
    /// <exception cref="ArgumentNullException">Thrown when message is null.</exception>
    public CommandException(string message) 
        : base(message ?? throw new ArgumentNullException(nameof(message)))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandException"/> class with an inner exception.
    /// </summary>
    /// <param name="message">The error message describing the command failure.</param>
    /// <param name="innerException">The inner exception that caused this exception.</param>
    /// <exception cref="ArgumentNullException">Thrown when message or innerException is null.</exception>
    public CommandException(string message, Exception innerException) 
        : base(
            message ?? throw new ArgumentNullException(nameof(message)), 
            innerException ?? throw new ArgumentNullException(nameof(innerException)))
    {
    }
}
