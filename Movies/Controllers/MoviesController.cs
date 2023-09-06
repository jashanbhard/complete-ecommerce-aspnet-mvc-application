using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Microsoft.EntityFrameworkCore;
using Movies.Data.Services;
using Movies.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allmovies = await _service.GetAllAsync(n => n.Cinema);
            return View(allmovies);
        }
        // below method is to search the movie
        public async Task<IActionResult> Filter(string searchString)
        {
            var allmovies = await _service.GetAllAsync(n => n.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allmovies.Where(n => n.Name.ToLower().Contains(searchString)||n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }
            
            return View("Index", allmovies);
        }
        //get: Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsynch(id);
            return View(movieDetails);
        }
        //Get: Movie/Create
        public async Task<IActionResult> Create()
        {
            var DropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(DropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(DropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(DropdownsData.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var DropdownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(DropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(DropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(DropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.AddnewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //Get: Movie/Edit/1(id)
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsynch(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),

            };

            var DropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(DropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(DropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(DropdownsData.Actors, "Id", "FullName");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var DropdownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(DropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(DropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(DropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
