using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Owin;

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
