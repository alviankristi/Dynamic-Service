using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using DynamicService.Dynamic.ApiControllerBuilder;
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
    }
}