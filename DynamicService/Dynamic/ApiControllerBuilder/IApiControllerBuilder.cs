using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicService.Dynamic.ApiControllerBuilder
{
    public interface IApiControllerBuilder<T>
    {
        void Build();
    }
}