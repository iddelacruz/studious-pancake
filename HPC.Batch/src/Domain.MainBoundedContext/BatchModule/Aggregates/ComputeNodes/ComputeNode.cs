
namespace Domain.MainBoundedContext.BatchModule.Aggregates.ComputeNodes
{
    using System;

    public abstract class ComputeNode
    {
        public NodeOperatingSystem OperatingSystem { get; internal set; }

        public NodeSize Size { get; internal set; }

        protected ComputeNode(NodeOperatingSystem operatingSystem, NodeSize size)
        {
            this.OperatingSystem = operatingSystem;
            this.Size = size;
        }
    }    
}
