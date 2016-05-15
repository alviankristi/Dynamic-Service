using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Autofac;
using DynamicService.ApplicationServices;
using DynamicService.ApplicationServices.Models;
using DynamicService.Dynamic;
using Newtonsoft.Json.Linq;
using IModelBinder = System.Web.Mvc.IModelBinder;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;


namespace DynamicService.Controllers
{
    public class DynamicServiceController : Controller
    {
        // GET: DynamicService
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult GetAll()
        {
            return View();
        }

        public ActionResult Put()
        {
            return View();
        }


        public ActionResult Post([ModelBinder(typeof(DynamicServiceModelBinder))] DynamicServiceModel model)
        {
            var service = ContainerConfig.Container.Resolve(DynamicApiBuilderManager.GetService(model.Id)).GetType();
            var methodInfo = service.GetMethod(model.MethodName);
            object result = null;
            if (methodInfo != null)
            {
                ParameterInfo[] parameters = methodInfo.GetParameters();
                object classInstance = Activator.CreateInstance(service, null);

                if (parameters.Length == 0)
                {
                    // This works fine
                     result = methodInfo.Invoke(classInstance, null);
                }
                else
                {

                    // The invoke does NOT work;
                    // it throws "Object does not match target type"             
                     result = methodInfo.Invoke(classInstance, model.Parameter);
                }
            }
            return Json(result);
        }
        private object Invoke(Type type, string methodName, object[] parameter)
        {
            MethodInfo methodInfo = type?.GetMethod(methodName);

            if (methodInfo != null)
            {
                object result = null;
                ParameterInfo[] parameters = methodInfo.GetParameters();
                object classInstance = Activator.CreateInstance(type, null);

                if (parameters.Length == 0)
                {
                    // This works fine
                    return methodInfo.Invoke(classInstance, null);
                }
                else
                {

                    // The invoke does NOT work;
                    // it throws "Object does not match target type"             
                    return methodInfo.Invoke(methodInfo, parameter);
                }
            }
            return null;
        }

    }

    public class DynamicServiceModelBinder : IModelBinder
    {
        public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }



        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var incomingData = bindingContext.ValueProvider;

            var id = incomingData.GetValue("Id").RawValue.ToString();
            var methodName = incomingData.GetValue("MethodName").RawValue.ToString();
            var service = ContainerConfig.Container.Resolve(DynamicApiBuilderManager.GetService(id));
            var parameters = service.GetType().GetMethod(methodName).GetParameters();
            var paramObj = new List<object>();
            foreach (var parameterInfo in parameters)
            {
                var parameterType = parameterInfo.ParameterType;
                object instance = Activator.CreateInstance(parameterType);



                // Get a property on the type that is stored in the 
                // property string
                var props = parameterType.GetProperties().ToList();
                foreach (var prop in props)
                {
                    var attrName = $"Parameter.{prop.Name}";
                    var value = incomingData.GetValue(attrName).RawValue;
                    prop.SetValue(instance, Convert.ChangeType(value, prop.PropertyType), null);
                }
                paramObj.Add(instance);
            }

            return new DynamicServiceModel
            {
                Id = id,
                MethodName = methodName,
                ServiceName = incomingData.GetValue("ServiceName").RawValue.ToString(),
                Parameter = paramObj.ToArray()
            };
        }
    }
    public class DynamicServiceModel
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public object[] Parameter { get; set; }
        public string MethodName { get; set; }
    }
}