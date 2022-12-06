using Microsoft.AspNetCore.Mvc;
using ProjControleEstoque.Context;
using ProjControleEstoque.Utils;

namespace ProjControleEstoque.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly AppDbContext _appDbContext;

        public class MovimentacaoEstoqueParams
        {
            public string? startDate { get; set; } = null;
            public string? endDate { get; set; } = null;
            public string? type { get; set; } = Constants.ReportEntradaSaida_Type_InOut;
            public int offset { get; set; } = 0;
            public int limit { get; set; } = 1;
        }

        public class InventarioParams
        {
            public string? realizadoPor { get; set; } = null;
            public string? solicitadoPor { get; set; } = null;
            public string? criadoStartDate { get; set; } = null;
            public string? criadoEndDate { get; set; } = null;
            public string? executadoStartDate { get; set; } = null;
            public string? executadoEndDate { get; set; } = null;
            public int? offset { get; set; } = 0;
            public int? limit { get; set; } = 15;
        }

        public ReportsController(ILogger<ReportsController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult GenerateMovimentacaoEstoque([FromBody] MovimentacaoEstoqueParams? bodyParams) {
            var query = _appDbContext.movimentacaoEstoques?.AsQueryable();

            // Filtra pelo tipo de movimentação.
            if (bodyParams != null)
            {
                if (bodyParams.type == Constants.ReportEntradaSaida_Type_OnlyIn)
                {
                    query = query?.Where(x => x.Tipo == "Entrada");
                }
                else if (bodyParams.type == Constants.ReportEntradaSaida_Type_OnlyOut)
                {
                    query = query?.Where(x => x.Tipo == "Saida");
                }

                // Filtra por data
                if (
                    bodyParams.startDate != null &&
                    bodyParams.startDate.Trim().Length > 0
                ) {
                    var dateTimeStart = Utils.Utils.fixSearchStartDate(bodyParams.startDate);
                    query = query?.Where(x => x.DataMovimento >= dateTimeStart);
                }

                if (
                    bodyParams.endDate != null &&
                    bodyParams.endDate.Trim().Length > 0
                ) {
                    var dateTimeEnd = Utils.Utils.fixSearchEndDate(bodyParams.endDate);
                    query = query?.Where(x => x.DataMovimento <= dateTimeEnd);
                }
            }

            var totalCount = query?.Count();

            // Limita o filtro.
            var results = query?
                .Skip(bodyParams?.offset ?? 0)
                .Take(bodyParams?.limit ?? 15)
                .ToArray();

            // Carrega dados auxiliares.
            if (results != null)
            {
                foreach (var record in results)
                {
                    _appDbContext.Entry(record).Reference("Produto").Load();
                    _appDbContext.Entry(record).Reference("SolicitadoPor").Load();
                    _appDbContext.Entry(record).Reference("RegistradoPor").Load();
                }
            }

            return Ok(Json(new {
                status = 200,
                data = new
                {
                    results = results,
                    totalCount = totalCount,
                    count = results?.Length,
                    startDate = bodyParams?.startDate,
                    endDate = bodyParams?.endDate,
                    type = bodyParams?.type,
                    limit = bodyParams?.limit,
                    offset = bodyParams?.offset
                }
            }));
        }

        [HttpPost]
        public IActionResult GenerateInventario([FromBody] InventarioParams? bodyParams) {
            var q = _appDbContext.Inventarios?.AsQueryable();

            // Filtra por realizadoPor
            if (bodyParams != null)
            {
                if (
                    bodyParams?.realizadoPor != null &&
                    bodyParams?.realizadoPor.Trim().Length > 0
                ) {
                    var q_users = _appDbContext.Users?.Where(u => u.Nome.Contains(bodyParams.realizadoPor));
                    q = q_users?.Join(q, u => u.Id, inv => inv.RealizadoPorId, (u, inv) => inv);
                }

                // Filtra por solicitadoPor

                if (
                    bodyParams?.solicitadoPor != null &&
                    bodyParams?.solicitadoPor.Trim().Length > 0
                ) {
                    var q_users = _appDbContext.Users?.Where(u => u.Nome.Contains(bodyParams.solicitadoPor));
                    q = q_users?.Join(q, u => u.Id, inv => inv.SolicitadoPorId, (u, inv) => inv);
                }

                // Filtra por data de criação

                if (
                    bodyParams?.criadoStartDate != null &&
                    bodyParams?.criadoStartDate.Trim().Length > 0
                ) {
                    q = q?.Where(x => x.DataCriacao >= Utils.Utils.fixSearchStartDate(bodyParams.criadoStartDate));
                }

                if (
                    bodyParams?.criadoEndDate != null &&
                    bodyParams?.criadoEndDate.Trim().Length > 0
                ) {
                    q = q?.Where(x => x.DataCriacao <= Utils.Utils.fixSearchEndDate(bodyParams.criadoEndDate));
                }

                // Filtra por data de execução

                if (
                    bodyParams?.executadoStartDate != null &&
                    bodyParams?.executadoStartDate.Trim().Length > 0
                ) {
                    q = q?.Where(x => x.DataDeExecucao >= Utils.Utils.fixSearchStartDate(bodyParams.executadoStartDate));
                }

                if (
                    bodyParams?.executadoEndDate != null &&
                    bodyParams?.executadoEndDate.Trim().Length > 0
                ) {
                    q = q?.Where(x => x.DataDeExecucao <= Utils.Utils.fixSearchEndDate(bodyParams.executadoEndDate));
                }
            }

            var totalCount = q?.Count();

            // Limita o filtro.
            var results = q?.Skip(bodyParams?.offset ?? 0).Take(bodyParams?.limit ?? 15).ToArray();
            var mappedResults = new List<object>();

            if (results != null)
            {
                foreach (var record in results)
                {
                    var ItemsCount = _appDbContext.ItemInventarios?.Where(x => x.InventarioId == record.Id).Count();

                    _appDbContext.Entry(record).Reference(x => x.SolicitadoPor).Load();
                    _appDbContext.Entry(record).Reference(x => x.RealizadoPor).Load();

                    mappedResults.Add(new {
                        Id = record.Id,
                        SolicitadoPor = record.SolicitadoPor?.Nome,
                        RealizadoPor = record.RealizadoPor?.Nome,
                        DataCriacao = record.DataCriacao,
                        DataDeExecucao = record.DataDeExecucao,
                        Count = ItemsCount
                    });
                }
            }

            return Ok(Json(new {
                status = 200,
                data = new
                {
                    results = mappedResults,
                    totalCount = totalCount,
                    count = results?.Length,
                    realizadoPor = bodyParams?.realizadoPor,
                    solicitadoPor = bodyParams?.solicitadoPor,
                    criadoStartDate = bodyParams?.criadoStartDate,
                    criadoEndDate = bodyParams?.criadoEndDate,
                    executadoStartDate = bodyParams?.executadoStartDate,
                    executadoEndDate = bodyParams?.executadoEndDate,
                    limit = bodyParams?.limit,
                    offset = bodyParams?.offset
                }
            }));
        }
    }
}
