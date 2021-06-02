
namespace Infrastructure.Crosscutting.IoC.Implementation.Extensions
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;
    using SimpleInjector.Integration.ServiceCollection;

    public static class SimpleInjectorServiceLocatorExtensions
    {
        public static void AddSimpleInjectorServiceLocator(this IServiceCollection services, Action<SimpleInjectorAddOptions> setupAction = null)
        {
            ServiceLocatorFactory.SetCurrent(new SimpleInjectorServiceLocatorFactory());
            var serviceLocator = (SimpleInjectorServiceLocator)ServiceLocatorFactory.Instance();

            services.AddSimpleInjector(serviceLocator.Container, setupAction);
        }
    }
}
