
namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;

    public abstract class Node
    {
        public NodeOperatingSystem OperatingSystem { get; internal set; }

        public NodeSize Size { get; internal set; }

        protected Node(NodeOperatingSystem operatingSystem, NodeSize size)
        {
            this.OperatingSystem = operatingSystem;
            this.Size = size;
        }
    }    
}
