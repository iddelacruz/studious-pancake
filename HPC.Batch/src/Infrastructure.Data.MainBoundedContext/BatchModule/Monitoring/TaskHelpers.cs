//Copyright (c) Microsoft Corporation

namespace Infrastructure.Data.MainBoundedContext.BatchModule.Monitoring
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class TaskHelpers
    {
        public static async Task CancellableDelay(TimeSpan delay, CancellationToken ct)
        {
            try
            {
                await Task.Delay(delay, ct);
            }
            catch (TaskCanceledException)
            {
            }
        }

        public static void WaitForCompletionOrCancellation(this Task task)
        {
            try
            {
                task.Wait();
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}
