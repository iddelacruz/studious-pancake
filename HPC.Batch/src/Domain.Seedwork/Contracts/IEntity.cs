
namespace Domain.Seedwork.Contracts
{
    using System;

    public interface IEntity<T>
    {
        T Identifier { get; }
    }
}
