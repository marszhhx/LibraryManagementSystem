using LibraryManagement.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _dbContext; // Assuming AppDbContext is your database context class

        public AuthorController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            var authorViewModels = _dbContext.Authors
                .Select(a => new AuthorViewModel
                {
                    AuthorId = a.AuthorId,
                    AuthorName = a.Name
                }).ToList();

            return View(authorViewModels);
        }
        
        public IActionResult Details(int id)
        {
            // // Simulated data access
            // Author author = new Author
            // {
            //     AuthorId = 1,
            //     Name = "John Doe"
            // };
            var author = _dbContext.Authors.FirstOrDefault(a => a.AuthorId == id); // Fetch the author from the database

            if (author == null)
            {
                return NotFound(); // If the author with the given id does not exist, return a Not Found response
            }

            AuthorViewModel viewModel = new AuthorViewModel
            {
                AuthorId = author.AuthorId,
                AuthorName = author.Name
            };

            return View(viewModel); // Pass the AuthorViewModel to the view
        }
    }
}