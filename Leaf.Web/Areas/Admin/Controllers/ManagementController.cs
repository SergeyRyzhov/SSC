using System.Web.Mvc;

namespace Leaf.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagementController : Controller
    {
        //
        // GET: /Admin/Management/
        public ActionResult Index()
        {
            return View();
        }
    }
}