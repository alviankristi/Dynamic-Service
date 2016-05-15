using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicService.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult Services()
        {
            return new JsonResult();
        }

        public ActionResult Index()
        {
            return View();
        }
    }

   
}