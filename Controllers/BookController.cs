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

        
        
        public IActionResult Details(int id)
        {
            // // Simulated data access
            // Book book = new Book
            // {
            //     BookId = id,
            //     Title = "Sample Book",
            //     AuthorId = 1,
            //     LibraryBranchId = 1
            // };
            //
            // Author author = new Author
            // {
            //     AuthorId = 1,
            //     Name = "John Doe"
            // };
            //
            // LibraryBranch branch = new LibraryBranch
            // {
            //     LibraryBranchId = 1,
            //     BranchName = "Main Branch"
            // };
            
            // Fetch the book from the database including its related Author and LibraryBranch
            
            // First, get the book with the specified ID
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
    }
}