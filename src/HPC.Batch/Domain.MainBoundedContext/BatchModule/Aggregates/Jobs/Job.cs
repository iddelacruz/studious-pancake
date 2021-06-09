
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    using System;
    using Aggregates.NodePools;
    using Domain.Seedwork.Contracts;

    public class Job : IEntity<string>
    {
        /// <summary>
        /// Gets the <see cref="Job"/> unique identifier.
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// Gets the priority of the <see cref="Job"/>.        
        /// </summary>
        /// <remarks>
        /// Priority values can range from -1000 to 1000, with -1000 being the lowest priority and 1000 being the highest priority.
        /// </remarks>
        public short Priority { get; internal set; }

        /// <summary>
        /// Gets the execution constraints for a <see cref="Job"/>.
        /// </summary>
        public JobContraints Contraints { get; internal set; }

        /// <summary>
        /// Gets the action the batch service should take when any task in the job fails.
        /// </summary>
        public TaskFailedAction TaskFailedAction { get; internal set; }

        public JobState State { get; internal set; }

        /// <summary>
        /// Job's associated tasks.
        /// </summary>
        public Tasks Tasks { get; internal set; }

        /// <summary>
        /// Gets the <see cref="NodePool"/> to which the <see cref="Job"/> belongs.
        /// </summary>
        public NodePool Pool { get; internal set; }

        /// <summary>
        /// Create a new instance of <see cref="Job"/>
        /// </summary>
        /// <param name="identifier"><see cref="Job"/> unique identifier.</param>
        internal Job(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }
            this.Identifier = identifier;
            this.Tasks = new Tasks(this);
        }

        internal Job()
        {

        }
    }
}
