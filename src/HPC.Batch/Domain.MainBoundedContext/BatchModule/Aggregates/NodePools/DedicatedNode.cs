
namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;

    public sealed class DedicatedNode : Node
    {
        internal DedicatedNode(NodeOperatingSystem operatingSystem, NodeSize size) : base(operatingSystem, size)
        {
        }
    }
}
