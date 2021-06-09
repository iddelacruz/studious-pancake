
namespace Application.MainBoundedContext.BatchModule
{
    using System;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.BatchModule.Handlers;
    using Application.MainBoundedContext.DTO;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Builders;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Decorators;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders;
    using Domain.MainBoundedContext.BatchModule.Services.Monitoring;
    using Domain.Seedwork.Contracts;
    using Domain.Seedwork.Events;
    using TaskAgg = Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    public sealed class BatchServiceExecutor : IBatchExecutor
    {
        public event NotificationEventHandler Notify;

        private readonly INodesRepository nodePoolRepository;

        private readonly IJobsRepository jobRepository;

        private readonly IMonitorService monitor;

        /// <summary>
        /// Create an instance of <see cref="BatchServiceExecutor"/>
        /// </summary>
        /// <param name="nodePoolRepository">Node collecction</param>
        /// <param name="jobRepository">Job colllection</param>
        public BatchServiceExecutor(INodesRepository nodePoolRepository, IJobsRepository jobRepository, IMonitorService monitor)
        {
            this.nodePoolRepository = nodePoolRepository
                ?? throw new ArgumentNullException(nameof(nodePoolRepository));

            this.jobRepository = jobRepository
                ?? throw new ArgumentNullException(nameof(jobRepository));

            this.monitor = monitor
                ?? throw new ArgumentNullException(nameof(monitor));
        }

        public async Task RunAsync(BatchExecutorConfig config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            try
            {
                var poolConfig = config.PoolConfig;

                INodePool pool = await this.nodePoolRepository.GetAsync(poolConfig?.Identifier);

                if (pool is null)
                {
                    pool = BuildBasicPool(poolConfig, config?.NodesConfig);

                    //TODO: Test this
                    new TurnsOnConcurrency(
                        new AddStartTask(pool, BuildStartTask(poolConfig?.Identifier, poolConfig?.StartTask)).Apply(),
                        Policy(poolConfig), poolConfig?.Concurrency.TaskSlotsPerNode).Apply();
                }

                await new JobConfigurator(config).ConfigureAsync(pool);

                this.monitor.Start();

                await pool.CommitAsync();                
            }
            catch (Exception ex)
            {
                OnNotify(new ExceptionEventArgs(ex));
                this.monitor.Stop();
                throw;
            }
        }

        private INodePool BuildBasicPool(PoolConfig poolConfig, NodesConfig nodesConfig)
        {
            return new NodePoolBuilder(jobRepository, nodePoolRepository)
                .ID(poolConfig?.Identifier)
                .Details(poolConfig?.Identifier)
                .OperatingSystem(nodesConfig?.NodeAgentSkuId, nodesConfig?.Publisher, nodesConfig?.Offer, nodesConfig?.Sku, nodesConfig?.Version)
                .NodeSize(nodesConfig?.VirtualMachineSize)
                .NodeType(poolConfig?.DedicatedNodes ?? 0, poolConfig?.LowPriorityNodes ?? 0)
                .AppPackageReference(poolConfig?.PackageReference?.Identifier, poolConfig?.PackageReference?.Version)
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
            if(!string.IsNullOrWhiteSpace(config?.Concurrency?.Policy))
            {
                if (config.Concurrency.Policy.ToLowerInvariant() == "pack")
                {
                    return new PackSchedulingPolicy();
                }
                else
                {
                    return new SpreadSchedulingPolicy();
                }
            }
            else
            {
                return null;
            }
        }

        private void OnNotify(NotificationEventArgs e)
        {
            Notify?.Invoke(this, e);
        }

        #region disposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(this.monitor is not null)
                {
                    this.monitor.Dispose();
                }
            }
        }
        #endregion
    }
} 