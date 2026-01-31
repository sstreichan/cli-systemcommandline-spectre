namespace SystemCommandLineSpectre.Console.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using SystemCommandLineSpectre.Console.Commands;

/// <summary>
/// Provides extension methods for configuring dependency injection containers.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all command handlers to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when services is null.</exception>
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Register command factory (singleton for better performance)
        services.AddSingleton<ICommandFactory, CommandFactory>();

        // Register command handlers (scoped for per-command isolation)
        services.AddScoped<IInfoCommandHandler, InfoCommandHandler>();
        services.AddScoped<CommandHandler<ProgressCommandOptions>, ProgressCommandHandler>();

        return services;
    }
}
