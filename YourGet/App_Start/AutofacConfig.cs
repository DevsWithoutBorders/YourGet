using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Owin;
using Autofac.Integration.Mvc;

namespace YourGet
{
    public static class AutofacConfig
    {
        public static IAppBuilder UseAutofacInjection(this IAppBuilder app)
        {
            var currentAssembly = typeof(AutofacConfig).Assembly;

            var builder = new ContainerBuilder();
            builder.RegisterControllers(currentAssembly).PropertiesAutowired();
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterFilterProvider();
            builder.RegisterAssemblyModules(currentAssembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Register the Autofac middleware FIRST, then the Autofac MVC middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            return app;
        }
    }
}