using LibraryManagement.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class LibraryBranchController : Controller
    {
        private readonly AppDbContext _dbContext; // Assuming AppDbContext is your database context class

        public LibraryBranchController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        

        // Show Library lists
        public IActionResult Index()
        {
            var libraryBranchViewModels = _dbContext.LibraryBranches
                .Select(a => new LibraryBranchViewModel()
                {
                    BranchId = a.LibraryBranchId,
                    BranchName = a.BranchName
                }).ToList();

            return View(libraryBranchViewModels);
        }
        
        // Show one specific library branch
        public IActionResult Details(int id)
        {
            // // Simulated data access
            // LibraryBranch branch = new LibraryBranch
            // {
            //     LibraryBranchId = 1,
            //     BranchName = "Main Branch"
            // };
            var branch = _dbContext.LibraryBranches.FirstOrDefault(a => a.LibraryBranchId == id); // Fetch the customer from the database

            if (branch == null)
            {
                return NotFound(); // If the customer with the given id does not exist, return a Not Found response
            }
            
            LibraryBranchViewModel viewModel = new LibraryBranchViewModel()
            {
                BranchId = branch.LibraryBranchId,
                BranchName = branch.BranchName
            };

            return View(viewModel);
        }
    }
}