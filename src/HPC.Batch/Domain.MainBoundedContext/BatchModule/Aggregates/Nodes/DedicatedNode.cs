
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Nodes
{
    using System;

    public class DedicatedNode : Node
    {
        internal DedicatedNode(NodeOperatingSystem operatingSystem, NodeSize size) : base(operatingSystem, size)
        {
        }
    }
}
