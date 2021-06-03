namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Decorators
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;

    public sealed class TurnsOnConcurrency : NodePoolDecorator
    {
        private readonly uint taskSlotsPerNode;
        private readonly TaskSchedulingPolicy policy;

        public TurnsOnConcurrency(INodePool nodePool, TaskSchedulingPolicy policy, uint? taskSlotsPerNode) : base(nodePool)
        {
            this.policy = policy ?? throw new ArgumentNullException(nameof(policy));
            this.taskSlotsPerNode = taskSlotsPerNode ?? 0;
        }

        public override INodePool Apply()
        {
            if (this.nodePool is NodePoolWithStartTask other)
            {
                var casted = new ConcurrentNodePoolWithStartTask(
                    other.Identifier,
                    taskSlotsPerNode,
                    this.policy,
                    other.poolRepository,
                    other.jobRepository,
                    other.StartTask)
                {
                    Details = other.Details,
                    Nodes = other.Nodes,
                    PackageReferences = other.PackageReferences,
                };

                this.nodePool = casted;
            }
            else
            {
                var simplePool = (NodePool)this.nodePool;

                var casted = new ConcurrentNodePool(
                    this.Identifier,
                    taskSlotsPerNode,
                    policy,
                    simplePool.poolRepository,
                    simplePool.jobRepository)
                {
                    Details = this.nodePool.Details,
                    Nodes = this.nodePool.Nodes,
                    PackageReferences = this.nodePool.PackageReferences,
                };

                this.nodePool = casted;
            }
            return this.nodePool;
        }
    }
}
