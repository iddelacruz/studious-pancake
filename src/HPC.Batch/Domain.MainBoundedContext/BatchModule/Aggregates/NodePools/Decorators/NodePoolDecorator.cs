namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools.Decorators
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Aggregates.Applications;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Nodes;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.Seedwork.Contracts;

    public abstract class NodePoolDecorator : INodePool
    {
        protected INodePool nodePool;

        public NodePoolDecorator(INodePool nodePool)
        {
            this.nodePool = nodePool ?? throw new ArgumentNullException(nameof(nodePool));
        }

        public string Identifier => this.nodePool.Identifier;

        public abstract INodePool Apply();

        #region hidden interface members
        Details INodePool.Details => throw new NotImplementedException();

        IEnumerable<PackageReference> INodePool.PackageReferences => throw new NotImplementedException();

        ICollection<Node> INodePool.Nodes => throw new NotImplementedException();

        ICollection<Job> INodePool.Jobs => throw new NotImplementedException();

        Task INodePool<string>.CommitAsync()
        {
            throw new InvalidOperationException("Operation is not required.");
        }
        #endregion
    }
}
