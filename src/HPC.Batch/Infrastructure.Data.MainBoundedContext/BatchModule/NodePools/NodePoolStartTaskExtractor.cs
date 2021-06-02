

namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using System;
    using System.Linq;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Decorators;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders;
    using Microsoft.Azure.Batch;

    internal class NodePoolStartTaskExtractor
    {
        private readonly CloudPool cloudPool;
        private readonly INodePool pool;

        public NodePoolStartTaskExtractor(CloudPool cloudPool, INodePool pool)
        {
            this.cloudPool = cloudPool;
            this.pool = pool;
        }

        internal void Extract()
        {
            var taskBuilder = new StartTaskBuilder()
                .ID($"start_task_{cloudPool.Id}")
                .TaskCommand(cloudPool.StartTask.CommandLine)
                .MaxTaskRetryCount(cloudPool.StartTask.MaxTaskRetryCount ?? 0)
                .WaitForSuccess(cloudPool.StartTask.WaitForSuccess ?? false);

            if(cloudPool.StartTask.ResourceFiles is not null && cloudPool.StartTask.ResourceFiles.Any())
            {
                var resource = cloudPool.StartTask.ResourceFiles.First();
                taskBuilder.ResourceFile(resource.AutoStorageContainerName, resource.BlobPrefix);
            }

            new AddStartTask(this.pool,taskBuilder.Build());
        }
    }
}