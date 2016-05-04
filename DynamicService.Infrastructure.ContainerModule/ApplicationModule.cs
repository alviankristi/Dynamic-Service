using Autofac;
using DynamicService.ApplicationServices;

namespace DynamicService.Infrastructure.ContainerModule
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationService>().As<IApplicationService>().SingleInstance();
        }
    }
}
