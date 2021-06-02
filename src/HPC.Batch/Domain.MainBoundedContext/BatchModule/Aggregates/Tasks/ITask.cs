

namespace Domain.MainBoundedContext.BatchModule.Aggregates.Tasks
{
    using System.Collections.Generic;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Resources;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.ValueTypes;
    using Domain.Seedwork.Contracts;

    public interface ITask : ITask<string>, IEntity<string>
    {
        new string Identifier { get; }

        FromAutoStorageResourceFile ResourceFile { get; }

        IEnumerable<EnvironmentVariable> EnvironmentVariables { get; }
    }
}
