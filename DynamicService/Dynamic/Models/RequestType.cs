using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicService.Dynamic.Models
{
    public class RequestType : Attribute
    {
        private RequestTypes _requestTypes;
        public RequestType(RequestTypes requestTypes)
        {
            _requestTypes = requestTypes;
        }
    }

    public enum RequestTypes
    {
        Get,
        Post
    }
}