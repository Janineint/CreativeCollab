# Delivery Management System (DMS)

## Project Overview

This project is a **Delivery Management System (DMS)** designed to facilitate the management of delivery orders, associated vehicles (cars), users (drivers/owners), customers, stores, and products involved in the delivery process. It provides a web-based interface built with **C#**, **ASP.NET Core MVC**, and **Entity Framework Core**, utilizing **SQL Server** for data persistence and **ASP.NET Core Identity** for secure user authentication and authorization.

The system allows for creating orders, assigning specific users and vehicles to those orders, managing vehicle details (including images and reviews), and tracking related entities like stores and products.

---

## Core Concepts & Entities

* **Order:** Represents a delivery request, potentially linked to a store, customer, a primary user (ApplicationUser), a primary car, and containing multiple order line items (OrderDetails).
* **User:** Represents simple user records (e.g., drivers, owners) linked to cars. Managed via `UsersController`. (Distinguished from ApplicationUser for login).
* **Car:** Represents vehicles used for deliveries, linked to a specific User (owner), and potentially categories. Includes details like Make, Model, Year, ImageURL, and Review. Managed via `CarsController`.
* **OrderVehicle:** A join entity explicitly linking an Order, a Car, and a User, allowing for tracking which specific user used which specific car for a given order.
* **ApplicationUser:** Represents login accounts managed by ASP.NET Core Identity. Linked directly to Orders.
* **OrderDetail:** Represents a line item within an Order, linking to a Product and specifying quantity and price.
* **Product:** Represents items that can be included in an order.
* **Store:** Represents store branches or locations associated with orders.
* **Category:** Represents categories for organizing entities (e.g., Car Categories).
* **Customer:** Represents customer records (distinct from Users/ApplicationUsers).
* **Supplier:** Represents supplier information (potentially for products).

---

## Technology Stack

* **Backend:** C#, ASP.NET Core MVC (.NET 9 specified in project files)
* **Data Access:** Entity Framework Core (EF Core)
* **Database:** SQL Server (or compatible database configured via connection string)
* **Authentication:** ASP.NET Core Identity
* **Frontend:** Razor Views, HTML, CSS (Bootstrap 5), JavaScript (jQuery, jQuery Validate)
* **IDE:** Visual Studio 2022+ recommended

---

## Features

* **Order Management:**
    * Create, View (Details), Edit, and Delete Orders.
    * Assign a specific User (owner/driver) and Car to an Order during creation/editing.
    * Dynamically filter the Car selection dropdown based on the selected User using AJAX.
    * Manage Order line items (Products and Quantities).
    * Associate Orders with Stores.
* **User Management (Simple Users):**
    * Create, View, Edit, Delete basic User records (distinct from login accounts).
* **Car Management:**
    * Create, View, Edit, Delete Car records.
    * Associate Cars with specific Users (Owners).
    * Store and display Car images (via URL) and reviews.
    * Manage Car-Category relationships (Many-to-Many).
* **Store Management:** CRUD operations for store branches.
* **Product Management:** CRUD operations for products.
* **Supplier Management:** CRUD operations for suppliers.
* **Customer Management:** CRUD operations for customers.
* **Category Management:** CRUD operations for categories.
* **Authentication & Authorization:** Secure user login and registration using ASP.NET Core Identity. Role-based authorization can be implemented.
* **Database Management:** Utilizes EF Core migrations for database schema management. Includes optional SQL scripts for initial data seeding.

---

## Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download) (.NET 9.0 or as specified in `global.json`/`.csproj`)
* [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (e.g., Express, Developer Edition) or another compatible database.
* A compatible IDE, such as [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/).
* [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) or similar database management tool.

---

## Setup Instructions

1.  **Clone the Repository:**
    ```bash
    # Replace with your actual repository URL if different
    git clone [https://github.com/Janineint/cms.git](https://github.com/Janineint/cms.git) 
    cd InventoryManagement 
    # Or cd DeliveryManagementSystem if you rename the folder
    ```

2.  **Configure Connection String:**
    * Open `appsettings.json` (and potentially `appsettings.Development.json`).
    * Locate the `ConnectionStrings` section.
    * Update the `DefaultConnection` string to point to your SQL Server instance and choose a database name (e.g., `DeliveryManagementDb`). Ensure the user credentials or integrated security settings are correct for your environment.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DeliveryManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true" 
      // Or: "Server=your_server_name;Database=DeliveryManagementDb;User ID=your_user;Password=your_password;TrustServerCertificate=True;"
    }
    ```

3.  **Open the Project:**
    * Open the `InventoryManagement.sln` (or renamed `.sln`) file in Visual Studio.

4.  **Apply Database Migrations:**
    * Open the Package Manager Console (`Tools` > `NuGet Package Manager` > `Package Manager Console`) or use a terminal in the project directory (`InventoryManagement`).
    * Run the following EF Core command to apply migrations and create/update the database schema:
        ```bash
        dotnet ef database update
        ```
    * *(Optional: If you need to add new migrations based on model changes)*
        ```bash
        # Make sure your DbContext and connection string are correct first
        dotnet ef migrations add MeaningfulMigrationName 
        dotnet ef database update
        ```

5.  **Seed Initial Data (Optional):**
    * The original README mentioned SQL scripts (`SQLQuery-Product.sql`, `SQLQuery-Suppliers.sql`, `SQLQuery-Orders.sql`).
    * If these are still relevant and contain initial data you need, open them in SSMS (or your SQL client), connect to the database you created (`DeliveryManagementDb`), and execute the scripts.

6.  **Build and Run:**
    * **Using Visual Studio:** Press `F5` or `Ctrl+F5` to build and run the application.
    * **Using .NET CLI:**
        ```bash
        dotnet build
        dotnet run
        ```
    * The application should now be running and accessible via the URLs specified in `Properties/launchSettings.json` (e.g., `https://localhost:7XXX` and `http://localhost:5XXX`). You may need to register an initial user through the application's UI.

