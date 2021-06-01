namespace Infrastructure.Data.Seedwork
{
    using System;

    public interface IBatchClient<T> : IDisposable
    {
        T GiveMeTheClient();
    }
}
