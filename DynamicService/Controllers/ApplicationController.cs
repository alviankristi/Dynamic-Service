using System.Web.Mvc;
using DynamicService.ApplicationServices;

namespace DynamicService.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET: Application
        public ActionResult Index()
        {
            var hello = _applicationService.Hello("Alvian");
            return PartialView("Index",hello);
        }
    }
}