
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders
{
    using System;
    using Aggregates.Resources;
    using Aggregates.Tasks.Constraints;
    using Domain.Seedwork.DataTypes;

    public class StartTaskBuilder
    {
        private string taskId;
        private string command;

        private string autoStorageContainerName;
        private string blobName;

        private int maxTaskRetryCount;
        private bool waitForSuccess;

        /// <summary>
        /// Set <see cref="ITask"/> unique identifier.
        /// </summary>
        /// <param name="taskId"><see cref="ITask"/> unique identifier.</param>
        public StartTaskBuilder ID(string taskId)
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
        /// <returns></returns>
        public StartTaskBuilder TaskCommand(string taskCommand)
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
        public StartTaskBuilder ResourceFile(string autoStorageContainerName, string blobName)
        {
            this.autoStorageContainerName = autoStorageContainerName;
            this.blobName = blobName;

            return this;
        }

        /// <summary>
        /// Set the maximum number of times the task may be retried.
        /// </summary>
        /// <param name="maxTaskRetryCount">The maximum number of times the task may be retried.</param>
        /// <remarks>
        /// If the maximum retry count is 0, the batch service does not retry the task.
        /// If the maximum retry count is -1, the batch service retries the task without limit.
        /// </remarks>
        public StartTaskBuilder MaxTaskRetryCount(int maxTaskRetryCount)
        {
            this.maxTaskRetryCount = maxTaskRetryCount;
            return this;
        }

        /// <summary>
        /// Sets if the batch service should wait for the task to complete successfully.
        /// </summary>
        /// <param name="waitForSuccess">Specifies whether the Batch Service should wait for the task to complete successfully.</param>
        /// <remarks>
        /// Specifies whether the Batch Service should wait for the task to complete successfully
        /// (that is, to exit with exit code 0) before scheduling any tasks on the compute node.
        /// </remarks>
        public StartTaskBuilder WaitForSuccess(bool waitForSuccess)
        {
            this.waitForSuccess = waitForSuccess;
            return this;
        }

        /// <summary>
        /// Create a new instance of <see cref="StartTask"/>
        /// </summary>
        public StartTask Build()
        {
            return new StartTask
            {
                Identifier = this.taskId,
                Command = new TaskCommand(this.command),
                ResourceFile = new FromAutoStorageResourceFile(this.autoStorageContainerName, this.blobName),
                Constraints = new StartTaskConstraints(this.maxTaskRetryCount, this.waitForSuccess)
            };
        }
    }
}
