using System.Web;
using System.Web.Optimization;
using DynamicService.ApplicationServices;
using DynamicService.Dynamic;

namespace DynamicService
{
    public class DynamicServiceConfig
    {
        public static void Setup()
        {
            DynamicApiBuilder.DynamicApiControllerBuilder.For<IApplicationService>("Application").Build();
            DynamicApiBuilder.DynamicApiControllerBuilder.For<ITestService>("Test").Build();
        }
    }
}
