namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;

    /// <summary>
    /// Specify task distribution
    /// </summary>
    /// <remarks>
    /// Specify that tasks should be assigned evenly across all nodes in the pool ("spreading")
    /// </remarks>
    public sealed class SpreadSchedulingPolicy : TaskSchedulingPolicy
    {

    }
}
