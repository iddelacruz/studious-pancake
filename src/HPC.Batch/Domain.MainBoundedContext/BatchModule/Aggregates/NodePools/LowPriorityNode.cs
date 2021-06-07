namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;

    public sealed class LowPriorityNode : Node
    {
        internal LowPriorityNode(NodeOperatingSystem operatingSystem, NodeSize size) : base(operatingSystem, size)
        {
        }
    }
}
