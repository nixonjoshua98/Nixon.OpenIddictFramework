using Microsoft.Extensions.Hosting;
using Nixon.OpenIddictFramework.Configuration;

namespace Nixon.OpenIddictFramework.Builders;

public sealed class OpenIddictFrameworkBuilder<TConfiguration>(
    TConfiguration configuration,
    IHostEnvironment environment)
    where TConfiguration : IOpenIddictFrameworkConfiguration
{
    public readonly TConfiguration Configuration = configuration;
    public readonly OpenIddictFrameworkServerBuilder Server = new(configuration);
    public readonly OpenIddictFrameworkClientBuilder Client = new(configuration, environment);
}