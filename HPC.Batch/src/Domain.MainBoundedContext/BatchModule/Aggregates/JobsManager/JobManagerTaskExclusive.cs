
namespace Domain.MainBoundedContext.BatchModule.Aggregates.JobsManager
{
    using System;
    using Domain.Seedwork.DataTypes;

    /// <summary>
    /// Determine whether the Job Manager task requires exclusive use of the compute node where it runs.
    /// </summary>
    public class JobManagerTaskExclusive : JobManagerTask
    {
        internal JobManagerTaskExclusive(string identifier, TaskCommand command) : base(identifier, command)
        {
        }
    }
}
