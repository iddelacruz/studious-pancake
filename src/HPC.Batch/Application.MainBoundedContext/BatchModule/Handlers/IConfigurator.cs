
namespace Application.MainBoundedContext.BatchModule.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;

    public interface IConfigurator
    {
        IConfigurator SetNext(IConfigurator handler);

        Task<INodePool> ConfigureAsync(INodePool request);
    }
}
