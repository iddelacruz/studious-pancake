namespace Domain.Seedwork.Contracts
{
    using System.Threading.Tasks;

    public interface INodePool<T>
    {
        T Identifier { get;}
        Task CommitAsync(); 
    }
}
