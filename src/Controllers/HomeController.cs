using Microsoft.AspNetCore.Mvc;
using ProjControleEstoque.Context;
using ProjControleEstoque.Models;
using ProjControleEstoque.QueryParameters;
using System.Diagnostics;
using System.IO;

namespace ProjControleEstoque.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index([FromQuery] int? productId)
        {
            if (productId != null)
            {
                var product = _appDbContext.Products?.Where(x => x.Nome == "Pano de prato").First();
                ViewData["product"] = product;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct([FromQuery] ProductParameters productParams)
        {
            Product product = new Product { Nome = productParams.ProductName };

            _appDbContext.Add(product);
            _appDbContext.SaveChanges();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}