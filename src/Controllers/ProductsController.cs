using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjControleEstoque.Context;
using ProjControleEstoque.Data.bodies;
using ProjControleEstoque.Models;
using ProjControleEstoque.Utils;
using System.Xaml.Permissions;

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
            var product = _appDbContext.Products?.Where(x => x.Id == productId).First();
            return Ok(Json(new { status = 200, product = product }));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _appDbContext.Products?.ToArray();
            return Ok(Json(new { status = 200, product = product }));
        }

        [HttpGet]
        public IActionResult QueryProduct([FromQuery] string q, [FromQuery] int Offset = 0, [FromQuery] int Limit = 20)
        {
            if (q != null && q.Length >= 3)
            {
                var queryProducts = _appDbContext.Products?
                    .Join(_appDbContext.Suppliers, p => p.FornecedorId, s => s.Id, (p, s) => new
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Descricao = p.Descricao,
                        Quantidade = p.Quantidade,
                        Localizacao = p.Localizacao,
                        Tags = p.Tags,
                        EstrategiaConsumo = p.EstrategiaConsumo,
                        Criado = p.Criado,
                        Validade = p.Validade,
                        Fornecedor = s.Nome
                    })
                    .Where(x =>
                        x.Nome.Contains(q) || x.Descricao.Contains(q) || x.Tags.Contains(q) || x.Fornecedor.Contains(q)
                    )
                    .Skip(Offset)
                    .Take(Limit)
                    .ToArray();

                return Ok(Json(new {
                    status = 200,
                    products = queryProducts,
                    totalCount = queryProducts?.Length,
                    offset = Offset,
                    pageCount = Limit
                }));
            }

            var allProducts = _appDbContext.Products?.Take(Limit).Skip(Offset).ToArray();
            return Ok(Json(new {
                status = 200,
                products = allProducts,
                totalCount = allProducts?.Length,
                offset = Offset,
                pageCount = Limit
            }));
        }
    }
}
