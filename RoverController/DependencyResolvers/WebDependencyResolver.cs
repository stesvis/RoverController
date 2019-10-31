using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Unity;
using Unity.Exceptions;

namespace RoverController.Web
{
    internal class WebDependencyResolver : IDependencyResolver
    {
        private IUnityContainer _unityContainer;

        public WebDependencyResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer ?? throw new ArgumentNullException("container");
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _unityContainer.IsRegistered(serviceType) ? _unityContainer.Resolve(serviceType) : null;
            }
            catch (ResolutionFailedException ex)
            {
                //PDLogger.Logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unityContainer.ResolveAll(serviceType);
            }
            catch (Exception ex)
            {
                //PDLogger.Logger.Error(ex);
                return new List<object>();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _unityContainer.Dispose();
        }
    }
}