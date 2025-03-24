using LAB02_DLL.Context;
using LAB02_DLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_01.Controllers
{
    public class PlayerController : Controller
    {
        private readonly FootballDbContext _dbContext;

        public PlayerController(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Players.Include(p => p.Team).Include(p => p.Positions).ToList());
        }

        public IActionResult AddSubmitForm()
        {
            Player p = new Player();
            ViewBag.Teams = _dbContext.Teams.ToList();
            ViewBag.Positions = _dbContext.Positions.ToList();
            return View("SubmitForm", p);
        }

        public async Task<IActionResult> Submit(Player player, int[] Positions)
        {
            if (player.Id == 0)
            {
                player.Positions = _dbContext.Positions.Where(p => Positions.Contains(p.Id)).ToList();
                _dbContext.Add(player);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                var p = await _dbContext.Players.FirstOrDefaultAsync(el => el.Id == player.Id);
                if (p == null)
                    return RedirectToAction("Index");

                p.FirstName = player.FirstName;
                p.LastName = player.LastName;
                p.Country = player.Country;
                p.BirthDate = player.BirthDate;
                p.TeamId = player.TeamId;
                p.Positions = player.Positions;
                _dbContext.Update(p);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Player? p = await _dbContext.Players.Include(p => p.Positions)
                .FirstOrDefaultAsync(el => el.Id == id);
            if (p == null)
                return RedirectToAction("Index");
            ViewBag.Teams = _dbContext.Teams.ToList();
            ViewBag.Positions = _dbContext.Positions.ToList();
            return View("SubmitForm", p);
        }

        public async Task<IActionResult> Remove(int id)
        {
            Player? p = await _dbContext.Players
                .FirstOrDefaultAsync(el => el.Id == id);
            if (p == null)
                return RedirectToAction("Index");
            _dbContext.Players.Remove(p);
            _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
