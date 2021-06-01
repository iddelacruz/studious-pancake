
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints
{
    using System;

    public class PreparationTaskConstraints : IMaxTaskRetryConstraint, IMaxWallClockConstraint, IRetentionTimeConstraint, IWaitForSuccessConstraint
    {
        /// <summary>
        /// Gets the maximum number of times each Task may be retried. The Batch service retries a Task if its exit code is nonzero.
        /// </summary>
        /// <remarks>
        /// Note that this value specifically controls the number of retries.
        /// The Batch service will try each Task once, and may then retry up to this limit.
        /// For example, if the maximum retry count is 3, Batch tries a Task up to 4 times (one initial try and 3 retries).
        /// If the maximum retry count is 0, the Batch service does not retry Tasks. If the maximum retry count is -1,
        /// the Batch service retries Tasks without limit. The default value is 0 (no retries).
        /// </remarks>
        public int MaxTaskRetryCount { get; private set; }

        /// <summary>
        /// Gets the maximum elapsed time that the task may run, measured from the time the task starts.
        /// </summary>
        public DateTime? MaxWallClockTime { get; private set; }

        /// <summary>
        /// Gets or sets the minimum time to retain the working directory for the task on the compute node where it ran,
        /// from the time it completes execution. After this time, the Batch service may delete the working directory and all its contents.
        /// </summary>
        public DateTime? RetentionTime { get; private set; }

        /// <summary>      
        /// Gets whether the Batch service should wait for the successful completion of the Job Preparation task before
        /// scheduling any tasks on the compute node.
        /// </summary>
        /// <remarks>
        /// A Job Preparation task execution is considered successful if its ExitCode is 0.
        /// If this property is not specified, a default value of true is assigned by the Batch service.
        /// </remarks>
        public bool WaitForSuccess { get; internal set; }

        /// <summary>
        /// Create a new instance of <see cref="TaskConstraints"/>
        /// </summary>
        /// <param name="maxTaskRetryCount">Maximum number of times each Task may be retried.</param>
        /// <param name="maxWallClockTime">Maximum elapsed time that the task may run, measured from the time the task starts.</param>
        /// <param name="retentionTime">Minimum time to retain the working directory for the task on the compute node where it ran,
        /// from the time it completes execution.</param>
        internal PreparationTaskConstraints(int maxTaskRetryCount, DateTime? maxWallClockTime, DateTime? retentionTime, bool waitForSuccess)
        {
            this.MaxTaskRetryCount = maxTaskRetryCount;
            this.MaxWallClockTime = maxWallClockTime;
            this.RetentionTime = retentionTime;
            this.WaitForSuccess = waitForSuccess;
        }
    }
}
