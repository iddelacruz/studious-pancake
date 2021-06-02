namespace Domain.MainBoundedContext.BatchModule.Aggregates.JobsManager
{
    using System;
    using System.Collections.Generic;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.ValueTypes;
    using Domain.Seedwork.DataTypes;

    /// <summary>
    /// Represents a batch JobManager task.
    /// </summary>
    public class JobManagerTask : ITask
    {
        /// <summary>
        /// Get the <see cref="ITask"/> unique identifier.
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        /// Gets the display name of the JobManager task.
        /// </summary>
        public string DisplayName { get; internal set; }

        /// <summary>
        /// Gets a value that indicates whether to terminate all tasks in the job and complete the job when the job manager task completes.
        /// </summary>
        public bool KillJobOnCompletion { get; internal set; }

        /// <summary>
        /// The <see cref="TaskCommand"/> that the <see cref="ITask"/> will run.
        /// </summary>
        public TaskCommand Command { get; internal set; }

        /// <summary>
        /// The constraints under which the task should execute.
        /// </summary>
        public TaskConstraints Constraints { get; internal set; }

        /// <summary>
        /// Gets the number of scheduling slots that the Task required to run.
        /// </summary>
        /// <remarks>
        /// The default is 1. A Task can only be scheduled to run on a compute node if the node has enough free scheduling
        /// slots available.For multi-instance Tasks, this must be 1.
        /// </remarks>
        public ushort RequiredSlots { get; internal set; } = 1;

        /// <summary>
        /// Gets a list of files that the Batch service will download to the compute node before running the command line.
        /// </summary>
        public IEnumerable<ResourceFile> ResourceFiles { get; private set; }

        /// <summary>
        /// Create a new instance of <see cref="JobManagerTask"/>
        /// </summary>
        internal JobManagerTask(string identifier, TaskCommand command)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            this.Identifier = identifier;
            this.Command = command;
        }        
    }
}
