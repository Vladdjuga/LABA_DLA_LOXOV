using LAB02_DLL.Context;
using LAB02_DLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_01.Controllers
{
    public class EventTypeController(FootballDbContext dbContext) : Controller
    {
        FootballDbContext _dbContext = dbContext;
        public IActionResult Index()
        {
            return View(_dbContext.EventTypes.AsNoTracking().ToList());
        }
        public IActionResult Add()
        {
            EventType eventType = new EventType();
            return View("Submit", eventType);
        }

        public IActionResult Edit(int id)
        {
            EventType? eventType = _dbContext.EventTypes.FirstOrDefault(el => el.Id == id);
            if (eventType == null)
                return RedirectToAction("Index");
            return View("Submit", eventType);
        }

        public IActionResult Delete(int id)
        {
            EventType? eventType = _dbContext.EventTypes.FirstOrDefault(el => el.Id == id);
            if (eventType == null)
                return RedirectToAction("Index");
            _dbContext.EventTypes.Remove(eventType);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Submit(EventType eventType)
        {
            _dbContext.EventTypes.Attach(eventType);
            var entry = _dbContext.Entry(eventType);
            entry.State = eventType.Id == 0 ? EntityState.Added : EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
