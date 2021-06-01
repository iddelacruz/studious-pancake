
namespace Infrastructure.Data.MainBoundedContext.BatchModule.Jobs
{
    using System;
    using System.Collections.Generic;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;
    using Microsoft.Azure.Batch;

    internal class CloudJobPreparationTaskExtractor
    {
        private readonly CloudJob cloudJob;
        private readonly PreparationTask preparationTask;

        public CloudJobPreparationTaskExtractor(CloudJob cloudJob, PreparationTask preparationTask)
        {
            this.cloudJob = cloudJob;
            this.preparationTask = preparationTask;
        }

        public void Extract()
        {
            
            cloudJob.JobPreparationTask = new JobPreparationTask
            {
                Id = preparationTask.Identifier,
                CommandLine = preparationTask.Command.Value,
                RerunOnComputeNodeRebootAfterSuccess = preparationTask.ReRunOnComputeNodeRebootAfterSuccess
            };

            if (preparationTask.ResourceFile is not null)
            {
                cloudJob.JobPreparationTask.ResourceFiles = new List<ResourceFile>
                {
                    ResourceFile.FromAutoStorageContainer(
                    preparationTask.ResourceFile.AutoStorageContainerName, null, preparationTask.ResourceFile.BlobName, null)
                };
            }

            if(preparationTask.Constraints is not null)
            {
                cloudJob.JobPreparationTask.Constraints = new TaskConstraints
                {
                    MaxTaskRetryCount = preparationTask.Constraints.MaxTaskRetryCount,
                    MaxWallClockTime = preparationTask.Constraints.MaxWallClockTime?.TimeOfDay //TODO: review this
                };
            }
        }
    }
}
