using StackExchange.Redis;

namespace HackerNewsAggregator.Extensions;

public static class Extensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton(sp =>
        {
            var redisConfig =
                ConfigurationOptions.Parse(configuration.GetRequiredConnectionString("Redis"),
                    true);

            return ConnectionMultiplexer.Connect(redisConfig);
        });
    }

    public static string GetRequiredConnectionString(this IConfiguration configuration, string name) =>
        configuration.GetConnectionString(name) ??
        throw new InvalidOperationException(
            $"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":ConnectionStrings:" + name : "ConnectionStrings:" + name)}");
}