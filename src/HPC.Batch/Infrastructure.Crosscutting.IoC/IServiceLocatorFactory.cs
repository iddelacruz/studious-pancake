namespace Infrastructure.Crosscutting.IoC
{
    using System;

    public interface IServiceLocatorFactory
    {
        IServiceLocator Instance();
    }
}
