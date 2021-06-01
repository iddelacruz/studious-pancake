
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Decorators
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    public class AddJobReleaseTask : JobDecorator
    {
        private readonly ReleaseTask release;

        public AddJobReleaseTask(Job job, ReleaseTask release) : base(job)
        {
            this.release = release ?? throw new ArgumentNullException(nameof(release));
        }

        public override Job Apply()
        {
            if (Job is JobWithPreparationTask preparation)
            {
                var casted = new JobWithPreparationAndReleaseTask(Job.Identifier, preparation.PreparationTask, release)
                {
                    Pool = preparation.Pool,
                    Priority = preparation.Priority,
                    Contraints = preparation.Contraints,
                    TaskFailedAction = preparation.TaskFailedAction,
                    Tasks = Job.Tasks
                };
                Job = casted;
            }
            else
            {
                var casted = new JobWithReleaseTask(Job.Identifier, this.release)
                {
                    Pool = Job.Pool,
                    Contraints = Job.Contraints,
                    Priority = Job.Priority,
                    TaskFailedAction = Job.TaskFailedAction,
                    Tasks = Job.Tasks,
                };

                Job = casted;
            }
            return Job;
        }
    }
}
