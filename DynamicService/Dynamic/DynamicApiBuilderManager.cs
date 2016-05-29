using System;
using System.Collections.Generic;
using System.Linq;
using DynamicService.Dynamic.Helpers;

namespace DynamicService.Dynamic
{
    public static class DynamicApiBuilderManager
    {
        private static readonly IDictionary<string, DynamicServiceInfo> DynamicApiControllers;

        static DynamicApiBuilderManager()
        {
            DynamicApiControllers = new Dictionary<string, DynamicServiceInfo>();
        }
        public static IEnumerable<DynamicServiceInfo> GetAll()
        {
            return DynamicApiControllers.Values;
        }

        public static void Register(string serviceName, DynamicServiceInfo serviceInfo)
        {
            DynamicApiControllers.Add(serviceName, serviceInfo);
        }

        public static Type GetService(string key)
        {
            var obj = DynamicApiControllers.FirstOrDefault(a => a.Key.Equals(key));
            if (obj.Value == null)
            {
                throw new ApplicationException("The key is not found");
            }
            return obj.Value.ServiceType;
        }
    }
}