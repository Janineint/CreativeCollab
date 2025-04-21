using System.Collections.Generic;
using System.Linq;
using CreativeCollab.Models; 

namespace CreativeCollab.Data 
{
    public static class StaticProductData
    {
        // Static list initialized with data from SQLQuery-Product.sql
        private static readonly List<Product> _products = new List<Product>
        {
            // Data extracted from SQLQuery-Product.sql
            new Product { ProductId = 1, SupplierId = 1, ProductName = "Apples", UnitPrice = 3.50m, UnitSize = "kg", Category = "Fruits" },
            new Product { ProductId = 2, SupplierId = 1, ProductName = "Bananas", UnitPrice = 1.20m, UnitSize = "kg", Category = "Fruits" },
            new Product { ProductId = 3, SupplierId = 2, ProductName = "Salmon Fillet", UnitPrice = 12.00m, UnitSize = "kg", Category = "Seafood" },
            new Product { ProductId = 4, SupplierId = 2, ProductName = "Cod Fillet", UnitPrice = 10.00m, UnitSize = "kg", Category = "Seafood" },
            new Product { ProductId = 5, SupplierId = 3, ProductName = "Wheat Flour", UnitPrice = 2.80m, UnitSize = "kg", Category = "Bakery" },
            new Product { ProductId = 6, SupplierId = 3, ProductName = "Sugar", UnitPrice = 1.00m, UnitSize = "kg", Category = "Bakery" },
            new Product { ProductId = 7, SupplierId = 4, ProductName = "Chicken Breast", UnitPrice = 8.00m, UnitSize = "kg", Category = "Meat" },
            new Product { ProductId = 8, SupplierId = 4, ProductName = "Beef Steak", UnitPrice = 15.00m, UnitSize = "kg", Category = "Meat" },
            new Product { ProductId = 9, SupplierId = 5, ProductName = "Cheddar Cheese", UnitPrice = 5.00m, UnitSize = "kg", Category = "Dairy" },
            new Product { ProductId = 10, SupplierId = 5, ProductName = "Butter", UnitPrice = 4.00m, UnitSize = "kg", Category = "Dairy" },
            new Product { ProductId = 11, SupplierId = 6, ProductName = "Black Pepper", UnitPrice = 10.00m, UnitSize = "kg", Category = "Spices" },
            new Product { ProductId = 12, SupplierId = 6, ProductName = "Turmeric", UnitPrice = 8.00m, UnitSize = "kg", Category = "Spices" },
            new Product { ProductId = 13, SupplierId = 7, ProductName = "Olive Oil", UnitPrice = 7.00m, UnitSize = "litre", Category = "Oil" },
            new Product { ProductId = 14, SupplierId = 7, ProductName = "Vinegar", UnitPrice = 4.00m, UnitSize = "litre", Category = "Condiments" },
            new Product { ProductId = 15, SupplierId = 8, ProductName = "Lettuce", UnitPrice = 1.50m, UnitSize = "head", Category = "Vegetables" },
            new Product { ProductId = 146, SupplierId = 4, ProductName = "Rabbit Meat", UnitPrice = 18.00m, UnitSize = "kg", Category = "Meat" },
            new Product { ProductId = 147, SupplierId = 5, ProductName = "Swiss Cheese", UnitPrice = 6.00m, UnitSize = "kg", Category = "Dairy" },
            new Product { ProductId = 148, SupplierId = 5, ProductName = "Cottage Cheese", UnitPrice = 4.00m, UnitSize = "kg", Category = "Dairy" },
            new Product { ProductId = 149, SupplierId = 6, ProductName = "Bay Leaves", UnitPrice = 12.00m, UnitSize = "kg", Category = "Spices" },
            new Product { ProductId = 150, SupplierId = 6, ProductName = "Cloves", UnitPrice = 10.00m, UnitSize = "kg", Category = "Spices" },
            new Product { ProductId = 151, SupplierId = 7, ProductName = "Peanut Oil", UnitPrice = 9.00m, UnitSize = "litre", Category = "Oil" },
            new Product { ProductId = 152, SupplierId = 7, ProductName = "Garlic Sauce", UnitPrice = 4.50m, UnitSize = "litre", Category = "Condiments" },
            new Product { ProductId = 153, SupplierId = 8, ProductName = "Sweet Peppers", UnitPrice = 3.50m, UnitSize = "kg", Category = "Vegetables" },
            new Product { ProductId = 154, SupplierId = 8, ProductName = "Cucumbers", UnitPrice = 2.20m, UnitSize = "kg", Category = "Vegetables" },
            new Product { ProductId = 155, SupplierId = 9, ProductName = "Decaf Coffee", UnitPrice = 14.00m, UnitSize = "kg", Category = "Beverages" },
            new Product { ProductId = 156, SupplierId = 9, ProductName = "Chai Tea", UnitPrice = 10.00m, UnitSize = "box", Category = "Beverages" },
            new Product { ProductId = 157, SupplierId = 10, ProductName = "Gnocchi", UnitPrice = 4.00m, UnitSize = "kg", Category = "Pasta" },
            new Product { ProductId = 158, SupplierId = 10, ProductName = "Cannelloni", UnitPrice = 5.00m, UnitSize = "kg", Category = "Pasta" },
            new Product { ProductId = 159, SupplierId = 11, ProductName = "Cherries", UnitPrice = 4.50m, UnitSize = "kg", Category = "Fruits" },
            new Product { ProductId = 160, SupplierId = 11, ProductName = "Figs", UnitPrice = 5.00m, UnitSize = "kg", Category = "Fruits" },
            
        };

        // Method to get all products
        public static IEnumerable<Product> GetAllProducts()
        {
            // Return a copy to prevent external modification of the original list
            return _products.ToList();
        }

        // Optional: Method to get a single product by ID
        public static Product GetProductById(int productId)
        {
            return _products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}