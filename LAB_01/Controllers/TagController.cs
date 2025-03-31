using LAB02_DLL.Context;
using LAB02_DLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_01.Controllers
{
    public class TagController(FootballDbContext footballDbContext) : Controller
    {
        private FootballDbContext _dbContext = footballDbContext;

        public IActionResult Index()
        {
            return View(_dbContext.Tags.Include(el=>el.Articles).ToList());
        }
        public IActionResult Add()
        {
            Tag tag = new Tag();
            return View("SubmitForm", tag);
        }

        public IActionResult Edit(int id)
        {
            Tag? tag = _dbContext.Tags.FirstOrDefault(el => el.Id == id);
            if (tag == null)
                return RedirectToAction("Index");
            return View("SubmitForm", tag);
        }

        public IActionResult Delete(int id)
        {
            Tag? tag = _dbContext.Tags.FirstOrDefault(el => el.Id == id);
            if (tag == null)
                return RedirectToAction("Index");
            _dbContext.Tags.Remove(tag);
            _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Submit(Tag tag)
        {
            _dbContext.Attach(tag).State=tag.Id==0?EntityState.Added:EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
