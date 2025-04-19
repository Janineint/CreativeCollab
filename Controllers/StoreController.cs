using CreativeCollab.Models;
using CreativeCollab.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreativeCollab.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var stores = await _context.Stores
                    .Include(c => c.Orders)
                    .ToListAsync();

            var viewModel = stores.Select(c => new StoreListViewModel
            {
                StoreID = c.StoreID,
                StoreName = c.StoreName,
                LastOrderDate = c.Orders.OrderByDescending(o => o.OrderDate).FirstOrDefault()?.OrderDate,
                LastOrderPrice = c.Orders.OrderByDescending(o => o.OrderDate).FirstOrDefault()?.TotalCost ?? 0
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var store = await _context.Stores.FindAsync(id);
            return store == null ? NotFound() : View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Store store)
        {
            if (id != store.StoreID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var store = await _context.Stores.FirstOrDefaultAsync(m => m.StoreID == id);
            return store == null ? NotFound() : View(store);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var store = await _context.Stores
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(m => m.StoreID == id);
            return store == null ? NotFound() : View(store);
        }
    }
}
