namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks
{
    using System;
    using System.Collections.Generic;
    using Aggregates.Resources;
    using Aggregates.Tasks.Constraints;
    using Domain.Seedwork.DataTypes;
    using ValueTypes;

    /// <summary>
    /// The Job Release Task runs when the Job ends, because of one of the following:
    /// The user calls the Terminate Job API, or the Delete Job API while the Job is still active,
    /// the Job's maximum wall clock time constraint is reached, and the Job is still active,
    /// or the Job's Job Manager Task completed, and the Job is configured to terminate when the Job Manager completes. 
    /// </summary>
    public class ReleaseTask : ITask
    {
        /// <summary>
        /// Get the <see cref="ITask"/> unique identifier.
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// Gets the <see cref="TaskCommand"/> that the <see cref="ITask"/> will run.
        /// </summary>
        public TaskCommand Command { get; internal set; }

        /// <summary>
        /// Gets the file that the Batch service will download to the compute node before running the command line.
        /// </summary>
        public FromAutoStorageResourceFile ResourceFile { get; internal set; }

        /// <summary>
        /// Get The environment variables that are required by your application. 
        /// </summary>
        public IEnumerable<EnvironmentVariable> EnvironmentVariables { get; private set; }

        /// <summary>
        /// Get the constraints under which the task should execute.
        /// </summary>
        public ReleaseTaskConstraints Constraints { get; internal set; }

        internal ReleaseTask()
        {

        }
    }
}
