
namespace Infrastructure.Data.MainBoundedContext.BatchModule.Jobs
{
    using System.Collections.Generic;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;
    using Microsoft.Azure.Batch;

    internal class CloudJobReleaseTaskExtractor
    {
        private readonly CloudJob cloudJob;
        private readonly ReleaseTask releaseTask;

        public CloudJobReleaseTaskExtractor(CloudJob cloudJob, ReleaseTask releaseTask)
        {
            this.cloudJob = cloudJob;
            this.releaseTask = releaseTask;
        }

        public void Extract()
        {
            cloudJob.JobReleaseTask = new JobReleaseTask
            {
                Id = releaseTask.Identifier,
                CommandLine = releaseTask.Command.Value
            };

            if (releaseTask.ResourceFile is not null)
            {
                cloudJob.JobReleaseTask.ResourceFiles = new List<ResourceFile>
                {
                    ResourceFile.FromAutoStorageContainer(
                    releaseTask.ResourceFile.AutoStorageContainerName, null, releaseTask.ResourceFile.BlobName, null)
                };
            }

            if (releaseTask.Constraints is not null)
            {
                cloudJob.JobReleaseTask.RetentionTime = releaseTask.Constraints.RetentionTime?.TimeOfDay;
                cloudJob.JobReleaseTask.MaxWallClockTime = releaseTask.Constraints.MaxWallClockTime?.TimeOfDay;
            }
        }
    }
}
