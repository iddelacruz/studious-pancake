
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    using System;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    public class JobWithReleaseTask : Job
    {
        public ReleaseTask ReleaseTask { get; private set; }

        internal JobWithReleaseTask(string identifier, ReleaseTask releaseTask) : base(identifier)
        {
            this.ReleaseTask = releaseTask ?? throw new ArgumentNullException(nameof(releaseTask));
        }
    }
}
