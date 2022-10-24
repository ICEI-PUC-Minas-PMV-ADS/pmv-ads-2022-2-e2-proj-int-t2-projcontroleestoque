using Microsoft.AspNetCore.Mvc;
using ProjControleEstoque.Context;
using ProjControleEstoque.Models;
using ProjControleEstoque.QueryParameters;
using System.Diagnostics;

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

        public IActionResult Index()
        {
            var products = _appDbContext.Products?.ToArray();
            ViewData["products"] = products;
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct([FromQuery] ProductParameters productParams)
        {
            Product product = new Product { Name = productParams.ProductName };

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