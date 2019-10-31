using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

public class ApiDependencyResolver : IDependencyResolver
{
    protected IUnityContainer _container;

    public ApiDependencyResolver(IUnityContainer container)
    {
        _container = container ?? throw new ArgumentNullException("container");
    }

    public object GetService(Type serviceType)
    {
        try
        {
            return _container.Resolve(serviceType);
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
            return _container.ResolveAll(serviceType);
        }
        catch (ResolutionFailedException)
        {
            return new List<object>();
        }
    }

    public IDependencyScope BeginScope()
    {
        var child = _container.CreateChildContainer();
        return new ApiDependencyResolver(child);
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        _container.Dispose();
    }
}