---

## API Endpoints / Controllers

This application follows the standard ASP.NET Core MVC pattern. Key controllers and their typical responsibilities include:

* **`/Home`**:
    * `GET /Home/Index`: Show homepage.
    * `GET /Home/Privacy`: Show privacy policy.
    * `GET /Home/Error`: Show error page.
* **`/Orders`**:
    * `GET /Orders/Index`: Get all orders.
    * `GET /Orders/Details/{id}`: Get an order by ID.
    * `GET /Orders/Create`: Show form to create a new order.
    * `POST /Orders/Create`: Create a new order.
    * `GET /Orders/Edit/{id}`: Show form to update an existing order.
    * `POST /Orders/Edit/{id}`: Update an existing order.
    * `GET /Orders/Delete/{id}`: Show confirmation page to delete an order.
    * `POST /Orders/Delete/{id}`: Delete an order.
    * `GET /Orders/GetCarsByOwner`: (AJAX endpoint) Gets cars associated with a specific user ID.
* **`/Cars`**:
    * `GET /Cars/Index`: Get all cars.
    * `GET /Cars/Details/{id}`: Get a car by ID.
    * `GET /Cars/Create`: Show form to create a new car.
    * `POST /Cars/Create`: Create a new car.
    * `GET /Cars/Edit/{id}`: Show form to update an existing car.
    * `POST /Cars/Edit/{id}`: Update an existing car.
    * `GET /Cars/Delete/{id}`: Show confirmation page to delete a car.
    * `POST /Cars/Delete/{id}`: Delete a car.
* **`/Users`**: (Manages simple User records)
    * `GET /Users/Index`: Get all users.
    * `GET /Users/Details/{id}`: Get a user by ID.
    * `GET /Users/Create`: Show form to create a new user.
    * `POST /Users/Create`: Create a new user.
    * `GET /Users/Edit/{id}`: Show form to update an existing user.
    * `POST /Users/Edit/{id}`: Update an existing user.
    * `GET /Users/Delete/{id}`: Show confirmation page to delete a user.
    * `POST /Users/Delete/{id}`: Delete a user.
* **`/Store`**:
    * `GET /Store/Index`: Get all stores.
    * `GET /Store/Details/{id}`: Get a store by ID.
    * `GET /Store/Create`: Show form to create a new store.
    * `POST /Store/Create`: Create a new store.
    * `GET /Store/Edit/{id}`: Show form to update an existing store.
    * `POST /Store/Edit/{id}`: Update an existing store.
    * `GET /Store/Delete/{id}`: Show confirmation page to delete a store.
    * `POST /Store/Delete/{id}`: Delete a store.
* **`/Products`**:
    * `GET /Products/Index`: Get all products.
    * `GET /Products/Details/{id}`: Get a product by ID.
    * `GET /Products/Create`: Show form to create a new product.
    * `POST /Products/Create`: Create a new product.
    * `GET /Products/Edit/{id}`: Show form to update an existing product.
    * `POST /Products/Edit/{id}`: Update an existing product.
    * `GET /Products/Delete/{id}`: Show confirmation page to delete a product.
    * `POST /Products/Delete/{id}`: Delete a product.
* **`/Suppliers`**:
    * `GET /Suppliers/Index`: Get all suppliers.
    * `GET /Suppliers/Details/{id}`: Get a supplier by ID.
    * `GET /Suppliers/Create`: Show form to create a new supplier.
    * `POST /Suppliers/Create`: Create a new supplier.
    * `GET /Suppliers/Edit/{id}`: Show form to update an existing supplier.
    * `POST /Suppliers/Edit/{id}`: Update an existing supplier.
    * `GET /Suppliers/Delete/{id}`: Show confirmation page to delete a supplier.
    * `POST /Suppliers/Delete/{id}`: Delete a supplier.
* **`/Customers`**:
    * `GET /Customers/Index`: Get all customers.
    * `GET /Customers/Details/{id}`: Get a customer by ID.
    * `GET /Customers/Create`: Show form to create a new customer.
    * `POST /Customers/Create`: Create a new customer.
    * `GET /Customers/Edit/{id}`: Show form to update an existing customer.
    * `POST /Customers/Edit/{id}`: Update an existing customer.
    * `GET /Customers/Delete/{id}`: Show confirmation page to delete a customer.
    * `POST /Customers/Delete/{id}`: Delete a customer.
* **`/Categories`**:
    * `GET /Categories/Index`: Get all categories.
    * `GET /Categories/Details/{id}`: Get a category by ID.
    * `GET /Categories/Create`: Show form to create a new category.
    * `POST /Categories/Create`: Create a new category.
    * `GET /Categories/Edit/{id}`: Show form to update an existing category.
    * `POST /Categories/Edit/{id}`: Update an existing category.
    * `GET /Categories/Delete/{id}`: Show confirmation page to delete a category.
    * `POST /Categories/Delete/{id}`: Delete a category.
* **(Identity Endpoints)**: Located under `/Identity/Account/...` for Register, Login, Logout, etc.


---


## License

Distributed under the MIT License. See `LICENSE` file for more information (if one exists).
