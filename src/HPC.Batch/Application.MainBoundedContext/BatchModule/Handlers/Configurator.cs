
namespace Application.MainBoundedContext.BatchModule.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.DTO;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;

    internal abstract class Configurator : IConfigurator
    {
        private IConfigurator _nextHandler;

        protected readonly BatchExecutorConfig config;

        public Configurator(BatchExecutorConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public IConfigurator SetNext(IConfigurator handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual async Task<INodePool> ConfigureAsync(INodePool request)
        {
            if (this._nextHandler != null)
            {
                return await this._nextHandler.ConfigureAsync(request);
            }
            else
            {
                return request;
            }
        }
    }
}
