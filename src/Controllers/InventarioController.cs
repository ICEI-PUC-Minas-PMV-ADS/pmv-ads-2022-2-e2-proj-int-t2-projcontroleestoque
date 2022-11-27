using Microsoft.AspNetCore.Mvc;
using ProjControleEstoque.Context;
using ProjControleEstoque.Models;
using System.Text.Json;

namespace ProjControleEstoque.Controllers
{
    public class InventarioController : Controller
    {
        public class InventarioParams
        {
            public string? tipo { get; set; }

            public string? semanal_value { get; set; }

            public string? mensal_value { get; set; }
            public Boolean proximo_dia_util { get; set; }

            public string? extraordinario_value { get; set; }            
        }

        public class RegistrarParams
        {
            public int productId { get; set; }
            public int quantidade { get; set; }
            public string? status { get; set; }
            public string? observacao { get; set; }
        }

        private readonly ILogger<InventarioController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _context;

        public InventarioController(ILogger<InventarioController> logger, AppDbContext appDbContext, IHttpContextAccessor context)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _context = context;
        }

        public IActionResult Index()
        {
            var userStr = _context?.HttpContext?.Session.GetString("User");
            if (userStr == null || userStr.Trim().Length == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            var user = JsonSerializer.Deserialize<User>(userStr);
            if (user == null)
            {
                return RedirectToAction("Login", "Users");
            }

            if (user.Funcao != "administrador")
            {
                return RedirectToAction("FazerInventario");
            }
            var results = _appDbContext.AgendaInventarios?.ToArray();

            if (results != null)
            {
                foreach (var r in results)
                {
                    _appDbContext.Entry(r).Reference(x => x.SolicitadoPor).Load();
                    _appDbContext.Entry(r).Reference(x => x.Inventario).Load();
                }
                ViewData["results"] = results;
            }
            return View();
        }

        public IActionResult FazerInventario()
        {
            var userStr = _context?.HttpContext?.Session.GetString("User");
            if (userStr == null || userStr.Trim().Length == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            var agendaInventarios = _appDbContext.AgendaInventarios?.Where(x => x.InventarioId == null).ToArray();
            ViewData["redirect"] = Utils.Utils.verificarAgendamentoInvetario(agendaInventarios);
            
            return View();
        }

        [HttpPost]
        public IActionResult Registrar([FromBody] RegistrarParams[]? bodyParams)
        {
            var userStr = _context?.HttpContext?.Session.GetString("User");
            if (userStr == null || userStr.Trim().Length == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            var user = JsonSerializer.Deserialize<User>(userStr);
            if (user == null)
            {
                return RedirectToAction("Login", "Users");
            }

            // Verifica se ha inventario agendado.
            var agendaInventarios = _appDbContext.AgendaInventarios?.Where(x => x.InventarioId == null).ToArray();
            var agendaInventario = Utils.Utils.getAgendaInventario(agendaInventarios);

            if (agendaInventario == null)
            {
                return Ok(Json(new { status = 500, message = "Não há nenhuma execução de inventário agendada!" }));
            }

            var inventario = new Inventario();
            inventario.SolicitadoPorId = agendaInventario?.SolicitadoPorId;
            inventario.RealizadoPorId = user.Id;
            inventario.DataDeExecucao = DateTime.Today;
            inventario.DataCriacao = DateTime.Today;

            inventario = _appDbContext.Add(inventario).Entity;
            _appDbContext.SaveChanges();

            if (bodyParams != null)
            {
                foreach (var itemInventarioInfo in bodyParams)
                {
                    var itemInventario = new ItemInventario();
                    itemInventario.InventarioId = inventario.Id;
                    itemInventario.ProdutoId = itemInventarioInfo.productId;
                    itemInventario.Quantidade = itemInventarioInfo.quantidade;
                    itemInventario.Status = itemInventarioInfo.status;
                    itemInventario.Observacao = itemInventarioInfo.observacao;
                    _appDbContext.Add(itemInventario);
                }
                _appDbContext.SaveChanges();
            }

            if (agendaInventario != null)
            {
                agendaInventario.InventarioId = inventario.Id;
                _appDbContext.Update(agendaInventario);
                _appDbContext.SaveChanges();
            }

            return Ok(Json(new
            {
                status = 200,
                data = new { }
            }));
        }

        [HttpPost]
        public IActionResult SchedulyInventory([FromBody] InventarioParams? bodyParams)
        {
            var userStr = _context?.HttpContext?.Session.GetString("User");
            if (userStr == null || userStr.Trim().Length == 0)
            {
                return Ok(Json(new
                {
                    status = 500,
                    data = new
                    {
                        message = "Falha ao recuperar dados do usuario logado!"
                    },
                }));
            }

            var user = JsonSerializer.Deserialize<User>(userStr);
            if (user == null || user.Funcao != "administrador")
            {
                return Ok(Json(new
                {
                    status = 500,
                    data = new
                    {
                        message = "Você não tem permissão para fazer esta operação!"
                    },
                }));
            }

            AgendaInventario agenda = new AgendaInventario();

            agenda.Tipo = bodyParams?.tipo;
            agenda.Agendamento = generateAgendamentoQuery(bodyParams);
            agenda.SolicitadoPorId = user.Id;

            var createdAgenda = _appDbContext.Add(agenda);
            _appDbContext.SaveChanges();

            return Ok(Json(new {
                status = 200,
                data = bodyParams,
            }));
        }

        private string generateAgendamentoQuery(InventarioParams? bodyParams)
        {
            switch (bodyParams?.tipo)
            {
                case "periodico_semanal":
                    return generateAgendamentoSemanal(bodyParams.semanal_value ?? "");

                case "periodico_mensal":
                    return generateAgendamentoMensal(bodyParams.mensal_value ?? "", bodyParams.proximo_dia_util);

                case "extraordinario":
                    return generateAgendamentoOrtraordinario(bodyParams.extraordinario_value ?? "");

                default:
                    break;
            }
            return "";
        }

        private string generateAgendamentoSemanal(string value)
        {
            return JsonSerializer.Serialize(new { value = value });
        }

        private string generateAgendamentoMensal(string value, bool proximoDiaUtil)
        {
            return JsonSerializer.Serialize(new { proximoDiaUtil = proximoDiaUtil, value = value });
        }

        private string generateAgendamentoOrtraordinario(string value)
        {
            return JsonSerializer.Serialize(new { value = value });
        }
    }
}
