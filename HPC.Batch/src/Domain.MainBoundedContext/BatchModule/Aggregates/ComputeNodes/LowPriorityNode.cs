namespace Domain.MainBoundedContext.BatchModule.Aggregates.ComputeNodes
{
    using System;

    public class LowPriorityNode : ComputeNode
    {
        internal LowPriorityNode(NodeOperatingSystem operatingSystem, NodeSize size) : base(operatingSystem, size)
        {
        }
    }
}
