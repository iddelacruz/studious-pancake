
namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Decorators
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    public sealed class AddStartTask : NodePoolDecorator
    {
        private readonly StartTask startTask;

        public AddStartTask(INodePool nodePool, StartTask startTask) : base(nodePool)
        {
            this.startTask = startTask;
        }

        public override INodePool Apply()
        {
            if(this.startTask is not null)
            {
                if (this.nodePool is ConcurrentNodePool other)
                {
                    //TODO: fill object
                    this.nodePool = new ConcurrentNodePoolWithStartTask(
                        this.Identifier,
                        other.TaskSlotsPerNode,
                        other.Policy, other.poolRepository, other.jobRepository, this.startTask);
                }
                else
                {
                    //TODO: fill object
                    var simplePool = (NodePool)this.nodePool;
                    var pool = new NodePoolWithStartTask(this.Identifier, simplePool.poolRepository, simplePool.jobRepository, this.startTask);
                    this.nodePool = pool;
                }
            }            
            return this.nodePool;
        }
    }
}
