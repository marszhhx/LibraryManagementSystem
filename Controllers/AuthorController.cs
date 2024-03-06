using LibraryManagement.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AuthorController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Authors
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

        // GET: Author/Edit/5
        public IActionResult Edit(int id)
        {
            
            try
            {
                var author = _dbContext.Authors.Find(id);
                if (author == null)
                {
                    return NotFound();
                }

                var viewModel = new AuthorViewModel
                {
                    AuthorId = author.AuthorId,
                    AuthorName = author.Name
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while edit the author. Please try again.");

                // Redirect to a safe page or return a view that displays an error message
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Author/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public IActionResult Create(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var author = new Author
                    {
                        Name = model.AuthorName
                    };

                    _dbContext.Authors.Add(author);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Optionally, add an error message to the ModelState to display in the view
                    ModelState.AddModelError("", "An error occurred while creating the author. Please try again.");

                    // Return the view with the model to display the error message
                    return View(model);
                }
            }

            return View(model);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public IActionResult Edit(AuthorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var author = _dbContext.Authors.Find(model.AuthorId);
                    if (author == null)
                    {
                        return NotFound();
                    }

                    author.Name = model.AuthorName;
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Optionally, add an error message to the ModelState to display in the view
                ModelState.AddModelError("", "An error occurred while creating the author. Please try again.");

                // Return the view with the model to display the error message
                return View(model);
            }

            return View(model);
        }

        // POST: Author/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var author = _dbContext.Authors.FirstOrDefault(a => a.AuthorId == id);
                if (author == null)
                {
                    return NotFound();
                }

                _dbContext.Authors.Remove(author);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below and replace it with your logging mechanism)
                // Log.Error(ex, "An error occurred while deleting the author with ID {AuthorId}", id);

                // Optionally, you can add a more specific error message if needed
                ModelState.AddModelError("", "An error occurred while deleting the author. Please try again.");

                // Redirect to the index action or return a view to display an error message
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
