namespace Nixon.OpenIddictFramework.Configuration;

public interface IOpenIddictFrameworkApplicationConfiguration
{
    string ClientId { get; }

    string[] AllowedGrantTypes { get; }

    IEnumerable<string> GetRedirectUris();
}