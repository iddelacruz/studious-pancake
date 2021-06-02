
namespace Infrastructure.Data.MainBoundedContext.BatchModule.Tasks
{
    using System.Collections.Generic;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;
    using Microsoft.Azure.Batch;

    public class AzureCloudTask : CloudTask
    {
        public AzureCloudTask(string id, string commandline) : base(id, commandline)
        {
            
        }

        public static explicit operator AzureCloudTask(Task task)
        {
            var output = new AzureCloudTask(task.Identifier, task.Command.Value);

            if(task.ResourceFile is not null)
            {
                output.ResourceFiles = new List<ResourceFile>
                {
                    ResourceFile.FromAutoStorageContainer(
                    task.ResourceFile.AutoStorageContainerName, null, task.ResourceFile.BlobName, null)
                };
            }

            if(task.Constraints is not null)
            {
                var constraints = task.Constraints;
                output.Constraints = new TaskConstraints(
                    constraints.MaxWallClockTime?.TimeOfDay, constraints.RetentionTime?.TimeOfDay, task.Constraints.MaxTaskRetryCount);
            }

            if(task.PackageReference is not null)
            {
                output.ApplicationPackageReferences = new List<ApplicationPackageReference>();

                output.ApplicationPackageReferences.Add(
                    new ApplicationPackageReference
                    {
                        ApplicationId = task.PackageReference.Identifier,
                        Version = task.PackageReference.Version
                    });
            }

            return output;
        }
    }
}
