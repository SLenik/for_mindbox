using Microsoft.Practices.Unity;
using Problem1.Application;
using Problem1.Infrastructure;
using System;

namespace Problem1.Test
{
    public class TripCardFixture : UnityContainerFixtureBase
    {
        public TripCardFixture()
        {
            Container
                .RegisterType<ITripCardService, TripCardService>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(StringComparer.InvariantCultureIgnoreCase))
                .RegisterType<TripCardComparer>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(StringComparer.InvariantCultureIgnoreCase));
        }

        internal ITripCardService TripCardService => Container.Resolve<ITripCardService>();

        internal TripCardComparer TripCardComparer => Container.Resolve<TripCardComparer>();
    }
}
