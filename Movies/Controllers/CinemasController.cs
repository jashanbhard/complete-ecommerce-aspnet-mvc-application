using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Data.Services;
using Movies.Models;

namespace Movies.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _Service;
        public CinemasController(ICinemaService service)
        {
            _Service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allcinemas = await _Service.GetAllAsync();
            return View(allcinemas);
        }
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _Service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _Service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _Service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _Service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _Service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDetails = await _Service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            await _Service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
