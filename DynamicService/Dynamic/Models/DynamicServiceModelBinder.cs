using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Autofac;
using IModelBinder = System.Web.Mvc.IModelBinder;
using IValueProvider = System.Web.Mvc.IValueProvider;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;

namespace DynamicService.Dynamic.Models
{
    public class DynamicServiceModelBinder : IModelBinder
    {
        public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }

        private object SetInstanceOfPrimitiveType(IValueProvider incomingData, ParameterInfo parameterInfo)
        {
            var attrName = $"Parameter.{parameterInfo.Name}";
            var value = incomingData.GetValue(attrName).RawValue.ToString();
            var converter = TypeDescriptor.GetConverter(parameterInfo.ParameterType);
            return converter.ConvertFromString(value);
        }

        private object SetInstanceOfObjectType(IValueProvider incomingData, ParameterInfo parameterInfo)
        {
            var instance = Activator.CreateInstance(parameterInfo.ParameterType, true);
            var props = parameterInfo.ParameterType.GetProperties().ToList();
            foreach (var prop in props)
            {
                var attrName = $"Parameter.{prop.Name}";
                var value = incomingData.GetValue(attrName).RawValue;
                prop.SetValue(instance, Convert.ChangeType(value, prop.PropertyType), null);
            }

            return instance;
        }

        private object[] SetParameter(IValueProvider incomingData, string methodName, string id)
        {
            var paramObj = new List<object>();
            var service = ContainerConfig.Container.Resolve(DynamicApiBuilderManager.GetService(id));
            var parameters = service.GetType().GetMethod(methodName).GetParameters();
            foreach (var parameterInfo in parameters)
            {
                var instance = parameterInfo.ParameterType.GetConstructor(Type.EmptyTypes) == null ? SetInstanceOfPrimitiveType(incomingData, parameterInfo) : SetInstanceOfObjectType(incomingData, parameterInfo);
                paramObj.Add(instance);
            }

            return paramObj.ToArray();
        }

        private DynamicServiceModel SetModel(IValueProvider incomingData)
        {
            var id = incomingData.GetValue("Id").RawValue.ToString();
            var methodName = incomingData.GetValue("MethodName").RawValue.ToString();
            var parameters = SetParameter(incomingData, methodName, id);

            return new DynamicServiceModel
            {
                Id = id,
                MethodName = methodName,
                ServiceName = incomingData.GetValue("ServiceName").RawValue.ToString(),
                Parameter = parameters
            };
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return SetModel(bindingContext.ValueProvider);
        }
    }
}