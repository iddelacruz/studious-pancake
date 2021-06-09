
namespace Domain.MainBoundedContext.BatchModule.Services.Monitoring
{
    using System;
    using System.Collections.Generic;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;

    public class TaskStateCache
    {
        private readonly Dictionary<string, TaskState> taskStateMap = new();

        public void UpdateTaskState(string taskId, TaskState taskState)
        {
            this.taskStateMap[taskId] = taskState;
        }

        public TaskStateCounts GetTaskStateCounts()
        {
            TaskStateCounts taskStateCounts = new TaskStateCounts();

            foreach (var kvp in this.taskStateMap)
            {
                taskStateCounts.IncrementCount(kvp.Value);
            }

            return taskStateCounts;
        }
    }
}
