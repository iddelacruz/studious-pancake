using System;
namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    public abstract class JobState
    {
  
    }

    public class NullState : JobState
    {

    }

    public class ActiveState : JobState
    {
       
    }

    public class DisablingState : JobState
    {
    
    }

	public class DisabledState : JobState
    {
		
    }

	public class EnablingState : JobState
    {

    }

	public class TerminatingState : JobState
    {

    }

	public class CompletedState : JobState
    {

	}

    public class DeletingState : JobState
    {

    }

    /*	/// <summary>
/// The job is available to have tasks scheduled.
/// </summary>
Active,
/// <summary>
/// A user has requested that the job be disabled, but the disable
/// operation is still in progress (for example, waiting for tasks to
/// terminate).
/// </summary>
Disabling,
/// <summary>
/// A user has disabled the job. No tasks are running, and no new tasks
/// will be scheduled.
/// </summary>
Disabled,
/// <summary>
/// A user has requested that the job be enabled, but the enable
/// operation is still in progress.
/// </summary>
Enabling,
/// <summary>
/// The job is about to complete, either because a Job Manager task has
/// completed or because the user has terminated the job, but the
/// terminate operation is still in progress (for example, because Job
/// Release tasks are running).
/// </summary>
Terminating,
/// <summary>
/// All tasks have terminated, and the system will not accept any more
/// tasks or any further changes to the job.
/// </summary>
Completed,
/// <summary>
/// A user has requested that the job be deleted, but the delete
/// operation is still in progress (for example, because the system is
/// still terminating running tasks).
/// </summary>
Deleting*/
}
