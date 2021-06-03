namespace Infrastructure.Data.MainBoundedContext.BatchModule
{
    using Infrastructure.Data.Seedwork;
    using Microsoft.Azure.Batch;
    using Microsoft.Azure.Batch.Auth;
    using System;

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

            this.GetSecretsFromVault(provider);
        }

        public BatchClient GiveMeTheClient()
        {
            try
            {
                var client = BatchClient.Open(this.credentials);
                client.CustomBehaviors.Add(RetryPolicyProvider.ExponentialRetryProvider(TimeSpan.FromSeconds(5), 3));
                return client;
            }
            catch
            {
                //TODO: capture azure exceptions and raise architecture custom exceptions
                throw;
            }            
        }

        private void GetSecretsFromVault(ICredentialProvider provider)
        {
            try
            {
                var batchServiceUrl = provider.GetSecret("BatchServiceUrl");
                var batchAccountName = provider.GetSecret("BatchAccountName");
                var batchAccountKey = provider.GetSecret("BatchAccountKey");

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