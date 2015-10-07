using System;
using System.Web.Mvc;

namespace YourGet.Infrastructure
{
    public class DependencyResolverServiceProviderAdapter
            : IServiceProvider
    {
        private readonly IDependencyResolver _dependencyResolver;

        public DependencyResolverServiceProviderAdapter(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public object GetService(Type serviceType)
        {
            return _dependencyResolver.GetService(serviceType);
        }
    }
}
