namespace Infrastructure.Data.MainBoundedContext.BatchModule
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Data.Seedwork;

    public class AzureKeyVaultCredentialProvider : ICredentialProvider
    {
        //private KeyVaultClient kvClient;

        //public AzureKeyVaultCredentialProvider()
        //{
        //    var clientId = Environment.GetEnvironmentVariable("akvClientId");
        //    var clientSecret = Environment.GetEnvironmentVariable("akvClientSecret");
        //    this.kvClient = Authenticate(clientId, clientSecret);
        //}

        //private static KeyVaultClient Authenticate(string clientId, string clientSecret)
        //{
        //    return new KeyVaultClient(async (authority, resource, scope) =>
        //    {
        //        var adCredential = new ClientCredential(clientId, clientSecret);
        //        var authenticationContext = new AuthenticationContext(authority, null);
        //        return (await authenticationContext.AcquireTokenAsync(resource, adCredential)).AccessToken;
        //    });
        //}

        public async Task<string> GetSecretAsync(string secretId)
        {
            try
            {
                //TODO: connect with azure key vault
                //var result = await kvClient.GetSecretAsync(secretId);
                return "";//result.Value;
            }
            catch
            {
                //TODO: capture azure exceptions and raise architecture custom exceptions
                throw;
            }
        }
    }
}
