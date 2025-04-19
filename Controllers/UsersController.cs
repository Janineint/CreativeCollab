using CreativeCollab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreativeCollab.ViewModels;

namespace CreativeCollab.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            var viewModel = new UserListViewModel
            {
                Users = users
            };

            return View(viewModel); // Pass the ViewModel to the view
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.Cars) // Include the cars owned by the user
                .FirstOrDefaultAsync(m => m.UserID == id);

            if (user == null) return NotFound();

            return View(user);
        }

        // GET: Users/Create
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can create
        public IActionResult Create() => View();

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can create
        public async Task<IActionResult> Create(User user) // Removed [Bind] as per example format
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can edit
        public async Task<IActionResult> Edit(int id, User user) // Removed [Bind] as per example format
        {
            if (id != user.UserID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Simplified check - assume if concurrency issue, it's because it was deleted
                    if (!_context.Users.Any(e => e.UserID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Re-throw if it's another concurrency issue
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);
            return user == null ? NotFound() : View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Consider adding role authorization if only admins can delete
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Assumes entity exists because Delete GET was successful
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user); // Removed null check as per example

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}