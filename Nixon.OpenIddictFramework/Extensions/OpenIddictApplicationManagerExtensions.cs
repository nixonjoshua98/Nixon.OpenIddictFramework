using OpenIddict.Abstractions;

namespace Nixon.OpenIddictFramework.Extensions;

public static class OpenIddictApplicationManagerExtensions
{
    public static async Task<IOpenIddictApplicationManager> CreateOrUpdateAsync(
        this IOpenIddictApplicationManager manager,
        OpenIddictApplicationDescriptor descriptor,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(descriptor.ClientId, "ClientId");

        var byClientIdAsync = await manager.FindByClientIdAsync(descriptor.ClientId, cancellationToken);

        if (byClientIdAsync != null)
        {
            await manager.UpdateAsync(byClientIdAsync, descriptor, cancellationToken);
        }
        else
        {
            await manager.CreateAsync(descriptor, cancellationToken);
        }

        return manager;
    }
}