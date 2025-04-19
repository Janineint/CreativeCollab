using CreativeCollab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreativeCollab.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CreativeCollab.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var cars = _context.Cars.Include(c => c.User); // Include the User

            var viewModel = new CarListViewModel
            {
                Cars = cars
            };
            return View(viewModel);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars
                .Include(c => c.User) // Include the User
                .Include(c => c.CarCategories) // Include the linking table for Categories
                    .ThenInclude(cc => cc.Category) // Include the Category itself
                .Include(c => c.OrderVehicles) // Include the linking table for Orders
                    .ThenInclude(ov => ov.Order) // Include the Order itself
                .FirstOrDefaultAsync(m => m.CarId == id);

            if (car == null) return NotFound();

            return View(car);
        }

        // GET: Cars/Create
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access
        public IActionResult Create()
        {
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name");
            ViewBag.UserId = new SelectList(_context.Users.OrderBy(u => u.Name), "UserID", "Name");
            // Add ViewData for categories if handling M-M in the form
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access
        // Keeping [Bind] for Create to simplify basic property binding
        public async Task<IActionResult> Create([Bind("Make,Model,Year,UserID,User,ImageURL,Review")] Car car)
        {
            if (car.User == null && car.UserID > 0)
            {

                car.User = await _context.Users.FindAsync(car.UserID);

                if (car.User == null)
                {
                    ModelState.AddModelError("UserID", "Invalid Owner selected.");

                }
                else
                {
                   
                    ModelState.Remove("User"); 
                    TryValidateModel(car);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();

                // Handle adding selected Categories here if implemented in the form

                return RedirectToAction(nameof(Index));
            }
            ViewBag.UserId = new SelectList(_context.Users.OrderBy(u => u.Name), "UserID", "Name");
            return View(car);
        }

        // GET: Cars/Edit/5
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // Include existing relationships for the edit form (User, Categories)
            var car = await _context.Cars
               .Include(c => c.CarCategories)
                   .ThenInclude(cc => cc.Category)
               .FirstOrDefaultAsync(m => m.CarId == id);

            if (car == null) return NotFound();

            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", car.UserID);
            // Add ViewData for all categories and selected categories if handling M-M

            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access
        // Using TryUpdateModelAsync to handle scalar properties and relationships separately
        public async Task<IActionResult> Edit(int id /*, int[] selectedCategoryIds */)
        {
            var carToUpdate = await _context.Cars
                                    .Include(c => c.CarCategories) // Include existing categories
                                    .FirstOrDefaultAsync(c => c.CarId == id);

            if (carToUpdate == null) return NotFound();

            // Update scalar properties from the form data
            if (await TryUpdateModelAsync<Car>(
                carToUpdate,
                "", // prefix
                c => c.UserID, c => c.Make, c => c.Model, c => c.Year, c => c.ImageURL))
            {
                // Handle updating Categories here:
                // Compare selectedCategoryIds with existing carToUpdate.CarCategories
                // Add/Remove CarCategory entries in the context

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Simplified check
                    if (!_context.Cars.Any(e => e.CarId == id))
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

            // If model state is invalid or update failed, return to the edit view
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", carToUpdate.UserID);
            // Add ViewData for categories if needed
            return View(carToUpdate);
        }


        // GET: Cars/Delete/5
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var car = await _context.Cars
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CarId == id);
            return car == null ? NotFound() : View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Assumes entity exists
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car); // Removed null check

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}