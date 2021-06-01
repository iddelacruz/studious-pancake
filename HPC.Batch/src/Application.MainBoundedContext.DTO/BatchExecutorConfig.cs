namespace Application.MainBoundedContext.DTO
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "batchExecConfig")]
    public class BatchExecutorConfig
    {
        /// <summary>
        /// Gets or sets the node pool configuration.
        /// </summary>
        [Required]
        [DataMember(Name = "poolConfig", Order = 1)]
        public PoolConfig PoolConfig { get; set; }

        /// <summary>
        /// Gets or sets the nodes configuration.
        /// </summary>
        [Required]  
        [DataMember(Name = "nodesConfig", Order = 2)]
        public NodesConfig NodesConfig { get; set; }

        /// <summary>
        /// Gets or sets the jobs configuration.
        /// </summary>
        [Required]
        [DataMember(Name = "jobsConfig", Order = 3)]
        public JobsConfig JobsConfig { get; set; }

        /// <summary>
        /// Gets or sets the tasks configuration.
        /// </summary>
        [Required]
        [DataMember(Name = "tasksConfig", Order = 4)]
        public TaskConfig TasksConfig { get; set; }

        /// <summary>
        /// Gets or sets the resources to be processed by tasks
        /// </summary>
        [Required]
        [DataMember(Name = "resources", Order = 5)]
        public List<ResourceFile> Resources { get; set; }
    }
}
