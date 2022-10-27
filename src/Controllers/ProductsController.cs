using Microsoft.AspNetCore.Mvc;
using ProjControleEstoque.Context;
using ProjControleEstoque.Data.bodies;
using ProjControleEstoque.Models;
using ProjControleEstoque.Utils;

namespace ProjControleEstoque.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public ProductsController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index([FromQuery] int Offset = 0, [FromQuery] int Limit = 20)
        {
            var products = _appDbContext
                .Products?
                    .Skip(Offset)
                    .Take(Math.Min(50, Limit))                    
                    .ToArray();

            ViewData["products"] = products;
            ViewData["pagination_totalCount"] = _appDbContext.Products?.Count();
            ViewData["pagination_offset"] = Offset;
            ViewData["pagination_pageCount"] = Limit;

            return View();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int productId) {
            var product = _appDbContext.Products?.First(x => x.Id == productId);
            return Ok(Json(new { status = 200, product = product }));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _appDbContext.Products?.ToArray();
            return Ok(Json(new { status = 200, product = product }));
        }
    }
}
