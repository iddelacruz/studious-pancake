
namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using System.Linq;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Builders;
    using Infrastructure.Crosscutting.IoC;
    using Microsoft.Azure.Batch;

    internal class NodePoolCreator
    {
        private readonly CloudPool cloudPool;
        private readonly IServiceLocator provider;

        public NodePoolCreator(CloudPool cloudPool, IServiceLocator provider)
        {
            this.cloudPool = cloudPool;
            this.provider = provider;
        }

        public INodePool Create()
        {
            var poolBuilder = new NodePoolBuilder(
                this.provider.GetService<IJobsRepository>(),
                this.provider.GetService<INodesRepository>());

            var dedicated = (ushort)(cloudPool.TargetDedicatedComputeNodes ?? 0);
            var low = (ushort)(cloudPool.TargetLowPriorityComputeNodes ?? 0);

            var os = cloudPool.VirtualMachineConfiguration;

            poolBuilder
                .ID(cloudPool.Id)
                .Details(cloudPool.DisplayName)
                .OperatingSystem(
                    os.NodeAgentSkuId,
                    os.ImageReference.Publisher,
                    os.ImageReference.Offer,
                    os.ImageReference.Sku,
                    os.ImageReference.Version)
                .NodeSize(cloudPool.VirtualMachineSize)
                .NodeType(dedicated, low);

            if(cloudPool.ApplicationPackageReferences is not null && cloudPool.ApplicationPackageReferences.Any())
            {
                poolBuilder.AppPackageReference(new PackageReferenceExtractor(cloudPool).Extract());
            }

            INodePool pool = poolBuilder.Build();

            if (cloudPool.TaskSlotsPerNode is not null && cloudPool.TaskSlotsPerNode > 0)
            {
                new NodePoolConcurrencyExtractor(cloudPool, pool).Extract();                
            }

            if(cloudPool.StartTask is not null)
            {
                new NodePoolStartTaskExtractor(cloudPool, pool).Extract();
            }
            return pool;
        }
    }
}
