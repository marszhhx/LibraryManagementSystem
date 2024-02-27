using LibraryManagement.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _dbContext; // Assuming AppDbContext is your database context class

        public CustomerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Show customer lists
        public IActionResult Index()
        {
            var customerViewModels = _dbContext.Customers
                .Select(a => new CustomerViewModel
                {
                    CustomerId = a.CustomerId,
                    CustomerName = a.Name
                }).ToList();

            return View(customerViewModels);
        }
        
        // Show one specific customer
        public IActionResult Edit(int id)
        {
            var customer = _dbContext.Customers.Find(id); // Fetch the customer from the database

            if (customer == null)
            {
                return NotFound(); // If the customer with the given id does not exist, return a Not Found response
            }
            
            CustomerViewModel viewModel = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.Name
            };

            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid) // Checks if the submitted data is valid
            {
                var customer = _dbContext.Customers.Find(model.CustomerId);
                if (customer == null)
                {
                    return NotFound();
                }

                customer.Name = model.CustomerName;
                // Update other fields as necessary

                _dbContext.SaveChanges(); // Saves changes to the database

                return RedirectToAction("Index"); // Redirects to the list of customers
            }
            return View(model); // If model state is invalid, return to the view with the current model to show validation errors
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Logic to delete the customer from the database
            // For example:
            var customer = _dbContext.Customers.FirstOrDefault(a => a.CustomerId == id); // Fetch the customer from the database

            if (customer == null)
            {
                // Handle the case where the customer is not found
                // You could return an appropriate response or view
                return NotFound();
            }
            
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
            
            // Redirect to the list view after deletion
            return RedirectToAction("Index"); // Assuming "Index" is the action method that shows the list of customers
        }
        

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Name = model.CustomerName
                };
    
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
        
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}