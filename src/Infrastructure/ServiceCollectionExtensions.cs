namespace SystemCommandLineSpectre.Infrastructure;

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

        services.AddScoped<IInfoCommandHandler, InfoCommandHandler>();
        services.AddScoped<IProgressCommandHandler, ProgressCommandHandler>();

        return services;
    }
}
