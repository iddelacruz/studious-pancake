namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;
    using Domain.Seedwork.Contracts;

    public interface INodePoolRepository : IRepository<INodePool,string>
    {
    }
}
