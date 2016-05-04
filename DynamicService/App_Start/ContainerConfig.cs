using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using DynamicService.Infrastructure.ContainerModule;

namespace DynamicService
{
    internal static class ContainerConfig
    {
        public static IContainer Container;

        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);

            builder.RegisterModule<ApplicationModule>();

            Container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}