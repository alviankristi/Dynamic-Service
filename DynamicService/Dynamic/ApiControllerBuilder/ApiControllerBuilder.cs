using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynamicService.Dynamic.Helpers;

namespace DynamicService.Dynamic.ApiControllerBuilder
{
    public class ApiControllerBuilder<T> : IApiControllerBuilder<T>
    {
        private readonly string _serviceName;

        public ApiControllerBuilder(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException("serviceName null or empty!", nameof(serviceName));
            }
            _serviceName = serviceName;
        }

        public void Build()
        {
            DynamicApiBuilderManager.Register(_serviceName, new DynamicServiceInfo(typeof(T),_serviceName));
        }
    }
}