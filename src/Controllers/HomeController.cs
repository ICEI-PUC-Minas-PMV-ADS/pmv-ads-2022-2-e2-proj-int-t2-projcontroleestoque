using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjControleEstoque.Context;
using ProjControleEstoque.Models;
using ProjControleEstoque.QueryParameters;
using System.Diagnostics;
using System.Text.Json;

namespace ProjControleEstoque.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDBContext;
        private readonly IHttpContextAccessor _httpContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
            _logger = logger;
            _appDBContext = appDbContext;
        }

        /***
         * Mostra a pagina para o usuario.
         */

        [HttpGet]
        public IActionResult Index()
        {
            var userStr = _httpContext?.HttpContext?.Session.GetString("User");
            if (userStr == null)
            {
                return RedirectToAction("Login", "Users");
            }
            ViewData["user"] = JsonSerializer.Deserialize<User>(userStr);

            if (userStr != null)
            {
                var startDate = DateTime.Today.AddDays(1);
                var endDate = DateTime.Today.AddDays(-1);

                var inventario = _appDBContext.Inventarios?.Where(x =>
                    x.DataDeExecucao >= startDate && x.DataDeExecucao <= endDate).FirstOrDefault();

                // Verifica se ha inventario agendado.
                var agendaInventarios = _appDBContext.AgendaInventarios?.Where(x => x.InventarioId == null).ToArray();
                var precisaExecutarInventario = Utils.Utils.verificarAgendamentoInvetario(agendaInventarios);

                if (precisaExecutarInventario && inventario == null)
                {
                    ViewData["has_inventory"] = true;
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}