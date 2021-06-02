
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints
{
    using System;

    public interface IRetentionTimeConstraint
    {
        /// <summary>
        /// Gets or sets the minimum time to retain the working directory for the task on the compute node where it ran,
        /// from the time it completes execution. After this time, the Batch service may delete the working directory and all its contents.
        /// </summary>
        DateTime? RetentionTime { get;}
    }
}
