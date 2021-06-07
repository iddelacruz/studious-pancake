namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;
    using Domain.Seedwork.Contracts;

    public interface INodesRepository : IRepository<INodePool,string>
    {
    }
}
