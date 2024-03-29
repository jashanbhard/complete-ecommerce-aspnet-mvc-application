﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;

namespace Movies.Controllers
{
    public class CinemasController : Controller
    {
        private readonly AppDbContext _context;
        public CinemasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task <IActionResult> Index()
        {
            var allcinemas = await _context.Cinemas.ToListAsync();
            return View(allcinemas);
        }
    }
}
