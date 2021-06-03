

namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders
{
    using System;
    using Aggregates.Resources;
    using Aggregates.Tasks.Constraints;
    using Domain.Seedwork.DataTypes;

    public sealed class ReleaseTaskBuilder
    {
        private string taskId;
        private string command;

        private string autoStorageContainerName;
        private string blobName;

        private DateTime? maxWallClockTime;
        private DateTime? retentionTime;

        /// <summary>
        /// Set <see cref="ITask"/> unique identifier.
        /// </summary>
        /// <param name="taskId"><see cref="ITask"/> unique identifier.</param>
        public ReleaseTaskBuilder ID(string taskId)
        {
            if (string.IsNullOrWhiteSpace(taskId))
            {
                throw new ArgumentNullException(nameof(taskId));
            }
            this.taskId = taskId;

            return this;
        }

        /// <summary>
        /// Set <see cref="TaskCommand"/> command.
        /// </summary>
        /// <param name="taskCommand">The <see cref="TaskCommand"/>.</param>
        public ReleaseTaskBuilder Command(string taskCommand)
        {
            if (string.IsNullOrWhiteSpace(taskCommand))
            {
                throw new ArgumentNullException(nameof(taskCommand));
            }

            this.command = taskCommand;
            return this;
        }

        /// <summary>
        /// Set the file that the batch service will download to the compute node before running the command line.
        /// </summary>
        /// <param name="autoStorageContainerName">The storage container name in the auto storage account.</param>
        /// <param name="blobName">The blob name to use when downloading blobs from a storage container.</param>
        public ReleaseTaskBuilder ResourceFile(string autoStorageContainerName, string blobName)
        {
            this.autoStorageContainerName = autoStorageContainerName;
            this.blobName = blobName;

            return this;
        }

        /// <summary>
        /// Set the maximum elapsed time that the task may run, measured from the time the task starts.
        /// </summary>
        /// <param name="maxWallClockTime">Maximum elapsed time that the task may run, measured from the time the task starts.</param>
        /// /// <param name="retentionTime">Minimum time to retain the working directory for the task on the compute node.</param>
        public ReleaseTaskBuilder TaskConstraints(DateTime? maxWallClockTime, DateTime? retentionTime)
        {
            this.maxWallClockTime = maxWallClockTime;
            this.retentionTime = retentionTime;
            return this;
        }

        /// <summary>
        /// Create a new instance of <see cref="ReleaseTask"/>
        /// </summary>
        public ReleaseTask Build()
        {
            return new ReleaseTask
            {
                Identifier = this.taskId,
                Command = new TaskCommand(this.command),
                ResourceFile = new FromAutoStorageResourceFile(this.autoStorageContainerName, this.blobName),
                Constraints = new ReleaseTaskConstraints(this.maxWallClockTime, this.retentionTime)
            };            
        }

        //TODO: faltan las variables de entorno
    }
}
