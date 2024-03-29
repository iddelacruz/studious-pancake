﻿

namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Resources;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints;
    using Domain.Seedwork.DataTypes;

    public sealed class PreparationTaskBuilder
    {
        private string taskId;
        private string command;

        private string autoStorageContainerName;
        private string blobName;

        private int maxTaskRetryCount;
        private DateTime? maxWallClockTime;
        private DateTime? retentionTime;
        private bool waitForSuccess;

        private bool reRun;

        /// <summary>
        /// Set <see cref="ITask"/> unique identifier.
        /// </summary>
        /// <param name="taskId"><see cref="ITask"/> unique identifier.</param>
        public PreparationTaskBuilder ID(string taskId)
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
        public PreparationTaskBuilder Command(string taskCommand)
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
        public PreparationTaskBuilder ResourceFile(string autoStorageContainerName, string blobName)
        {
            this.autoStorageContainerName = autoStorageContainerName;
            this.blobName = blobName;

            return this;
        }

        /// <summary>
        /// Set the execution constraints that apply to this task.
        /// </summary>
        /// <param name="maxTaskRetryCount">Maximum number of times each <see cref="ITask"/> may be retried.
        /// The Batch service retries a Task if its exit code is nonzero.</param>
        /// <param name="maxWallClockTime">Maximum elapsed time that the task may run, measured from the time the task starts.</param>
        /// <param name="retentionTime">minimum time to retain the working directory for the task on the compute node where it ran,
        /// from the time it completes execution.</param>
        public PreparationTaskBuilder TaskConstraints(int maxTaskRetryCount, DateTime? maxWallClockTime, DateTime? retentionTime)
        {
            this.maxTaskRetryCount = maxTaskRetryCount;
            this.maxWallClockTime = maxWallClockTime;
            this.retentionTime = retentionTime;
            return this;
        }

        /// <summary>
        /// Sets whether the batch service should rerun the Job Preparation task after a compute node reboots.
        /// </summary>
        /// <param name="reRun">The batch service should rerun the Job Preparation task after a compute node reboots.</param>
        public PreparationTaskBuilder ReRunOnComputeNodeRebootAfterSuccess(bool reRun)
        {
            this.reRun = reRun;
            return this;
        }

        /// <summary>
        /// Determine if the batch service should wait for the successful completion of the Job Preparation task before
        /// scheduling any tasks on the compute node.
        /// </summary>
        /// <param name="wait">The batch service should wait for the successful completion of the Job Preparation task.</param>
        public PreparationTaskBuilder WaitForSuccess(bool wait)
        {
            this.waitForSuccess = wait;
            return this;
        }

        /// <summary>
        /// Create a new instance of <see cref="PreparationTask"/>
        /// </summary>
        public PreparationTask Build()
        {
            return new PreparationTask
            {
                Identifier = this.taskId,
                Command = new TaskCommand(this.command),
                ResourceFile = new FromAutoStorageResourceFile(this.autoStorageContainerName, this.blobName),
                Constraints = new  PreparationTaskConstraints(this.maxTaskRetryCount, this.maxWallClockTime, this.retentionTime, this.waitForSuccess),
                ReRunOnComputeNodeRebootAfterSuccess = this.reRun
            };
        }

        //TODO: faltan las variables de entorno
    }
}
