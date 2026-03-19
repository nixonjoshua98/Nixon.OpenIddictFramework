using Microsoft.Extensions.DependencyInjection;

namespace Nixon.OpenIddictFramework.Extensions;

public static class OpenIddictServerBuilderExtensions
{
    public static OpenIddictServerBuilder AllowRefreshTokenFlow(
        this OpenIddictServerBuilder builder,
        TimeSpan refreshTokenLifetime)
    {
        return builder.AllowRefreshTokenFlow().SetRefreshTokenLifetime(new TimeSpan?(refreshTokenLifetime));
    }

    public static OpenIddictServerBuilder AllowCustomFlows(
        this OpenIddictServerBuilder builder,
        IEnumerable<string> customFlows)
    {
        foreach (var customFlow in customFlows)
            builder.AllowCustomFlow(customFlow);
        return builder;
    }
}