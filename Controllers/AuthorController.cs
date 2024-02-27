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
                var author = new Author
                {
                    Name = model.AuthorName
                };

                _dbContext.Authors.Add(author);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        
        // POST: Author/Edit/5
        [HttpPost]
        public IActionResult Edit(AuthorViewModel model)
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
            return View(model);
        }

        // POST: Author/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
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
    }
}
