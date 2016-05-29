using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DynamicService.Dynamic.Helpers
{
    public class DynamicServiceInfo
    {
        public DynamicServiceInfo(Type serviceType, string id)
        {
            Id = id;
            ServiceFullName = serviceType.FullName;
            ServiceName = serviceType.Name;
            ServiceType = serviceType;
            Methods = serviceType.GetMethods(bindingAttr: BindingFlags.Public | BindingFlags.Instance);
            MethodServices = Methods.Select(a => new MethodService
            {
                Name = a.Name,
                Parameters = a.GetParameters().Select(b => new ParameterMethod
                {
                    Type = b.ParameterType,
                    Name = b.Name
                }).ToList(),
                Return = a.ReturnType
            }).ToList();
        }

        public string ServiceName { get; set; }
        public string ServiceFullName { get; set; }
        public Type ServiceType { get; set; }
        public MethodInfo[] Methods { get; set; }
        public List<MethodService> MethodServices { get; set; }
        public string Id { get; set; }
    }

    public class MethodService
    {
        public string Name { get; set; }
        public object Return { get; set; }
        public List<ParameterMethod> Parameters { get; set; }
    }

    public class ParameterMethod
    {
        public Type Type { get; set; }
        public string Name { get; set; }
    }
}