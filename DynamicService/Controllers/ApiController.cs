using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using DynamicService.Dynamic.Helpers;
using DynamicService.Dynamic.JavascriptGenerator;

namespace DynamicService.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult GetAll()
        {
            IScriptGenerator angular = new AngularScriptGenerator();
            return JavaScript(angular.GenerateScript());
        }



        public ActionResult Index()
        {
            return View();
        }
    }

   
}