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
        public IActionResult Details(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(a => a.CustomerId == id); // Fetch the customer from the database

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
    }
}