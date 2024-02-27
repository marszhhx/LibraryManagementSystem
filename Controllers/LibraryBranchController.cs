using LibraryManagement.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class LibraryBranchController : Controller
    {
        private readonly AppDbContext _dbContext;

        public LibraryBranchController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: LibraryBranch
        public IActionResult Index()
        {
            var libraryBranchViewModels = _dbContext.LibraryBranches
                .Select(a => new LibraryBranchViewModel
                {
                    BranchId = a.LibraryBranchId,
                    BranchName = a.BranchName
                }).ToList();

            return View(libraryBranchViewModels);
        }
        
        // GET: LibraryBranch/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibraryBranch/Create
        [HttpPost]
        public IActionResult Create(LibraryBranchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var branch = new LibraryBranch
                {
                    BranchName = model.BranchName
                };

                _dbContext.LibraryBranches.Add(branch);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: LibraryBranch/Edit/5
        public IActionResult Edit(int id)
        {
            var branch = _dbContext.LibraryBranches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            var viewModel = new LibraryBranchViewModel
            {
                BranchId = branch.LibraryBranchId,
                BranchName = branch.BranchName
            };

            return View(viewModel);
        }

        // POST: LibraryBranch/Edit/5
        [HttpPost]
        public IActionResult Edit(LibraryBranchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var branch = _dbContext.LibraryBranches.Find(model.BranchId);
                if (branch == null)
                {
                    return NotFound();
                }

                branch.BranchName = model.BranchName;
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: LibraryBranch/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var branch = _dbContext.LibraryBranches.FirstOrDefault(a => a.LibraryBranchId == id);
            if (branch == null)
            {
                return NotFound();
            }

            _dbContext.LibraryBranches.Remove(branch);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
