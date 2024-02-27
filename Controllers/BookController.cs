using LibraryManagement.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _dbContext; // Assuming AppDbContext is your database context class

        public BookController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            // Query to fetch all books with their related author and library branch names
            var bookList = _dbContext.Books
                .ToList() // Fetch all books first to avoid querying inside a loop
                .Select(book => new
                {
                    Book = book,
                    AuthorName = _dbContext.Authors.FirstOrDefault(a => a.AuthorId == book.AuthorId)?.Name,
                    BranchName = _dbContext.LibraryBranches.FirstOrDefault(lb => lb.LibraryBranchId == book.LibraryBranchId)?.BranchName
                })
                .ToList(); // Materialize query to work with in-memory

            // Project into BookViewModel list
            var bookViewModels = bookList.Select(b => new BookViewModel
            {
                BookId = b.Book.BookId,
                Title = b.Book.Title,
                AuthorName = b.AuthorName ?? "Unknown Author",
                BranchName = b.BranchName ?? "Unknown Branch"
            }).ToList();

            return View(bookViewModels);
        }
        
        public IActionResult Edit(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            
            // Then, find the related author and library branch using the IDs stored in the book
            var author = _dbContext.Authors.FirstOrDefault(a => a.AuthorId == book.AuthorId);
            var branch = _dbContext.LibraryBranches.FirstOrDefault(lb => lb.LibraryBranchId == book.LibraryBranchId);

            // Construct the BookViewModel with the information retrieved
            BookViewModel viewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorName = author != null ? author.Name : "Unknown Author", // Handle case where author might not be found
                BranchName = branch != null ? branch.BranchName : "Unknown Branch" // Handle case where branch might not be found
            };

            // Pass the constructed viewModel to the view
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult Edit(BookViewModel model)
        {
            if (ModelState.IsValid) // Checks if the submitted data is valid
            {
                var book = _dbContext.Books.Find(model.BookId);
                if (book == null)
                {
                    return NotFound();
                }
                
                // Lookup or create the Author based on AuthorName
                var author = _dbContext.Authors.FirstOrDefault(a => a.Name == model.AuthorName);
                if (author == null)
                {
                    // Author not found, create a new one
                    author = new Author { Name = model.AuthorName };
                    _dbContext.Authors.Add(author);
                    // Note: The new Author's ID will be generated when SaveChanges() is called
                }
                
                var branch = _dbContext.LibraryBranches.FirstOrDefault(lb => lb.BranchName == model.BranchName);
                if (branch == null)
                {
                    // LibraryBranch not found, create a new one
                    branch = new LibraryBranch { BranchName = model.BranchName };
                    _dbContext.LibraryBranches.Add(branch);
                    // Note: The new LibraryBranch's ID will be generated when SaveChanges() is called
                }
                
                _dbContext.SaveChanges(); // This call updates the database and sets IDs for new entities

                book.Title = model.Title;
                book.AuthorId = author.AuthorId;
                book.LibraryBranchId = branch.LibraryBranchId;

                _dbContext.SaveChanges(); // Saves changes to the database

                return RedirectToAction("Index"); // Redirects to the list of customers
            }
            return View(model); // If model state is invalid, return to the view with the current model to show validation errors
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Book/Create
        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Lookup or create the Author based on AuthorName
                var author = _dbContext.Authors.FirstOrDefault(a => a.Name == model.AuthorName);
                if (author == null)
                {
                    // Author not found, create a new one
                    author = new Author { Name = model.AuthorName };
                    _dbContext.Authors.Add(author);
                    // Note: The new Author's ID will be generated when SaveChanges() is called
                }
                
                // Lookup or create the LibraryBranch based on BranchName
                var branch = _dbContext.LibraryBranches.FirstOrDefault(lb => lb.BranchName == model.BranchName);
                if (branch == null)
                {
                    // LibraryBranch not found, create a new one
                    branch = new LibraryBranch { BranchName = model.BranchName };
                    _dbContext.LibraryBranches.Add(branch);
                    // Note: The new LibraryBranch's ID will be generated when SaveChanges() is called
                }

                _dbContext.SaveChanges(); // This call updates the database and sets IDs for new entities

                // Now that we have an author and branch (either existing or new),
                // we can create the book with references to them
                var book = new Book
                {
                    Title = model.Title,
                    AuthorId = author.AuthorId, // This will be set for both new and existing authors
                    LibraryBranchId = branch.LibraryBranchId, // This will be set for both new and existing branches
                };

                _dbContext.Books.Add(book);
                _dbContext.SaveChanges(); // This call updates the database and sets IDs for new entities

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Logic to delete the customer from the database
            // For example:
            var book = _dbContext.Books.FirstOrDefault(a => a.BookId == id); // Fetch the customer from the database

            if (book == null)
            {
                // Handle the case where the customer is not found
                // You could return an appropriate response or view
                return NotFound();
            }
            
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            
            // Redirect to the list view after deletion
            return RedirectToAction("Index"); // Assuming "Index" is the action method that shows the list of customers
        }
    }
    
}