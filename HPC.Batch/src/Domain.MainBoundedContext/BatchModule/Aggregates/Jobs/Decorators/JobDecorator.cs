namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Decorators
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    public abstract class JobDecorator : Job
    {
        protected Job Job;

        public JobDecorator(Job job) : base(job.Identifier)
        {
            this.Job = job;
        }

        public abstract Job Apply();
    }       
}
