
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    using System;
    using Aggregates.Tasks;

    public class JobWithPreparationTask : Job
    {
        public PreparationTask PreparationTask { get; internal set; }

        internal JobWithPreparationTask(string identifier, PreparationTask preparationTask) : base(identifier)
        {
            this.PreparationTask = preparationTask ?? throw new ArgumentNullException(nameof(preparationTask));
        }
    }
}
