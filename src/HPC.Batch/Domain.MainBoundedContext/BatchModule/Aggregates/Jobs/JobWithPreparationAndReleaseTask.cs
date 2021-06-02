
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    public class JobWithPreparationAndReleaseTask : JobWithPreparationTask
    {
        public ReleaseTask ReleaseTask { get; internal set; }

        internal JobWithPreparationAndReleaseTask(string identifier, PreparationTask preparationTask, ReleaseTask releaseTask)
            : base(identifier, preparationTask)
        {
            this.ReleaseTask = releaseTask ?? throw new ArgumentNullException(nameof(releaseTask));
        }
    }
}
