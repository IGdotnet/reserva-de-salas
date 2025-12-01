using Microsoft.AspNetCore.Mvc;
using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = await _usuarioService.GetAllAsync();
            return View(usuario);
        }

        public async Task<IActionResult> Details(long id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if(usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _usuarioService.CreateAsync(usuario);
                    return RedirectToAction(nameof(Index));
                }
                catch(InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id) 
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if(usuario == null)
            {
                return NotFound(); 
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Usuario usuario)
        {
            if(id != usuario.Id)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    await _usuarioService.UpdateAsync(usuario);
                    return RedirectToAction(nameof(Index));
                }
                catch(InvalidOperationException ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }

            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if(usuario == null)
            {
                return NotFound(); 
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _usuarioService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
