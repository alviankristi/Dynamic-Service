using System;
using DynamicService.ApplicationServices;
using DynamicService.Dynamic;
using DynamicService.Dynamic.JavascriptGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicService.Test.ApiController
{
    [TestClass]
    public class AngularScriptGeneratorTest
    {
        [TestMethod]
        public void GenerateScript()
        {
            DynamicApiBuilder.DynamicApiControllerBuilder.For<IApplicationService>("Application").Build();
            DynamicApiBuilder.DynamicApiControllerBuilder.For<ITestService>("Test").Build();
            //var dynamicBuilder = DynamicApiBuilderManager.GetAll();
            var script = new AngularScriptGenerator();
            Console.Write(script.GenerateScript());
        }
    }
}
