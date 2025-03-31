using LAB02_DLL.Context;
using LAB02_DLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_01.Controllers
{
    public class CategoryController(FootballDbContext dbContext) : Controller
    {
        private readonly FootballDbContext _dbContext = dbContext;
        public IActionResult Index()
        {
            return View(_dbContext.Categories.AsNoTracking().ToList());
        }
        public IActionResult Add()
        {
            Category category = new Category();
            return View("Submit", category);
        }
        public IActionResult Edit(int id) {
            Category? category = _dbContext.Categories.FirstOrDefault(el => el.Id == id);
            if (category == null)
                return RedirectToAction("Index");
            return View("Submit", category);
        }
        public IActionResult Delete(int id)
        {
            Category? category = _dbContext.Categories.FirstOrDefault(el => el.Id == id);
            if (category == null)
                return RedirectToAction("Index");
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Submit(Category category)
        {
            _dbContext.Categories.Attach(category);
            var entry = _dbContext.Entry(category);
            entry.State = category.Id == 0 ? EntityState.Added : EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
