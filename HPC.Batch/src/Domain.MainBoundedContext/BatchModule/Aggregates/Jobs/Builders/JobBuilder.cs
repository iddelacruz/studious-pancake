
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Builders
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders;
    using Domain.Seedwork.DataTypes;

    /// <summary>
    /// <see cref="Job"/>'s builder
    /// </summary>
    public class JobBuilder
    {
        private string identifier;
        private short priority;

        private int maxTaskRetryCount;
        private DateTime? maxWallClockTime;

        private TaskFailedAction taskFailedAction;
        

        /// <summary>
        /// Sets the <see cref="Job"/> unique identifier.
        /// </summary>
        /// <param name="jobId"><see cref="Job" unique identifier./></param>
        public JobBuilder ID(string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(nameof(jobId));
            }
            this.identifier = jobId;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="Job"/>'s priority.        
        /// </summary>
        /// <param name="priority">The <see cref="Job"/>'s priority.</param>
        /// <remarks>
        /// Priority values can range from -1000 to 1000, with -1000 being the lowest priority and 1000 being the highest priority.
        /// </remarks>
        public JobBuilder Priority(short priority)
        {
            var range = new Range<short>(-1000, 1000);

            var comparision = range.Compare(priority);

            if(comparision != ValueComparison.BetweenFromAndTo)
            {
                throw new InvalidOperationException("Priority values can range from -1000 to 1000");
            }

            this.priority = priority;

            return this;
        }

        /// <summary>
        /// Sets the execution constraints for a <see cref="Job"/>.
        /// </summary>
        /// <param name="maxTaskRetryCount">The maximum number of times each Task may be retried. The Batch service retries a Task if its exit code is nonzero.</param>
        /// <param name="maxWallClockTime">The maximum elapsed time that the Job may run, measured from the time the Job is created.</param>
        public JobBuilder Contraints(int maxTaskRetryCount, DateTime? maxWallClockTime)
        {
            this.maxTaskRetryCount = maxTaskRetryCount;
            this.maxWallClockTime = maxWallClockTime;

            return this;            
        }

        /// <summary>
        /// Sets the action the batch service should take when any task in the job fails.
        /// </summary>
        /// <param name="onTaskFailure">Specifies an action the batch service should take when any task in the job fails.</param>
        public JobBuilder TaskFailureAction(TaskFailure onTaskFailure)
        {
            this.taskFailedAction = null;

            switch (onTaskFailure)
            {
                case TaskFailure.NoAction:
                    this.taskFailedAction = new NoActionWhenTaskFails();
                    break;
                case TaskFailure.PerformExitOptionsJobAction:
                    this.taskFailedAction = new PerformExitOptionsJobActionWhenTaskFails();
                    break;
            }
            return this;
        }

        /// <summary>
        /// Create a new instance of <see cref="Job"/>
        /// </summary>
        public Job Build()
        {
            var job = new Job(this.identifier)
            {
                Priority = this.priority,
                Contraints = new JobContraints(this.maxTaskRetryCount, this.maxWallClockTime),
                TaskFailedAction = this.taskFailedAction
            };
            return job;
        }
    }
}
