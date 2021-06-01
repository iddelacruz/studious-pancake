
namespace Infrastructure.Crosscutting.IoC.Exceptions
{
    using System;

    public class ServiceLocatorFactoryNotFoundException : Exception
    {
        public ServiceLocatorFactoryNotFoundException() : base("Service Locator Factory not found") { }
    }
}
