
namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using System.Collections.Generic;
    using Microsoft.Azure.Batch;

    using TasksAggregate = Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    internal class CloudPoolStartTaskExtractor
    {
        private readonly CloudPool cloudPool;
        private readonly TasksAggregate.StartTask task;

        public CloudPoolStartTaskExtractor(CloudPool cloudPool, TasksAggregate.StartTask task)
        {
            this.cloudPool = cloudPool;
            this.task = task;
        }

        public void Extract()
        {
            cloudPool.StartTask = new StartTask
            {
                CommandLine = task.Command.Value,
                MaxTaskRetryCount = task.Constraints.MaxTaskRetryCount,
                ResourceFiles = new List<ResourceFile>
                {
                    ResourceFile.FromAutoStorageContainer(
                        task.ResourceFile.AutoStorageContainerName, null, task.ResourceFile.BlobName)
                }
            };
        }
    }
}
