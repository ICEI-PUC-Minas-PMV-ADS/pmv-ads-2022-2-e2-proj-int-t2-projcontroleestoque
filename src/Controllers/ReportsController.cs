<<<<<<< Updated upstream
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjControleEstoque.Context;
using ProjControleEstoque.Utils;
=======
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjControleEstoque.Context;
using ProjControleEstoque.Utils;
using System;
using System.Globalization;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
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
=======
        public IActionResult GenerateMovimentacaoEstoque(
            [FromQuery] string? startDate,
            [FromQuery] string? endDate,
            [FromQuery] string? type = Constants.ReportEntradaSaida_Type_InOut,
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 15
        ) {
            var query = _appDbContext.movimentacaoEstoques?.AsQueryable();

            // Filtra pelo tipo de movimentação.
            if (type == Constants.ReportEntradaSaida_Type_OnlyIn)
            {
                query = query?.Where(x => x.Tipo == "Entrada");
            }
            else if (type == Constants.ReportEntradaSaida_Type_OnlyOut)
            {
                query = query?.Where(x => x.Tipo == "Saida");
            }

            // Filtra por data
            if (startDate != null && endDate != null)
            {
                var dateTimeStart = Utils.Utils.fixSearchStartDate(startDate);
                var dateTimeEnd = Utils.Utils.fixSearchEndDate(endDate);

                query = query?.Where(x => x.DataMovimento >= dateTimeStart && x.DataMovimento <= dateTimeEnd);
            }
            else if (startDate != null)
            {
                var dateTimeStart = Utils.Utils.fixSearchStartDate(startDate);

                query = query?.Where(x => x.DataMovimento >= dateTimeStart);
            }
            else if (endDate != null)
            {
                var dateTimeEnd = Utils.Utils.fixSearchEndDate(endDate);

                query = query?.Where(x => x.DataMovimento <= dateTimeEnd);
            }

            var totalCount = query?.Count();

            // Limita o filtro.
            var results = query?
                .Skip(offset)
                .Take(limit)
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
                    startDate = startDate,
                    endDate = endDate,
                    type = type,
                    limit = limit,
                    offset = offset
                }
            }));
>>>>>>> Stashed changes
        }
    }
}
