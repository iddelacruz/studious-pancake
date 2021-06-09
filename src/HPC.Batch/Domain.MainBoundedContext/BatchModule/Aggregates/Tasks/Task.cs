namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks
{
    using System;
    using System.Collections.Generic;
    using Aggregates.Applications;
    using Aggregates.Jobs;
    using Aggregates.Resources;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints;
    using Domain.Seedwork.DataTypes;
    using ValueTypes;

    /// <summary>
    /// It represents a computation unit.
    /// </summary>
    /// https://docs.microsoft.com/en-us/azure/batch/jobs-and-tasks#tasks
    public class Task : ITask
    {
        /// <summary>
        /// Get the <see cref="ITask"/> unique identifier.
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// The <see cref="TaskCommand"/> that the <see cref="Task"/> will run.
        /// </summary>
        public TaskCommand Command { get; internal set; }

        /// <summary>
        /// Gets the file(s) that the batch service will download to the compute node before running the command line.
        /// </summary>
        public FromAutoStorageResourceFile ResourceFile { get; internal set; }

        /// <summary>
        /// Get the constraints under which the task should execute.
        /// </summary>
        public TaskConstraints Constraints { get; internal set; }

        /// <summary>
        /// Gets a list of environment variable settings for the task. 
        /// </summary>
        public IEnumerable<EnvironmentVariable> EnvironmentVariables { get; private set; }

        /// <summary>
        /// Gets an application package that the batch service will deploy to the compute node before running the command line.
        /// </summary>
        public PackageReference PackageReference { get; internal set; }

        public TaskState State { get; internal set; }

        internal Job Job { get; set; }

        internal Task()
        {

        }
    }
}
