namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;

    /// <summary>
    /// Specify task distribution
    /// </summary>
    /// <remarks>
    /// Specify that as many tasks as possible should be assigned to each node before tasks are assigned to another node in the pool ("packing").
    /// </remarks>
    public sealed class PackSchedulingPolicy : TaskSchedulingPolicy
    {
        public PackSchedulingPolicy()
        {
        }
    }
}
