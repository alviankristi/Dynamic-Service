using System;
using System.Web.Mvc;
using Autofac;
using DynamicService.Dynamic;
using DynamicService.Dynamic.JavascriptGenerator;
using DynamicService.Dynamic.Models;


namespace DynamicService.Controllers
{
    public class DynamicServiceController : Controller
    {
        public ActionResult GetAll()
        {
            var angular = new AngularScriptGenerator();
            return JavaScript(angular.GenerateScript());
        }

        public ActionResult Post([ModelBinder(typeof(DynamicServiceModelBinder))] DynamicServiceModel model)
        {
            var service = ContainerConfig.Container.Resolve(DynamicApiBuilderManager.GetService(model.Id)).GetType();
            var methodInfo = service.GetMethod(model.MethodName);
            object result = null;
            if (methodInfo != null)
            {
                var parameters = methodInfo.GetParameters();
                var classInstance = Activator.CreateInstance(service, null);

                result =
                    SetReturnModel(methodInfo.Invoke(classInstance, parameters.Length == 0 ? null : model.Parameter));
            }
            
            return Json(result);
        }

        private DynamicResponseModel SetReturnModel(object returnInvoke)
        {
            try
            {
                return new DynamicResponseModel
                {
                    IsError = false,
                    ErrorMessage = null,
                    ResponseObject = returnInvoke
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponseModel
                {
                    IsError = true,
                    ErrorMessage = ex.Message,
                    ResponseObject = null
                };
            }
        }
    }
}