using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicService.Dynamic.Models
{
    public class DynamicResponseModel
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public object ResponseObject { get; set; }
    }
}