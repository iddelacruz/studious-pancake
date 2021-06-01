namespace Domain.Seedwork.Contracts
{
    using System;
    using Domain.Seedwork.DataTypes;

    public interface ITask<T>
    {
        T Identifier { get; }

        TaskCommand Command { get; }
    }
}
