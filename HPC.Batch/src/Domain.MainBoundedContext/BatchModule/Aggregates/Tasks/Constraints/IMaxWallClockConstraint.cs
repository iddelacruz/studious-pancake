
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Constraints
{
    using System;

    public interface IMaxWallClockConstraint
    {
        /// <summary>
        /// Gets the maximum elapsed time that the task may run, measured from the time the task starts.
        /// </summary>
        DateTime? MaxWallClockTime { get; }
    }
}
