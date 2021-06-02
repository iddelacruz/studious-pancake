
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Decorators
{
    using System;
    using System.Linq;
    using Aggregates.Tasks;

    public class AddJobPreparationTask : JobDecorator
    {
        private readonly PreparationTask preparation;

        public AddJobPreparationTask(Job job, PreparationTask preparation) : base(job)
        {
            this.preparation = preparation ?? throw new ArgumentNullException(nameof(preparation));
        }

        public override Job Apply()
        {
            if(Job is JobWithReleaseTask release)
            {
                var casted = new JobWithPreparationAndReleaseTask(Job.Identifier, this.preparation, release.ReleaseTask)
                {
                    Priority = release.Priority,
                    Contraints = release.Contraints,
                    TaskFailedAction = release.TaskFailedAction,                    
                    Pool = release.Pool
                };
                Job = casted;
            }
            else
            {
                var casted = new JobWithPreparationTask(Job.Identifier, this.preparation)
                {
                    Pool = Job.Pool,
                    Contraints = Job.Contraints,
                    Priority = Job.Priority,
                    TaskFailedAction = Job.TaskFailedAction
                };

                Job = casted;
            }
            return Job;
        }
    }
}
