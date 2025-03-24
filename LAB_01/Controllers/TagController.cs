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
            tag.Id = 0;
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
            if (tag.Id == 0)
            {
                _dbContext.Tags.Add(tag);
                _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var t = _dbContext.Tags.FirstOrDefault(el => el.Id == tag.Id);
                if (t == null)
                    return RedirectToAction("Index");
                t.Name = tag.Name;
                _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
