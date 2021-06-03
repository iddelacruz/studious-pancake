namespace Infrastructure.Data.MainBoundedContext.BatchModule
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Data.Seedwork;
    using Microsoft.Extensions.Configuration;

    public class AzureKeyVaultCredentialProvider : ICredentialProvider
    {
        private readonly IConfiguration configuration;

        public AzureKeyVaultCredentialProvider(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetSecret(string secretId)
        {
            if (string.IsNullOrWhiteSpace(secretId))
            {
                throw new ArgumentNullException(nameof(secretId));
            }
            return this.configuration[secretId];
        }
    }
}
