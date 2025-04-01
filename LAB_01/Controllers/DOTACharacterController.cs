using LAB02_DLL.Context;
using LAB02_DLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_01.Controllers
{
    public class DOTACharacterController(FootballDbContext dbContext) : Controller
    {
        public IActionResult Index()
        {
            return View(dbContext.DOTACharacters);
        }

        public IActionResult Add()
        {
            ViewBag.Types = new string[] { "Strength", "Agility", "Intelligence" };
            ViewBag.Players = dbContext.Players.AsNoTracking().ToList();
            ViewBag.Positions = dbContext.Positions.AsNoTracking().ToList();
            return View("Submit", new DOTACharacter());
        }

        public IActionResult Edit(int id)
        {
            DOTACharacter? d = dbContext.DOTACharacters
                .Include(d => d.Players)
                .Include(d => d.Position)
                .FirstOrDefault(d => d.Id == id);
            if(d==null)
                return RedirectToAction("Index");
            ViewBag.Types = new string[] { "Strength", "Agility", "Intelligence" };
            ViewBag.Players = dbContext.Players.AsNoTracking().ToList();
            ViewBag.Positions = dbContext.Positions.AsNoTracking().ToList();
            return View("Submit", d);
        }

        public IActionResult Delete(int id)
        {
            DOTACharacter? d = dbContext.DOTACharacters
                .FirstOrDefault(d => d.Id == id);
            if(d==null)
                return RedirectToAction("Index");
            dbContext.DOTACharacters.Remove(d);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Submit(DOTACharacter dotaCharacter, int[] Players)
        {
            dbContext.DOTACharacters.Attach(dotaCharacter)
                .State=dotaCharacter.Id==0?EntityState.Added:EntityState.Modified;
            dotaCharacter.Players= dbContext.Players
                .Where(p => Players.Contains(p.Id))
                .ToList();
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
