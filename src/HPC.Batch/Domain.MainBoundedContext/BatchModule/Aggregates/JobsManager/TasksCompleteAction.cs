
namespace Domain.MainBoundedContext.BatchModule.Aggregates.JobsManager
{
    using System;

    public class TasksCompleteAction
    {
        public byte Value { get; protected set; }

        internal TasksCompleteAction(byte value)
        {
            this.Value = value;
        }
    }

    public class NoActionWhenAllTasksComplete : TasksCompleteAction
    {
        internal NoActionWhenAllTasksComplete() : base(0)
        {

        }
    }

    public class TerminateJobWhenAllTasksComplete : TasksCompleteAction
    {
        internal TerminateJobWhenAllTasksComplete():base(1)
        {
            
        }
    }
}
