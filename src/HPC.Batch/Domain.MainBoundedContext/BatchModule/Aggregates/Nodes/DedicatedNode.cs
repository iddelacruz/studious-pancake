
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Nodes
{
    using System;

    public sealed class DedicatedNode : Node
    {
        internal DedicatedNode(NodeOperatingSystem operatingSystem, NodeSize size) : base(operatingSystem, size)
        {
        }
    }
}
