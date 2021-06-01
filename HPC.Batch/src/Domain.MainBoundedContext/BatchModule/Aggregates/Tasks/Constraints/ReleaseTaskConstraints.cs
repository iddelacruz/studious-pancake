using System;
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints
{
    public class ReleaseTaskConstraints : IMaxWallClockConstraint, IRetentionTimeConstraint
    {
        /// <summary>
        /// Gets the maximum duration of time for which a task is allowed to run from the time it is created.
        /// </summary>
        public DateTime? MaxWallClockTime { get; private set; }

        /// <summary>
        /// Gets or sets the duration of time for which files in the task's working directory are retained,
        /// from the time it completes execution. After this duration, the task's working directory is reclaimed.
        /// </summary>
        public DateTime? RetentionTime { get; private set; }

        internal ReleaseTaskConstraints(DateTime? maxWallClockTime, DateTime? retentionTime)
        {
            this.MaxWallClockTime = maxWallClockTime;
            this.RetentionTime = retentionTime;
        }
    }
}
