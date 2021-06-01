namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    using System;

    public class JobContraints
    {
        /// <summary>
        /// Get the maximum number of times each Task may be retried. The Batch service retries a Task if its exit code is nonzero.
        /// </summary>
        /// <remarks>
        /// Note that this value specifically controls the number of retries.
        /// The Batch service will try each Task once, and may then retry up to this limit.
        /// For example, if the maximum retry count is 3, Batch tries a Task up to 4 times (one initial try and 3 retries).
        /// If the maximum retry count is 0, the Batch service does not retry Tasks. If the maximum retry count is -1,
        /// the Batch service retries Tasks without limit. The default value is 0 (no retries).
        /// </remarks>
        public int MaxTaskRetryCount { get; internal set; }

        /// <summary>
        /// Get the maximum elapsed time that the Job may run, measured from the time the Job is created.
        /// </summary>
        /// <remarks>
        /// If the Job does not complete within the time limit, the Batch service terminates it and any Tasks that are still running.
        /// In this case, the termination reason will be MaxWallClockTimeExpiry.
        /// If this property is not specified, there is no time limit on how long the Job may run.
        /// </remarks>
        public DateTime? MaxWallClockTime { get; internal set; }

        /// <summary>
        /// Create a new instance of <see cref="JobContraints"/>
        /// </summary>
        /// <param name="maxTaskRetryCount">The maximum number of times each Task may be retried.</param>
        /// <param name="maxWallClockTime">The maximum elapsed time that the Job may run</param>
        public JobContraints(int maxTaskRetryCount, DateTime? maxWallClockTime)
        {
            this.MaxTaskRetryCount = maxTaskRetryCount;
            this.MaxWallClockTime = maxWallClockTime;
        }
    }
}
