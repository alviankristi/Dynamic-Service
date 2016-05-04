using System;
using DynamicService.ApplicationServices;
using DynamicService.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicService.Test.ApiController
{
    [TestClass]
    public class RegisterDynamicService
    {
        [TestMethod]
        public void DynamicApiBuilderShouldHaveOneDynamicInfo()
        {
            DynamicApiBuilder.DynamicApiControllerBuilder.For<IApplicationService>("Application").Build();
            DynamicApiBuilder.DynamicApiControllerBuilder.For<ITestService>("Test").Build();
            var dynamicBuilder = DynamicApiBuilderManager.GetAll();
            foreach (var dynamicServiceInfo in dynamicBuilder)
            {
                dynamicServiceInfo.MethodServices.ForEach(a =>
                {
                    Console.WriteLine("Method: " + a.Name);
                    a.Parameters.ForEach(b =>
                    {
                        Console.WriteLine("Parameter : " + b.Name);
                    });

                    Console.WriteLine("Return : " + a.Return.ToString());
                });
            }
        }
    }
}
