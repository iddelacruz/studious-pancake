namespace Infrastructure.Data.MainBoundedContext.BatchModule
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Data.Seedwork;
    using Microsoft.Azure.Batch;
    using Microsoft.Azure.Batch.Auth;

    //singleton
    public class AzureBatchClient : IBatchClient<BatchClient>
    {
        private BatchSharedKeyCredentials credentials;

        /// <summary>
        /// Create a new instance of <see cref="AzureBatchClient"/>
        /// </summary>
        /// <param name="provider">The credentials provider.</param>
        public AzureBatchClient(ICredentialProvider provider)
        {
            if(provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.GetSecretsFromKeyVault(provider)
                .GetAwaiter()
                .GetResult();
        }

        public BatchClient GiveMeTheClient()
        {
            try
            {
                //TODO: put retry options
                return BatchClient.Open(this.credentials);
            }
            catch
            {
                //TODO: capture azure exceptions and raise architecture custom exceptions
                throw;
            }            
        }

        private async Task GetSecretsFromKeyVault(ICredentialProvider provider)
        {
            try
            {
                var batchServiceUrl = await provider.GetSecretAsync("batchServiceUrl");
                var batchAccountName = await provider.GetSecretAsync("batchAccountName");
                var batchAccountKey = await provider.GetSecretAsync("batchAccountKey");

                this.credentials = new BatchSharedKeyCredentials(batchServiceUrl, batchAccountName, batchAccountKey);
            }
            catch
            {
                //TODO: capture azure exceptions and raise architecture custom exceptions
                throw;
            }

        }

        #region disposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
              
            }
        }
        #endregion
    }
}