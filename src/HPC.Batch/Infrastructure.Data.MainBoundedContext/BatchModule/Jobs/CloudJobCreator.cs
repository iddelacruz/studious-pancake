
namespace Infrastructure.Data.MainBoundedContext.BatchModule.Jobs
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Microsoft.Azure.Batch;

    internal class CloudJobCreator
    {
        private readonly BatchClient client;
        private readonly Job entity;

        public CloudJobCreator(BatchClient client, Job entity)
        {
            this.client = client;
            this.entity = entity;
        }

        public CloudJob Create()
        {
            var cloudJob = this.client.JobOperations.CreateJob();
            cloudJob.Id = entity.Identifier;
            cloudJob.PoolInformation = new PoolInformation { PoolId = entity.Pool.Identifier };

            if (entity.Contraints is not null)
            {
                cloudJob.Constraints.MaxTaskRetryCount = entity.Contraints.MaxTaskRetryCount;
                cloudJob.Constraints.MaxWallClockTime = entity.Contraints.MaxWallClockTime?.TimeOfDay;
            }

            if (entity.TaskFailedAction is not null)
            {
                if (entity.TaskFailedAction is NoActionWhenTaskFails)
                {
                    cloudJob.OnTaskFailure = Microsoft.Azure.Batch.Common.OnTaskFailure.NoAction;
                }
                else
                {
                    cloudJob.OnTaskFailure = Microsoft.Azure.Batch.Common.OnTaskFailure.PerformExitOptionsJobAction;
                }
            }

            if (entity is JobWithPreparationTask preparation)
            {
                new CloudJobPreparationTaskExtractor(cloudJob, preparation.PreparationTask).Extract();                
            }

            if (entity is JobWithReleaseTask release)
            {
                new CloudJobReleaseTaskExtractor(cloudJob, release.ReleaseTask);   
            }

            return cloudJob;
        }
    }
}
