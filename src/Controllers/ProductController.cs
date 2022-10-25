using Microsoft.AspNetCore.Mvc;

namespace ProjControleEstoque.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
