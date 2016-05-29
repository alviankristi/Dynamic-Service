using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Autofac;
using IModelBinder = System.Web.Mvc.IModelBinder;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;

namespace DynamicService.Dynamic.Models
{
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

                if (parameterInfo.ParameterType.GetConstructor(Type.EmptyTypes) == null)
                {
                    var attrName = $"Parameter.{parameterInfo.Name}";
                    var value = incomingData.GetValue(attrName).RawValue.ToString();
                    var converter = TypeDescriptor.GetConverter(parameterType);
                    var instance = converter.ConvertFromString(value);
                    paramObj.Add(instance);
                }
                else
                {
                    var instance = Activator.CreateInstance(parameterType, true);
                    var props = parameterType.GetProperties().ToList();
                    foreach (var prop in props)
                    {
                        var attrName = $"Parameter.{prop.Name}";
                        var value = incomingData.GetValue(attrName).RawValue;
                        prop.SetValue(instance, Convert.ChangeType(value, prop.PropertyType), null);
                    }
                    paramObj.Add(instance);
                }
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
}