using Microsoft.IdentityModel.Tokens;

namespace Nixon.OpenIddictFramework.Configuration;

public interface IOpenIddictFrameworkConfiguration
{
    string Issuer { get; }
    
    IEnumerable<IOpenIddictFrameworkApplicationConfiguration> Applications { get; }

    SecurityKey EncryptionSecurityKey { get; }

    IEnumerable<string> GetRedirectUris(IOpenIddictFrameworkApplicationConfiguration application);

    IEnumerable<string> GetGrantTypes();

    void Validate();
}