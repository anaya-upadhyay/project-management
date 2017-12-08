using System.Web.Mvc;

namespace ProjectManagement.Api.Http.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View("Index");
        }
    }
}