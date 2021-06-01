//Copyright (c) Microsoft Corporation
namespace Infrastructure.Data.MainBoundedContext.BatchModule.Monitoring
{
    using System.Collections.Generic;
    using Microsoft.Azure.Batch.Common;

    internal sealed class TaskStateCache
    {
        // The key is the task id
        private readonly Dictionary<string, TaskState> taskStateMap = new Dictionary<string, TaskState>();

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
