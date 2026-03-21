using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Nixon.OpenIddictFramework.Configuration;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public class OpenIddictFrameworkConfiguration :
    OpenIddictFrameworkConfiguration<OpenIddictFrameworkApplicationConfiguration>
{
}

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public class OpenIddictFrameworkConfiguration<TApplication> : IOpenIddictFrameworkConfiguration
    where TApplication : OpenIddictFrameworkApplicationConfiguration
{
    public string Issuer { get; init; } = null!;
    public string EncryptionKey { get; init; } = null!;

    public TApplication[] Applications { get; init; } = [];

    IEnumerable<IOpenIddictFrameworkApplicationConfiguration> IOpenIddictFrameworkConfiguration.Applications => 
        Applications;

    public SecurityKey EncryptionSecurityKey =>
        field ??= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EncryptionKey));

    public virtual IEnumerable<string> GetGrantTypes()
    {
        return Applications.SelectMany(x => x.AllowedGrantTypes).Distinct();
    }

    public virtual IEnumerable<string> GetRedirectUris(IOpenIddictFrameworkApplicationConfiguration application)
    {
        return application.GetRedirectUris();
    }

    public virtual void Validate()
    {
        ArgumentNullException.ThrowIfNull(Issuer);
        
        ArgumentNullException.ThrowIfNull(EncryptionKey);
        
        ArgumentNullException.ThrowIfNull(Issuer);
    }
}