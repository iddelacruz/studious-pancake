namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks
{
    using System.Collections.Generic;
    using Aggregates.Resources;
    using Aggregates.Tasks.Constraints;
    using Domain.Seedwork.DataTypes;
    using ValueTypes;

    /// <summary>
    /// A task which is run when a compute node joins a pool in the Azure Batch service, or when the compute node is rebooted or reimaged.
    /// </summary>
    public sealed class StartTask : ITask
    {
        /// <summary>
        /// Get the <see cref="ITask"/> unique identifier.
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// The <see cref="TaskCommand"/> that the <see cref="ITask"/> will run.
        /// </summary>
        public TaskCommand Command { get; internal set; }

        /// <summary>
        /// Gets a list of files that the batch service will download to the compute node before running the command line.
        /// </summary>
        public FromAutoStorageResourceFile ResourceFile { get; internal set; }

        /// <summary>
        /// Gets a set of environment settings for the start task. 
        /// </summary>
        public IEnumerable<EnvironmentVariable> EnvironmentVariables { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="StartTask"/> constraints.
        /// </summary>
        public StartTaskConstraints Constraints { get; internal set; }


        internal StartTask()
        {

        }
    }
}
