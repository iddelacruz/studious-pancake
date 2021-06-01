
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints
{
    using System;

    public interface IMaxTaskRetryConstraint
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
        int MaxTaskRetryCount { get; }
    }
}
