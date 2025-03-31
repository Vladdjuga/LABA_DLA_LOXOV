using LAB02_DLL.Context;
using LAB02_DLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_01.Controllers
{
    public class TeamController : Controller
    {
        FootballDbContext _dbContext;
        public TeamController(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Teams.Include(t => t.League).ToList());
        }
        public IActionResult Add()
        {
            Team t = new Team();
            //t.Id=0;
            t.FoundingDate= System.DateTime.Now;
            ViewBag.Leagues = _dbContext.Leagues.ToList();
            return View("SubmitForm", t);
        }
        [HttpPost]
        public IActionResult Submit(Team team)
        {
            var entry = _dbContext.Entry(team);
            if (team.Id == 0)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                entry.State = EntityState.Modified;
            }

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Team? t = _dbContext.Teams.FirstOrDefault(e => e.Id == id);
            if (t == null) return RedirectToAction("Index");
            ViewBag.Leagues = _dbContext.Leagues.ToList();
            return View("SubmitForm", t);
        }
        public IActionResult Delete(int id)
        {
            Team? t = _dbContext.Teams.FirstOrDefault(e => e.Id == id);
            if (t == null) return RedirectToAction("Index");
            _dbContext.Remove(t);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
