using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjControleEstoque.Context;
using ProjControleEstoque.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Newtonsoft.Json;

namespace ProjControleEstoque.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _appDBcontext;
        private readonly IHttpContextAccessor _httpContext;

        
        public UsersController(AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _appDBcontext = context;
            _httpContext = httpContextAccessor;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Nome","Hash")] User user)
        {
            
            var usuario = await _appDBcontext.Users.FirstOrDefaultAsync(m => m.Nome == user.Nome);            

            if (usuario != null)
            {
                var validaSenha = BCrypt.Net.BCrypt.Verify(user.Hash, usuario.Hash);
                if (validaSenha)
                {
                    usuario.Hash = null;

                    _httpContext?.HttpContext?.Session.SetString("User", JsonSerializer.Serialize(usuario));

                    var startDate = DateTime.Today.AddDays(1);
                    var endDate = DateTime.Today.AddDays(-1);

                    var inventario = _appDBcontext.Inventarios?.Where(x =>
                        x.DataDeExecucao >= startDate && x.DataDeExecucao <= endDate).FirstOrDefault();

                    // Verifica se ha inventario agendado.
                    var agendaInventarios = _appDBcontext.AgendaInventarios?.ToArray();
                    var precisaExecutarInventario = Utils.Utils.verificarAgendamentoInvetario(agendaInventarios);

                    if (precisaExecutarInventario && inventario == null)
                    {
                        ViewData["has_inventory"] = true;
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Message = "Login Inválido senha ou usuário incorretos";
            return View();           
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            // Validação de Login.
            User user = new User();
            var userStr = _httpContext.HttpContext.Session.GetString("User");
            if (userStr != null)
                user = JsonConvert.DeserializeObject<User>(userStr);
            if (userStr == null )
            {
                return RedirectToAction("Login", "Users");
            }
            if(user.Funcao != "administrador")
            {
                return RedirectToAction("Login", "Users");
            }

            return View(await _appDBcontext.Users.ToListAsync());
        }
       
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _appDBcontext.Users == null)
            {
                return NotFound();
            }

            var user = await _appDBcontext.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Logout()
        {
            _httpContext.HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");

        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Hash,Funcao,DataCadastro,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Hash = BCrypt.Net.BCrypt.HashPassword(user.Hash);
                _appDBcontext.Add(user);
                await _appDBcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _appDBcontext.Users == null)
            {
                return NotFound();
            }

            var user = await _appDBcontext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Hash,Funcao,DataCadastro,Email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Hash = BCrypt.Net.BCrypt.HashPassword(user.Hash);
                    _appDBcontext.Update(user);
                    await _appDBcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _appDBcontext.Users == null)
            {
                return NotFound();
            }

            var user = await _appDBcontext.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_appDBcontext.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            var user = await _appDBcontext.Users.FindAsync(id);
            if (user != null)
            {
                _appDBcontext.Users.Remove(user);
            }
            
            await _appDBcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return _appDBcontext.Users.Any(e => e.Id == id);
        }
    }
}
