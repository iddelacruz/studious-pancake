namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Builders
{
    public enum WhenAllTaskComplete : byte
    {
        NoAction,
        TerminateJob
    }

    public enum TaskFailure : byte
    {
        //Do nothing.
        NoAction,
        //Take the action associated with the task exit condition in the task's ExitConditions collection.
        PerformExitOptionsJobAction
    }
}
