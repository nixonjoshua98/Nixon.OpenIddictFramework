using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;

namespace Nixon.OpenIddictFramework.Extensions;

public static class OpenIddictExtensions
{
    extension(OpenIddictServerBuilder builder)
    {
        public OpenIddictServerBuilder AllowRefreshTokenFlow(TimeSpan refreshTokenLifetime)
        {
            return builder
                .AllowRefreshTokenFlow()
                .SetRefreshTokenLifetime(refreshTokenLifetime);
        }
        
        public OpenIddictServerBuilder AllowCustomFlows(IEnumerable<string> customFlows)
        {
            foreach (var customFlow in customFlows)
            {
                builder.AllowCustomFlow(customFlow);
            }
            
            return builder;
        }
    }
    
    extension(OpenIddictRequest request)
    {
        public bool TryGetParameter<T>(string name, [NotNullWhen(true)] out T? value) 
            where T : IParsable<T>
        {
            return TryGetParameter(request, name, CultureInfo.InvariantCulture, out value);
        }
        
        public bool TryGetParameterAsString(string name, [NotNullWhen(true)] out string? value)
        {
            value = null;

            if (!request.TryGetParameter(name, out var param))
            {
                return false;
            }

            value = param.ToString();

            return !string.IsNullOrEmpty(value);
        }
        
        public bool TryGetParameter<T>(string name, IFormatProvider provider, [NotNullWhen(true)] out T? value) 
            where T : IParsable<T>
        {
            ArgumentNullException.ThrowIfNull(request);
            
            value = default;

            return TryGetParameterAsString(request, name, out var str) && T.TryParse(str, provider, out value);
        }
    }
    
    extension(OpenIddictClientAspNetCoreBuilder builder)
    {
        public OpenIddictClientAspNetCoreBuilder DisableDevelopmentTransportSecurityRequirement(
            IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                builder.DisableTransportSecurityRequirement();
            }

            return builder;
        }
    }
    
    extension(IOpenIddictApplicationManager manager)
    {
        public async Task<IOpenIddictApplicationManager> CreateOrUpdateAsync(
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
    
    extension(OpenIddictApplicationDescriptor descriptor)
    {
        public OpenIddictApplicationDescriptor AddRedirectUris(IEnumerable<Uri> redirectUris)
        {
            foreach (var redirectUri in redirectUris)
            {
                descriptor.RedirectUris.Add(redirectUri);
            }
            
            return descriptor;
        }

        public OpenIddictApplicationDescriptor AddRedirectUris(IEnumerable<string> redirectUris)
        {
            foreach (var redirectUri in redirectUris)
            {
                descriptor.RedirectUris.Add(new Uri(redirectUri));
            }
            
            return descriptor;
        }
    }
}