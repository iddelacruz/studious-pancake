
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints
{
    using System;

    public interface IWaitForSuccessConstraint
    {
        /// <summary>
        /// Specifies whether the Batch Service should wait for the task to complete successfully.
        /// </summary>
        /// <remarks>
        /// Specifies whether the Batch Service should wait for the task to complete successfully
        /// (that is, to exit with exit code 0) before scheduling any tasks on the compute node.
        /// </remarks>
        bool WaitForSuccess { get; }
    }
}
