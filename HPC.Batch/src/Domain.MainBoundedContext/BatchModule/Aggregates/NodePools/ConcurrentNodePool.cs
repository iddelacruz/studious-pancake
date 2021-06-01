

namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using Aggregates.Jobs;


    /// <summary>
    /// Creates concurrent pools
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/azure/batch/batch-parallel-node-tasks
    /// </remarks>
    public class ConcurrentNodePool : NodePool
    {
        /// <summary>
        /// Get the number of task slots that can be used to run concurrent tasks on a single compute node in the pool.
        /// </summary>
        /// <remarks>
        /// You can set the taskSlotsPerNode element and TaskSlotsPerNode property only at pool creation time.
        /// They can't be modified after a pool has already been created.
        /// Azure Batch allows you to set task slots per node up to (4x) the number of node cores.
        /// For example, if the pool is configured with nodes of size "Large" (four cores), then taskSlotsPerNode may be set to 16.
        /// However, regardless of how many cores the node has, you can't have more than 256 task slots per node
        /// </remarks>
        public uint TaskSlotsPerNode { get; internal set; }

        /// <summary>
        /// Specify task distribution.
        /// </summary>
        public TaskSchedulingPolicy Policy { get; internal set; }

        internal ConcurrentNodePool(string poolId, uint taskSlotsPerNode, TaskSchedulingPolicy policy, INodePoolRepository poolRepository, IJobsRepository jobRepository)
            : base(poolId, poolRepository, jobRepository)
        {
            this.TaskSlotsPerNode = taskSlotsPerNode;
            this.Policy = policy;
        }
    }
}
