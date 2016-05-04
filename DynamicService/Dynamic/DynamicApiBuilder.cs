using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using DynamicService.Dynamic.ApiControllerBuilder;

namespace DynamicService.Dynamic
{
    public class DynamicApiBuilder
    {
        public static class DynamicApiControllerBuilder
        {
           
            public static IApiControllerBuilder<T> For<T>(string serviceName)
            {
                return new ApiControllerBuilder<T>(serviceName);
            }


          
        }
    }
}