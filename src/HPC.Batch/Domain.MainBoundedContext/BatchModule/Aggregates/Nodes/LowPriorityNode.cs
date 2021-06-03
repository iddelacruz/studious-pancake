namespace Domain.MainBoundedContext.BatchModule.Aggregates.Nodes
{
    using System;

    public sealed class LowPriorityNode : Node
    {
        internal LowPriorityNode(NodeOperatingSystem operatingSystem, NodeSize size) : base(operatingSystem, size)
        {
        }
    }
}
