using Microsoft.Extensions.Configuration;

namespace CraveCart.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationSection SafeGetSection(this IConfiguration configuration, string key)
        {
            IConfigurationSection? configSection = configuration.GetChildren().FirstOrDefault(item => item.Key == key);
            return configSection ?? throw new ArgumentException($"The configuration section '{key}' is not defined.");
        }

        public static TConfig SafeGet<TConfig>(this IConfiguration configuration, string key)
        {
            IConfigurationSection section = SafeGetSection(configuration, key);
            return section.Get<TConfig>() ?? throw new InvalidOperationException();
        }
    }
}
