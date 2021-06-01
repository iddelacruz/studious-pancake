
namespace Application.MainBoundedContext.BatchModule.Handlers
{
    using System.Threading.Tasks;
    using Application.MainBoundedContext.DTO;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;

    internal abstract class Handler : IHandler
    {
        private IHandler _nextHandler;

        protected readonly BatchExecutorConfig config;

        public Handler(BatchExecutorConfig config)
        {
            this.config = config;
        }

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual async Task<INodePool> HandleAsync(INodePool request)
        {
            if (this._nextHandler != null)
            {
                return await this._nextHandler.HandleAsync(request);
            }
            else
            {
                return request;
            }
        }
    }
}
