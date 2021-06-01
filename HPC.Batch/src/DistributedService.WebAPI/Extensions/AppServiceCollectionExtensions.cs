namespace DistributedService.WebAPI.Extensions
{
    using Application.MainBoundedContext.BatchModule;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Infrastructure.Crosscutting.IoC;
    using Infrastructure.Data.MainBoundedContext.BatchModule;
    using Infrastructure.Data.MainBoundedContext.BatchModule.Repositories;
    using Infrastructure.Data.Seedwork;
    using Microsoft.Azure.Batch;
    using Microsoft.Extensions.Configuration;

    public static class AppServiceCollectionExtensions
    {

        public static void InitializeContainer(this IServiceLocator services, IConfiguration configuration)
        {
            services.RegisterSingleton<ICredentialProvider, AzureKeyVaultCredentialProvider>();
            services.RegisterSingleton<IBatchClient<BatchClient>, AzureBatchClient>();

            services.Register<INodePoolRepository, AzureBatchNodePoolRepository>();
            services.Register<IJobsRepository, AzureBatchJobsRepository>();
            services.Register<IBatchExecutor, BatchServiceExecutor>();
        }
    }
}