
namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Microsoft.Azure.Batch;

    internal class CloudPoolCreator
    {
        private readonly INodePool pool;
        private readonly BatchClient client;

        public CloudPoolCreator(INodePool pool, BatchClient client)
        {
            this.pool = pool;
            this.client = client;
        }

        public CloudPool Create()
        {
            var cloudPool = new CloudPoolExtractor(pool, this.client).Extract();

            if (pool.PackageReferences is not null)
            {
                new CloudPackageReferenceExtractor(cloudPool, pool.PackageReferences).Extract();
            }

            if (pool is ConcurrentNodePool concurrent)
            {
                new CloudPoolConcurrencyExtractor(cloudPool, concurrent).Extract();
            }

            if (pool is ConcurrentNodePoolWithStartTask concurrentTask)
            {
                new CloudPoolStartTaskExtractor(cloudPool, concurrentTask.StartTask).Extract();
            }

            if (pool is NodePoolWithStartTask nodeTask)
            {
                new CloudPoolStartTaskExtractor(cloudPool, nodeTask.StartTask).Extract();
            }
            return cloudPool;
        }
    }
}
