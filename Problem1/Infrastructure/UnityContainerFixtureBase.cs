using Microsoft.Practices.Unity;
using System;

namespace Problem1.Infrastructure
{
    /// <summary>
    /// Контекст запуска группы тестов, содержащий IoC-контейнер.
    /// </summary>
    public abstract class UnityContainerFixtureBase : IDisposable
    {
        protected IUnityContainer Container;

        protected UnityContainerFixtureBase()
        {
            Container = new UnityContainer();
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    Container.Dispose();

                _disposed = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
