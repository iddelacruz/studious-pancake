
namespace Application.MainBoundedContext.BatchModule.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        Task<INodePool> HandleAsync(INodePool request);
    }
}
