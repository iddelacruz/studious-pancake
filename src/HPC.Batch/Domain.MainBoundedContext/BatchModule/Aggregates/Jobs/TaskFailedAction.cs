
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    using System;

    public abstract class TaskFailedAction
    {
        public byte Value { get; private set; }

        public TaskFailedAction(byte value)
        {
            this.Value = value;
        }
    }

    public class NoActionWhenTaskFails : TaskFailedAction
    {
        public NoActionWhenTaskFails():base(0)
        {

        }
    }

    public class PerformExitOptionsJobActionWhenTaskFails : TaskFailedAction
    {
        public PerformExitOptionsJobActionWhenTaskFails():base(1)
        {

        }
    }
}
