namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks
{
    using System;
    using Domain.Seedwork.DataTypes;

    /// <summary>
    /// Allow to define variable slots per task.
    /// </summary>
    public sealed class TaskWithSlots : Task
    {
        /// <summary>
        /// Define slots per task in job.
        /// </summary>
        /// <remarks>
        /// You can set variable task slots if your tasks have different weights regarding to resource usage on the compute node.
        /// You don't specify a task's requiredSlots to be greater than the pool's taskSlotsPerNode
        /// https://docs.microsoft.com/en-us/azure/batch/batch-parallel-node-tasks#define-variable-slots-per-task
        /// </remarks>
        public uint RequiredSlots { get; internal set; }

        internal TaskWithSlots()
        {

        }
    }
}
