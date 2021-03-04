using System.Web.Mvc;

namespace DocumentRegistration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Sobre a aplicação:";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Informações:";

            return View();
        }
    }
}