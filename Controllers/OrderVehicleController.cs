using CreativeCollab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreativeCollab.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CreativeCollab.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    // This controller manages the M-M link between Orders and Cars
    public class OrderVehicleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderVehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderVehicle/ListForOrder/5
        // Displays the cars linked to a specific order
        public async Task<IActionResult> ListForOrder(int? orderId)
        {
            if (orderId == null) return NotFound("Order ID is required.");

            var order = await _context.Orders
                .Include(o => o.OrderVehicles)
                    .ThenInclude(ov => ov.Car) // Include the Car details
                .FirstOrDefaultAsync(o => o.TransactionId == orderId);

            if (order == null) return NotFound($"Order with ID {orderId} not found.");

            ViewBag.Order = order; // Pass the order to the view
            return View(order.OrderVehicles.ToList()); // Pass the list of linking entries
        }

        // GET: OrderVehicle/AddCarToOrder/5
        // Displays a form to add a car to a specific order
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access if needed
        public IActionResult AddCarToOrder(int? orderId)
        {
            if (orderId == null) return NotFound("Order ID is required.");

            // Check if the order exists
            if (!_context.Orders.Any(o => o.TransactionId == orderId)) return NotFound($"Order with ID {orderId} not found.");

            ViewBag.TransactionId = orderId;
            // Provide a list of Cars to select from
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "Make"); // Or display make/model

            return View();
        }

        // POST: OrderVehicle/AddCarToOrder
        // Handles the form submission to add a car to an order
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access if needed
        // Keeping [Bind] to explicitly get the necessary link properties from the form
        public async Task<IActionResult> AddCarToOrder([Bind("OrderId,CarId,UnitPrice")] OrderVehicle orderVehicle)
        {
            // Set OrderVehicleId to default so EF Core generates a new one
            orderVehicle.OrderVehicleId = 0;

            if (ModelState.IsValid)
            {
                // Check if the specific OrderVehicle link already exists
                var existingLink = await _context.OrderVehicles
                    .AnyAsync(ov => ov.OrderId == orderVehicle.OrderId && ov.CarId == orderVehicle.CarId);

                if (existingLink)
                {
                    ModelState.AddModelError("", "This car is already linked to this order.");
                    // Reload SelectList if returning to the view
                    ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "Make", orderVehicle.CarId);
                    ViewBag.OrderId = orderVehicle.OrderId;
                    return View(orderVehicle);
                }

                _context.OrderVehicles.Add(orderVehicle);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ListForOrder), new { orderId = orderVehicle.OrderId });
            }

            // If model state is invalid, return to the view with errors
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "Make", orderVehicle.CarId);
            ViewBag.OrderId = orderVehicle.OrderId;
            return View(orderVehicle);
        }

        // GET: OrderVehicle/RemoveCarFromOrder/5
        // Displays a confirmation to remove a specific OrderVehicle link
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access if needed
        public async Task<IActionResult> RemoveCarFromOrder(int? id) // This ID is the OrderVehicleId
        {
            if (id == null) return NotFound();

            var orderVehicle = await _context.OrderVehicles
                .Include(ov => ov.Order) // Include Order details for display
                .Include(ov => ov.Car)   // Include Car details for display
                .FirstOrDefaultAsync(m => m.OrderVehicleId == id);

            return orderVehicle == null ? NotFound() : View(orderVehicle); // Use a view to confirm removal
        }

        // POST: OrderVehicle/RemoveCarFromOrder/5
        // Handles the confirmation to remove the link
        [HttpPost, ActionName("RemoveCarFromOrder")]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Add this attribute for admin-only access if needed
        public async Task<IActionResult> RemoveCarFromOrderConfirmed(int id) // This ID is the OrderVehicleId
        {
            // Assumes entity exists
            var orderVehicle = await _context.OrderVehicles.FindAsync(id);
            var orderId = orderVehicle.OrderId; // Get the order ID before removing
            _context.OrderVehicles.Remove(orderVehicle); // Removed null check

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListForOrder), new { orderId = orderId });
        }
    }
}