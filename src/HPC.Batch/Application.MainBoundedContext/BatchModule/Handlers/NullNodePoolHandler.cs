
namespace Application.MainBoundedContext.BatchModule.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.DTO;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Builders;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Decorators;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders;

    using TaskAgg = Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    internal class NullNodePoolHandler : Handler
    {
        private readonly INodePoolRepository nodePoolRepository;

        private readonly IJobsRepository jobRepository;

        public NullNodePoolHandler(BatchExecutorConfig config, INodePoolRepository nodePoolRepository, IJobsRepository jobRepository)
            :base(config)
        {
            this.nodePoolRepository = nodePoolRepository;
            this.jobRepository = jobRepository;
        }

        public override async Task<INodePool> HandleAsync(INodePool request = null)
        {
            if (config.PoolConfig is null)
            {
                throw new ArgumentNullException(nameof(config.PoolConfig));
            }

            if(config.NodesConfig is null)
            {
                throw new ArgumentNullException(nameof(config.NodesConfig));
            }

            var poolConfig = config.PoolConfig;
            var poolId = poolConfig.Identifier;

            INodePool pool = BuildBasicPool(poolConfig, config.NodesConfig);

            if (!string.IsNullOrWhiteSpace(poolConfig.Concurrency?.Policy))
            {
                new TurnsOnConcurrency(pool, Policy(poolConfig), poolConfig.Concurrency.TaskSlotsPerNode).Apply();
            }

            if (poolConfig.StartTask is not null)
            {                
                new AddStartTask(pool, BuildStartTask(poolId, poolConfig.StartTask)).Apply();
            }
            return await base.HandleAsync(request);
        }

        private INodePool BuildBasicPool(PoolConfig poolConfig, NodesConfig nodesConfig)
        {   
            return new NodePoolBuilder(jobRepository, nodePoolRepository)
                .ID(poolConfig.Identifier)
                .Details(poolConfig.Identifier)
                .OperatingSystem(nodesConfig.NodeAgentSkuId, nodesConfig.Publisher, nodesConfig.Offer, nodesConfig.Sku, nodesConfig.Version)
                .NodeSize(nodesConfig.VirtualMachineSize)
                .NodeType(poolConfig.DedicatedNodes, poolConfig.LowPriorityNodes ?? 0)
                .AppPackageReference(poolConfig.PackageReference?.Identifier, poolConfig.PackageReference?.Version)
                .Build();
        }

        private static TaskAgg.StartTask BuildStartTask(string poolId, StartTask taskConfig)
        {
            //TODO: acabar builder de la task
            return new StartTaskBuilder()
                    .ID($"start_task_{poolId}")
                    .Command(taskConfig.Command)
                    .MaxTaskRetryCount(taskConfig.MaxTaskRetryCount ?? 0)
                    .WaitForSuccess(taskConfig.WaitForSuccess ?? false)
                    .ResourceFile(taskConfig.Resource.ContainerName, taskConfig.Resource.BlobName)
                    .Build();
        }

        private static TaskSchedulingPolicy Policy(PoolConfig config)
        {
            TaskSchedulingPolicy policy;

            //TODO: fix this
            if (config.Concurrency.Policy.ToLowerInvariant() == "pack")
            {
                policy = new PackSchedulingPolicy();
            }
            else
            {
                policy = new SpreadSchedulingPolicy();
            }

            return policy;
        }

    }
}
