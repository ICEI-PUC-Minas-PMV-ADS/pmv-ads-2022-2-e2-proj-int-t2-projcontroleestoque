using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjControleEstoque.Context;
using ProjControleEstoque.Utils;

namespace ProjControleEstoque.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly AppDbContext _appDbContext;

        public ReportsController(ILogger<ReportsController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetEntradaSaida(
            [FromQuery] string? startDate,
            [FromQuery] string? endDate,
            [FromQuery] string? type = Constants.ReportEntradaSaida_Type_InOut
        ) {
            if (type == Constants.ReportEntradaSaida_Type_InOut)
            {
                var query = _appDbContext.movimentacaoEstoques?.AsQueryable();
                
                if (startDate != null && endDate != null)
                {
                    var dateTimeStart = DateTime.Parse(startDate);
                    var dateTimeEnd = DateTime.Parse(endDate);
                    
                    query
                        .Where(x => x.DataMovimento >= dateTimeStart && x.DataMovimento <= dateTimeEnd)
                        .Include(x => x.Produto)
                        .Include(x => x.SolicitadoPor)
                        .Include(x => x.RegistradoPor);
                    
                    ViewData["reportData"] = query.ToArray();
                }
                else if (startDate != null)
                {
                    var dateTimeStart = DateTime.Parse(startDate);
                    
                    query
                        .Where(x => x.DataMovimento >= dateTimeStart)
                        .Include(x => x.Produto)
                        .Include(x => x.SolicitadoPor)
                        .Include(x => x.RegistradoPor);

                    ViewData["reportData"] = query.ToArray();
                }
                else if (endDate != null)
                {
                    var dateTimeEnd = DateTime.Parse(endDate);
                    
                    query
                        .Where(x => x.DataMovimento <= dateTimeEnd)
                        .Include(x => x.Produto)
                        .Include(x => x.SolicitadoPor)
                        .Include(x => x.RegistradoPor);

                    ViewData["reportData"] = query.ToArray();
                }
                else
                {
                    var result = query
                        .Include(x => x.Produto)
                        .Include(x => x.SolicitadoPor)
                        .Include(x => x.RegistradoPor)
                        .ToArray();

                    ViewData["reportData"] = result;
                }
                ViewData["formTitle"] = "Entrada e saida de produtos";
            }
            else if (type == Constants.ReportEntradaSaida_Type_OnlyIn)
            {
                ViewData["formTitle"] = "Entrada de produtos";
            }
            else if (type == Constants.ReportEntradaSaida_Type_OnlyOut)
            {
                ViewData["formTitle"] = "Saida de produtos";
            }
            return View();
        }
    }
}
