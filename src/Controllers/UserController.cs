using Microsoft.AspNetCore.Mvc;
using ProjControleEstoque.Context;
using ProjControleEstoque.Models;
using ProjControleEstoque.Models.Data;
using ProjControleEstoque.QueryParameters;
using System.Diagnostics;

namespace ProjControleEstoque.Controllers
{


    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _appDbContext;


        public UserController(ILogger<UserController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public IActionResult PostRet([FromForm] User user)
        {
            if(user.Email == null || user.Email.Trim().Length == 0)
            {
                ViewData["error"] =new FieldError { field="Email", message="Este Campo deve ser Preenchido." };
                return View("Index");

            } 

            _appDbContext.Add(user);
            _appDbContext.SaveChanges();
            return View("Index");

        }

        [HttpPost]
        public IActionResult ValidaEmail([FromBody] ValidaEmailBody body)
        {
            if (body == null || body.Email.Trim().Length == 0)
            {
                return Ok(Json(new {status = 500,message = "Parametro de Entrada é invalido."}));

            }

            var x = _appDbContext.Users?.Where((x) => x.Email == body.Email ).FirstOrDefault();
            if (x != null)
                return Ok(Json(new { status = 500, message = "Esse Email já esta em uso." }));

            return Ok(Json(new { status = 200}));

        }

        public IActionResult Index([FromQuery] string nome)
        {

            ViewData["message"] = nome;
            return View();
        }



        
    }
}