namespace Domain.MainBoundedContext.BatchModule.Aggregates.NodePools
{
    using System;
    using System.Collections.Generic;
    using Aggregates.Applications;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Nodes;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.Seedwork.Contracts;

    public interface INodePool : INodePool<string>, IEntity<string>
    {
        new string Identifier { get; }

        Details Details { get; }

        public ICollection<Node> Nodes { get; }

        public ICollection<Job> Jobs { get; }

        IEnumerable<PackageReference> PackageReferences { get; }
    }
}
