namespace Infrastructure.Crosscutting.IoC
{
    using System;

    public interface IServiceLocator : IDependencyResolver, IDependencyRegister
    {
        void Verify();
    }
}
