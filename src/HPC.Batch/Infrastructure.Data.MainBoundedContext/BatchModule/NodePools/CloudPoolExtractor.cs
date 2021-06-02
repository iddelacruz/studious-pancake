
namespace Infrastructure.Data.MainBoundedContext.BatchModule.NodePools
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Nodes;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Microsoft.Azure.Batch;

    internal class CloudPoolExtractor
    {
        private readonly INodePool pool;
        private readonly BatchClient client;

        public CloudPoolExtractor(INodePool pool, BatchClient client)
        {
            this.pool = pool;
            this.client = client;
        }

        public CloudPool Extract()
        {
            var node = pool.Nodes.First();

            return this.client.PoolOperations.CreatePool(
                             poolId: pool.Identifier,
                             targetDedicatedComputeNodes: NodeCounts<DedicatedNode>(pool.Nodes),
                             virtualMachineSize: node.Size.VirtualMachineSize,
                             virtualMachineConfiguration: new VirtualMachineConfiguration(
                                 imageReference: new Microsoft.Azure.Batch.ImageReference(
                                     publisher: node.OperatingSystem.ImageReference.Publisher,
                                     offer: node.OperatingSystem.ImageReference.Offer,
                                     sku: node.OperatingSystem.ImageReference.Sku,
                                     version: node.OperatingSystem.ImageReference.Version),
                                 nodeAgentSkuId: node.OperatingSystem.NodeAgentSkuId),
                             targetLowPriorityComputeNodes: NodeCounts<LowPriorityNode>(pool.Nodes));
        }

        private static int NodeCounts<T>(IEnumerable<Domain.MainBoundedContext.BatchModule.Aggregates.Nodes.Node> nodes)
        {
            int output = 0;

            foreach (var node in nodes)
            {
                if (node is T)
                {
                    output++;
                }
            }

            return output;
        }
    }
}
