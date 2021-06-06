namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    using Domain.Seedwork.Contracts;

    public interface IJobsRepository : IRepository<Job,string>
    {
        
    }
}
