using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using reserva_de_salas.Models;
using reserva_de_salas.Services;

namespace reserva_de_salas.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ReservasFacede _facede;

        public ReservaController(ReservasFacede facede)
        {
            _facede = facede;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Indicadores = await _facede.GetIndicadoresAsync();
            return View(await _facede.ListarReservasAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = new SelectList(await _facede.ListarUsuarioAsync(), "Id", "Email");
            ViewBag.Salas = new SelectList(await _facede.ListarSalaAsync(), "Id", "Nome");
            var r = new Reserva
            {
                Date = System.DateTime.Today,
                HoraInicio = System.TimeSpan.FromHours(8),
                HoraFim = System.TimeSpan.FromHours(9),
            };
            return View(r);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(await _facede.ListarUsuarioAsync(), "Id", "Email", model.UsuarioId);
                ViewBag.Salas = new SelectList(await _facede.ListarSalaAsync(), "Id", "Nome", model.SalaId);
                return View(model);
            }

            var msg = await _facede.ReservarAsync(model);
            TempData[msg.Contains("sucesso") ? "SuccessMessage" : "ErrorMessage"] = msg;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var r = await _facede.GetIdAsync(id);
            ViewBag.Usuarios = new SelectList(await _facede.ListarUsuarioAsync(), "Id", "Email", r.UsuarioId);
            ViewBag.Salas = new SelectList(await _facede.ListarSalaAsync(), "Id", "Nome", r.SalaId);
            return View(r);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Reserva model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(await _facede.ListarUsuarioAsync(), "Id", "Email", model.UsuarioId);
                ViewBag.Salas = new SelectList(await _facede.ListarSalaAsync(), "Id", "Nome", model.SalaId);
                return View(model);
            }

            var msg = await _facede.AtualizarAsync(model);
            TempData[msg.Contains("sucesso") ? "SuccessMessage" : "ErrorMessage"] = msg;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var r = await _facede.GetIdAsync(id);
            return View(r);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _facede.DeleteAsync(id);
            TempData["SuccessMessage"] = "Reserva exlcuída com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(long id)
        {
            var r = await _facede.GetIdAsync(id);
            return View(r); 
        }
    }
}
