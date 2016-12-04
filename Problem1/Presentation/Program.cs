using Microsoft.Practices.Unity;
using Problem1.Application;
using System;

namespace Problem1.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();

            container.RegisterType<ITripCardService, TripCardService>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(StringComparer.InvariantCultureIgnoreCase));

            container.RegisterType<TripCardConsole>();

            var tripCardConsoleInstance = container.Resolve<TripCardConsole>();
            tripCardConsoleInstance.Run();
        }
    }
}
