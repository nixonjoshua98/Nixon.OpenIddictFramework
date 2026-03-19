using OpenIddict.Abstractions;

namespace Nixon.OpenIddictFramework.Extensions;

public static class OpenIddictApplicationDescriptorExtensions
{
    public static OpenIddictApplicationDescriptor AddRedirectUris(
        this OpenIddictApplicationDescriptor descriptor,
        IEnumerable<Uri> redirectUris)
    {
        foreach (var redirectUri in redirectUris)
            descriptor.RedirectUris.Add(redirectUri);
        return descriptor;
    }

    public static OpenIddictApplicationDescriptor AddRedirectUris(
        this OpenIddictApplicationDescriptor descriptor,
        IEnumerable<string> redirectUris)
    {
        foreach (var redirectUri in redirectUris)
            descriptor.RedirectUris.Add(new Uri(redirectUri));
        return descriptor;
    }
}