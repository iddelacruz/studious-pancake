
namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    public sealed class NodePoolWithStartTask : NodePool
    {
        /// <summary>
        /// Get the start task.
        /// </summary>
        /// <remarks>
        /// The total size of a start task must be less than or equal to 32768 characters,
        /// including resource files and environment variables.
        /// </remarks>
        public StartTask StartTask { get; internal set; }

        internal NodePoolWithStartTask(string poolId, INodePoolRepository poolRepository, IJobsRepository jobRepository, StartTask startTask)
            : base(poolId, poolRepository, jobRepository)
        {
            this.StartTask = startTask;
        }
    }
}
