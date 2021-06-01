

namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Decorators;
    using Microsoft.Azure.Batch;
    using Microsoft.Azure.Batch.Common;
    using PoolAggregate = Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;

    internal class NodePoolConcurrencyExtractor
    {
        private readonly CloudPool cloudPool;
        private readonly INodePool pool;

        public NodePoolConcurrencyExtractor(CloudPool cloudPool, INodePool pool)
        {
            this.cloudPool = cloudPool;
            this.pool = pool;
        }

        public void Extract()
        {
            PoolAggregate.TaskSchedulingPolicy policy;
            if (cloudPool.TaskSchedulingPolicy?.ComputeNodeFillType == ComputeNodeFillType.Pack)
            {
                policy = new PackSchedulingPolicy();
            }
            else
            {
                policy = new SpreadSchedulingPolicy();
            }

            new TurnsOnConcurrency(pool, policy, (uint)cloudPool.TaskSlotsPerNode.Value);
        }
    }
}
