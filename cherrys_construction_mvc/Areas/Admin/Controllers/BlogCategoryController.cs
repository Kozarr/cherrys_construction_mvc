using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    public class BlogCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
