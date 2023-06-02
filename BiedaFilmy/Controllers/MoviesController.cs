using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiedaFilmy.Data;
using BiedaFilmy.Models;
using Microsoft.AspNetCore.Identity;

namespace BiedaFilmy.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MoviesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Movies
        [HttpGet("movies")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movies.Include(m => m.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieComments)
                .ThenInclude(mc => mc.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public async Task<IActionResult> CreateMovieComment(MovieComment movieComment)
        {
            movieComment.User = await _userManager.GetUserAsync(HttpContext.User);
            movieComment.Created = DateTime.Now;

            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.Add(movieComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), movieComment.Movie);
            }
            return View(movieComment.Movie);
        }

        private bool MovieExists(int id)
        {
          return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
