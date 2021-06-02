namespace Presentation.Console
{
    using System;
    using System.Threading.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Builders;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Decorators;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders;
    using Infrastructure.Data.MainBoundedContext.BatchModule;

    class Program
    {
        static async Task Main(string[] args)
        {
            /*var poolBuilder = new NodePoolBuilder(null,null);
            var startTaskBuilder = new StartTaskBuilder();

            INodePool pool = poolBuilder
                .ID("demo-01-pool")
                .Details("Demo node pool 01")
                .OperatingSystem("batch.node.windows amd64", "MicrosoftWindowsServer", "WindowsServer", "2016-datacenter-smalldisk", "latest")
                .NodeSize("standard_d1_v2")
                .NodeType(targetDedicatedNodes: 2, targetLowPriorityNodes: 0)
                .Build();

            new TurnsOnConcurrency(new AddStartTask(pool, startTaskBuilder.Build()).Apply(), new PackSchedulingPolicy(), 4).Apply();

            if ((pool is ConcurrentNodePoolWithStartTask concurrent))
            {
                //true
            }

            //before create nodes and tasks
            await pool.CommitAsync();*/

            var provider = new AzureKeyVaultCredentialProvider();

            Console.ReadLine();
        }
    }
}
