
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks
{
    using System.Collections.Generic;
    using Aggregates.Resources;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints;
    using Domain.Seedwork.DataTypes;
    using ValueTypes;

    /// <summary>
    /// A Job Preparation task to run before any tasks of the job on any given compute node.
    /// </summary>
    public class PreparationTask : ITask
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
        /// Gets the file that the batch service will download to the compute node before running the command line.
        /// </summary>
        public FromAutoStorageResourceFile ResourceFile { get; internal set; }

        /// <summary>
        /// Get the constraints under which the task should execute.
        /// </summary>
        public PreparationTaskConstraints Constraints { get; internal set; }

        /// <summary>
        /// Get The environment variables that are required by your application. 
        /// </summary>
        public IEnumerable<EnvironmentVariable> EnvironmentVariables { get; private set; }

        /// <summary>
        /// Gets whether the batch service should rerun the Job Preparation task after a compute node reboots.
        /// </summary>
        /// <remarks>
        /// The Job Preparation task is always rerun if a compute node is reimaged, or if the Job Preparation task did not complete
        /// (e.g. because the reboot occurred while the task was running). Therefore, you should always write a Job Preparation task to be idempotent
        /// and to behave correctly if run multiple times. If this property is not specified, a default value of true is assigned by the Batch service.
        /// </remarks>
        public bool ReRunOnComputeNodeRebootAfterSuccess { get; internal set; }

        internal PreparationTask()
        {

        }
    }
}
