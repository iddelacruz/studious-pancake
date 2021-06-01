
namespace Domain.MainBoundedContext.BatchModule.Aggregates.JobsManager
{
    using System;
    using Domain.Seedwork.Contracts;


    public class JobManager : IEntity<string>
    {
        /// <summary>
        /// Gets the <see cref="JobManager"/> unique identifier.
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// Get the Specific action the batch service should take when all tasks in the job are in the completed state.
        /// </summary>
        public TasksCompleteAction OnAllTasksComplete { get; internal set; }

        public JobManagerTask Task { get; internal set; }        

        internal JobManager()
        {
        }
    }
}
