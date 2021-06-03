 
namespace Application.MainBoundedContext.BatchModule
{
    using System;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.BatchModule.Handlers;
    using Application.MainBoundedContext.DTO;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.Seedwork.Contracts;
    using Domain.Seedwork.Events;

    public class BatchServiceExecutor : IBatchExecutor
    {
        public event NotificationEventHandler Notify;

        private readonly INodePoolRepository nodePoolRepository;

        private readonly IJobsRepository jobRepository;

        /// <summary>
        /// Create an instance of <see cref="BatchServiceExecutor"/>
        /// </summary>
        /// <param name="nodePoolRepository">Node pool collecction</param>
        /// <param name="jobRepository">Job colllection</param>
        public BatchServiceExecutor(INodePoolRepository nodePoolRepository, IJobsRepository jobRepository)
        {
            this.nodePoolRepository = nodePoolRepository
                ?? throw new ArgumentNullException(nameof(nodePoolRepository));

            this.jobRepository = jobRepository
                ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        public async Task RunAsync(BatchExecutorConfig config)
        {
            if(config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            try
            {
                INodePool pool = await this.nodePoolRepository.GetAsync(config.PoolConfig.Identifier);       

                if (pool is null)
                {
                    var handler = new NullNodePoolHandler(config, nodePoolRepository, jobRepository);
                    handler.SetNext(new JobHandler(config));
                    await handler.HandleAsync();
                }
                else
                {
                    await new JobHandler(config)
                        .HandleAsync(pool);
                }

                await pool.CommitAsync();
            }
            catch (Exception ex)
            {
                OnNotify(new ExceptionEventArgs(ex));
                return;
            }           
        }

        protected void OnNotify(NotificationEventArgs e)
        {
            Notify?.Invoke(this, e);
        }
    }
} 