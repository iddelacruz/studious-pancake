
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints
{

    public class StartTaskConstraints : IMaxTaskRetryConstraint, IWaitForSuccessConstraint
    {
        /// <summary>
        /// Get the maximum number of times the task may be retried.        
        /// </summary>
        /// <remarks>
        /// If the maximum retry count is 0, the batch service does not retry the task.
        /// If the maximum retry count is -1, the batch service retries the task without limit.
        /// </remarks>
        public int MaxTaskRetryCount { get; private  set; }

        /// <summary>
        /// Specifies whether the Batch Service should wait for the task to complete successfully.
        /// </summary>
        /// <remarks>
        /// Specifies whether the Batch Service should wait for the task to complete successfully
        /// (that is, to exit with exit code 0) before scheduling any tasks on the compute node.
        /// </remarks>
        public bool WaitForSuccess { get; private set; }

        internal StartTaskConstraints(int maxTaskRetryCount, bool waitForSuccess)
        {
            this.MaxTaskRetryCount = maxTaskRetryCount;
            this.WaitForSuccess = waitForSuccess;
        }
    }
}
