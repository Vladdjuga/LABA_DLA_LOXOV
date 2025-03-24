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
        public IActionResult AddSubmitForm()
        {
            Team t = new Team();
            ViewBag.Leagues = _dbContext.Leagues.ToList();
            return View("SubmitForm", t);
        }
        public IActionResult Submit(Team team)
        {
            if(team.Id==0)
            {
                _dbContext.Add(team);
                _dbContext.SaveChangesAsync();
            }
            else
            {
                var t = _dbContext.Teams.FirstOrDefault(el => el.Id == team.Id);
                if(t==null)
                    return RedirectToAction("Index");
                t.Name = team.Name;
                t.Country = team.Country;
                t.City = team.City;
                t.FoundingDate = team.FoundingDate;
                t.LeagueId = team.LeagueId;
                _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
