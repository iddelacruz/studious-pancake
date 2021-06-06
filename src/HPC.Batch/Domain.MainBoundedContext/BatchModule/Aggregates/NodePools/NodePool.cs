namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Aggregates.Applications;
    using Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Nodes;

    public class NodePool : INodePool
    {
        /// <summary>
        /// Get the <see cref="INodePool"/> unique identifier.
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// Get the <see cref="NodePool"/> details.
        /// </summary>
        public Details Details { get; internal set; }

        /// <summary>
        /// Get the collection of <see cref="NodePool"/>'s associated nodes.
        /// </summary>
        public ICollection<Node> Nodes { get; internal set; }

        /// <summary>
        /// Gets a list of application packages to be installed on each compute node in the pool.
        /// </summary>
        public IEnumerable<PackageReference> PackageReferences { get; internal set; }

        /// <summary>
        /// Get the collection of <see cref="NodePool"/>'s associated jobs. 
        /// </summary>
        public ICollection<Job> Jobs { get; internal set; }

        internal readonly IJobsRepository jobRepository;

        internal readonly INodePoolRepository poolRepository;

        /// <summary>
        /// Create a new instance of <see cref="NodePool"/>
        /// </summary>
        /// <param name="poolId"><see cref="NodePool"/> unique identifier.</param>
        internal NodePool(string poolId, INodePoolRepository poolRepository, IJobsRepository jobRepository)
        {
            this.Identifier = poolId;
            this.poolRepository = poolRepository;
            this.jobRepository = jobRepository;
            this.Nodes = new List<Node>();
        }
       
        /// <summary>
        /// Execute configured batch service.
        /// </summary>
        public virtual async Task CommitAsync()
        {
            try
            {
                foreach (var job in this.Jobs)
                {
                    await this.jobRepository.AddAsync(job);
                }
            }
            catch (Exception)
            {
                throw;
            } 
        }
    }
}
