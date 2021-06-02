
namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Microsoft.Azure.Batch;
    using Microsoft.Azure.Batch.Common;

    internal class CloudPoolConcurrencyExtractor
    {
        private readonly CloudPool cloudPool;
        private readonly ConcurrentNodePool pool;

        public CloudPoolConcurrencyExtractor(CloudPool cloudPool, ConcurrentNodePool pool)
        {
            this.cloudPool = cloudPool;
            this.pool = pool;
        }

        public void Extract()
        {
            cloudPool.TaskSlotsPerNode = (int)pool.TaskSlotsPerNode;

            if (pool.Policy is PackSchedulingPolicy)
            {
                cloudPool.TaskSchedulingPolicy = new Microsoft.Azure.Batch.TaskSchedulingPolicy(ComputeNodeFillType.Pack);
            }
            else
            {
                cloudPool.TaskSchedulingPolicy = new Microsoft.Azure.Batch.TaskSchedulingPolicy(ComputeNodeFillType.Spread);
            }
        }
    }
}
