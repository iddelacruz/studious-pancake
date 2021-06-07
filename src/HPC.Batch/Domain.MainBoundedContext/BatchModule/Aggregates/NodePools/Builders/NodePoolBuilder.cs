namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Aggregates.Applications;
    using Aggregates.Jobs;

    public class NodePoolBuilder
    {
        private IList<PackageReference> applications;

        private Details details;

        private readonly IJobsRepository jobsRepository;

        private NodeSize nodeSize;

        private NodePool nodePool;

        private readonly INodesRepository nodePoolRepository;

        private NodeOperatingSystem operatingSystem;

        private string poolId;

        private ushort targetDedicatedNodes;

        private ushort targetLowPriorityNodes;

        /// <summary>
        /// Create a new instance of <see cref="NodePoolBuilder"/>
        /// </summary>
        public NodePoolBuilder(IJobsRepository jobsRepository, INodesRepository nodePoolRepository)
        {
            this.jobsRepository = jobsRepository ?? throw new ArgumentNullException(nameof(jobsRepository));
            this.nodePoolRepository = nodePoolRepository ?? throw new ArgumentNullException(nameof(nodePoolRepository));
        }

        public NodePoolBuilder AppPackageReference(string appId, string version)
        {
     
            if (this.applications is null)
            {
                this.applications = new List<PackageReference>();
            }

            var query = this.applications
                .FirstOrDefault(x => x.Identifier == appId && x.Version == version);

            if (query is null)
            {
                this.applications.Add(new PackageReference(appId, version));
            }

            return this;
        }

        public NodePoolBuilder AppPackageReference(IDictionary<string, string> packageReferences)
        {
            if (packageReferences is null || !packageReferences.Any())
            {
                throw new ArgumentNullException(nameof(packageReferences),"can't be null or empty");
            }

            foreach (var app in packageReferences)
            {
                this.AppPackageReference(app.Key, app.Value);
            }
            return this;
        }

        /// <summary>
        /// Set node poll details.
        /// </summary>
        /// <param name="displayName">Display name.</param>
        public NodePoolBuilder Details(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new InvalidOperationException("DisplayName parameter can't be null.");
            }

            if (this.details is not null)
            {
                this.details.DisplayName = displayName;
            }
            this.details = new Details(displayName);

            return this;
        }

        /// <summary>
        /// Set node pool size.
        /// </summary>
        /// <param name="virtualMachineSize">Virtual machine size.</param>
        public NodePoolBuilder NodeSize(string virtualMachineSize)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineSize))
            {
                throw new ArgumentNullException(nameof(virtualMachineSize),"parameter can't be null.");
            }

            if (this.nodeSize is not null)
            {
                this.nodeSize.VirtualMachineSize = virtualMachineSize;
            }
            else
            {
                this.nodeSize = new NodeSize(virtualMachineSize);
            }
            return this;
        }

        public NodePoolBuilder NodeType(ushort targetDedicatedNodes, ushort targetLowPriorityNodes)
        {
            if (!(targetDedicatedNodes > 0))
            {
                throw new ArgumentOutOfRangeException(nameof(targetDedicatedNodes),"parameter must be greather than zero.");
            }
            this.targetDedicatedNodes = targetDedicatedNodes;

            this.targetLowPriorityNodes = targetLowPriorityNodes;
            
            return this;
        }

        public NodePoolBuilder OperatingSystem(string nodeAgentSkuId, string publisher, string offer, string sku, string version)
        {
            if (string.IsNullOrWhiteSpace(publisher))
            {
                throw new ArgumentNullException(nameof(publisher));
            }

            if (string.IsNullOrWhiteSpace(offer) || string.IsNullOrWhiteSpace(sku) || string.IsNullOrWhiteSpace(nodeAgentSkuId))
            {
                throw new InvalidOperationException("If publisher has value, nodeAgentSkuId, offer and sku can't be null");
            }

            if (this.operatingSystem is not null)
            {
                this.operatingSystem.NodeAgentSkuId = nodeAgentSkuId;
                this.operatingSystem.ImageReference.Publisher = publisher;
                this.operatingSystem.ImageReference.Offer = offer;
                this.operatingSystem.ImageReference.Sku = sku;
                this.operatingSystem.ImageReference.Version = version;
            }
            else
            {
                this.operatingSystem = new NodeOperatingSystem(
                nodeAgentSkuId, new ImageReference(
                    publisher,
                    offer,
                    sku,
                    version));
            }
            return this;
        }

        public NodePoolBuilder ID(string poolId)
        {
            if (string.IsNullOrWhiteSpace(poolId))
            {
                throw new ArgumentNullException(nameof(poolId), "parameter is required");
            }
            this.poolId = poolId;
            return this;
        }        

        /// <summary>
        /// Create a new instance of <see cref="INodePool"/> .
        /// </summary>
        /// <returns><see cref="INodePool"/> object</returns>
        public INodePool Build()
        {
            if(this.nodePool is null)
            {
                this.nodePool = new NodePool(this.poolId, this.nodePoolRepository, this.jobsRepository);
            }

            this.nodePool.Details = this.details;
            this.nodePool.Nodes = GiveMeTheNodes();
            this.nodePool.PackageReferences = this.applications;
            return this.nodePool;
        }

        private ICollection<Node> GiveMeTheNodes()
        {
            var nodes = new List<Node>(this.targetDedicatedNodes + this.targetLowPriorityNodes);

            for (int i = 0; i < this.targetDedicatedNodes; i++)
            {
                nodes.Add(new DedicatedNode(this.operatingSystem, this.nodeSize));
            }
            for (int i = 0; i < this.targetLowPriorityNodes; i++)
            {
                nodes.Add(new LowPriorityNode(this.operatingSystem, this.nodeSize));
            }
            return nodes;
        }
    }
}
