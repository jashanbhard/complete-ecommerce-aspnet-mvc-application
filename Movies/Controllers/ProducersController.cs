using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Data.Services;
using Movies.Models;

namespace Movies.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allproducers = await _service.GetAllAsync();
            return View(allproducers);
        }
        // Get/producer/Create
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //Get method to add producers details and id 1 
        public async Task<IActionResult> Details(int id)
        {
            var ProducerDetails = await _service.GetByIdAsync(id);
            if (ProducerDetails == null) return View("NotFound");
            return View(ProducerDetails);
        }

        //get/ producer Edit/1
        public IActionResult Edit(int id)
        {
            var ProducerUpdate = _service.GetByIdAsync(id);
            if (ProducerUpdate == null) return View("NotFound");
            return View(ProducerUpdate);
        }
        [HttpPost]
        public IActionResult Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            _service.UpdateAsync(id,producer);
            return RedirectToAction();
        }
    }
}
