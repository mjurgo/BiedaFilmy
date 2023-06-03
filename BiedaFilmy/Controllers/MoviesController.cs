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
using Microsoft.AspNetCore.Authorization;
using static System.Formats.Asn1.AsnWriter;

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

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Collections.Where(c => c.User == user))
                .Include(m => m.Roles)
                .Include(m => m.MovieComments)
                .ThenInclude(mc => mc.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var scores = _context.Collections.Where(c => c.MovieId == id && c.Mark != null).Select(c => c.Mark);
            if (scores.Count() != 0)
            {
                movie.Score = (float)((float)scores.Sum() / scores.Count());
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Editor, Admin")]
        public async Task<IActionResult> CreateMovieComment(MovieComment movieComment)
        {
            movieComment.User = await _userManager.GetUserAsync(HttpContext.User);
            movieComment.Created = DateTime.Now;

            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.Add(movieComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { Id = movieComment.MovieId });
            }
            var movie = _context.Movies.FirstOrDefaultAsync(m => m.Id == movieComment.MovieId);
            return View("Details", movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Editor, Admin")]
        public async Task<IActionResult> AddToCollection(Collection collection)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            collection.User = user;

            var existingCollectionCount = _context.Collections.Where(c => c.User == user && c.MovieId == collection.MovieId).Count();

            ModelState.Remove("User");

            if (collection.Mark != null && collection.Status == Enums.CollectionStatus.WantToSee)
            {
                collection.Status = Enums.CollectionStatus.Seen;
            }

            if (ModelState.IsValid)
            {
                _ = existingCollectionCount != 0 ? _context.Update(collection) : _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { Id = collection.MovieId});
            }
            var movie = _context.Movies.FirstOrDefault(m => m.Id == collection.MovieId);
            return View("Details", movie);
        }

        [Authorize(Roles = "User, Editor, Admin")]
        public async Task<IActionResult> DeleteCollectionEntry(int Id)
        {
            var collection = await _context.Collections.FirstOrDefaultAsync(c => c.Id == Id);
            if (collection != null)
            {
                _context.Collections.Remove(collection);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = collection.MovieId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovieComment(int Id)
        {
            var comment = await _context.MovieComments.FirstOrDefaultAsync(c => c.Id == Id);
            if (comment != null)
            {
                _context.MovieComments.Remove(comment);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = comment.MovieId });
        }

        private bool MovieExists(int id)
        {
          return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
