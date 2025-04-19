using CreativeCollab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreativeCollab.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories
                .Include(c => c.CarCategories) // Include the linking table
                    .ThenInclude(cc => cc.Car) // Include the related Cars
                .FirstOrDefaultAsync(m => m.CategoryID == id);

            if (category == null) return NotFound();

            return View(category);
        }

        // GET: Categories/Create
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can create
        public IActionResult Create() => View();

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can create
        public async Task<IActionResult> Create(Category category) // Removed [Bind] as per example format
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var category = await _context.Categories.FindAsync(id);
            return category == null ? NotFound() : View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can edit
        public async Task<IActionResult> Edit(int id, Category category) // Removed [Bind] as per example format
        {
            if (id != category.CategoryID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Simplified check
                    if (!_context.Categories.Any(e => e.CategoryID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Re-throw
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryID == id);
            return category == null ? NotFound() : View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can delete
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Assumes entity exists
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category); // Removed null check

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}