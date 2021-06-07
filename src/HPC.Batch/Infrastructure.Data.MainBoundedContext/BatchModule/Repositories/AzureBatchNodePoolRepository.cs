namespace Infrastructure.Data.MainBoundedContext.BatchModule.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Infrastructure.Crosscutting.IoC;
    using Infrastructure.Data.Seedwork;
    using Microsoft.Azure.Batch;
    using Microsoft.Azure.Batch.Common;
    using NodePools;

    public sealed class AzureBatchNodePoolRepository : INodesRepository, IDisposable
    {
        private readonly BatchClient client;

        private readonly IServiceLocator provider;

        public AzureBatchNodePoolRepository(IBatchClient<BatchClient> client, IServiceLocator provider)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.client = client.GiveMeTheClient() ?? throw new ArgumentNullException(nameof(this.client));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// Add a <see cref="INodePool"/> to the batch service.
        /// </summary>
        /// <param name="item">The object to be added.</param>
        public async Task AddAsync(INodePool item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            try
            {
                var query = await this.GetIfExistAsync(item.Identifier);

                if (query is null)
                {
                    await new CloudPoolCreator(item, this.client)
                        .Create()
                        .CommitAsync();
                }
            }
            catch (BatchException ex)
            {
                var code = ex.RequestInformation?.BatchError?.Code;
                //TODO: verify if this exception occurs
                if ((code != BatchErrorCodeStrings.PoolNotFound) && (code != BatchErrorCodeStrings.PoolExists))
                {
                    //TODO: capture exceptons and raise DDD exceptions.
                    throw;
                }
            }
            catch (Exception)
            {
                //TODO: capture exceptons and raise DDD exceptions.
                throw;
            }
        }

        public Task<IEnumerable<INodePool>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a node pool from azure batch service
        /// </summary>
        /// <param name="identifier"><see cref="INodePool"/> unique identifier.</param>
        /// <returns></returns>
        public async Task<INodePool> GetAsync(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }
            
            try
            {
                INodePool pool = null;

                var cloudPool = await this.GetIfExistAsync(identifier);

                if (cloudPool is not null)
                {
                    pool =  new NodePoolCreator(cloudPool, this.provider).Create();
                }
                return pool;
            }
            catch (BatchException ex)
            {

                var code = ex.RequestInformation?.BatchError?.Code;

                //TODO: verify if this exception occurs
                if (code != BatchErrorCodeStrings.PoolNotFound)
                {
                    
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> RemoveAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        #region private methods
        private async Task<CloudPool> GetIfExistAsync(string poolId)
        {
            var detail = new ODATADetailLevel(filterClause: string.Format("id eq '{0}'", poolId));
            List<CloudPool> pools = await this.client.PoolOperations.ListPools(detailLevel: detail)
                .ToListAsync()
                .ConfigureAwait(continueOnCapturedContext: false);

            return pools.FirstOrDefault();
        }
        #endregion

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
                if (this.client != null)
                {
                    this.client.Dispose();
                }
            }
        }
        #endregion
    }
}
