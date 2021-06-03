namespace Domain.MainBoundedContext.BatchModule.Aggregates.Nodes
{
    using System;

    public sealed class NodeOperatingSystem
    {
        public string NodeAgentSkuId { get; internal set; }

        public ImageReference ImageReference { get; internal set; }

        internal NodeOperatingSystem(string nodeAgentSkuId, ImageReference imageReference)
        {
            this.NodeAgentSkuId = nodeAgentSkuId;
            this.ImageReference = imageReference;
        }
    }

}
