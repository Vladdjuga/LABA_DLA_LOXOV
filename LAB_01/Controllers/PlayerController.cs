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
            p.BirthDate= System.DateTime.Now;
            ViewBag.Teams = _dbContext.Teams.ToList();
            ViewBag.Positions = _dbContext.Positions.ToList();
            return View("SubmitForm", p);
        }

        public async Task<IActionResult> Submit(Player player, int[] positions)
        {
            _dbContext.Attach(player).State = player.Id == 0 ? EntityState.Added : EntityState.Modified;
            player.Positions = await _dbContext.Positions
                .Where(p => positions.Contains(p.Id))
                .ToListAsync();
            await _dbContext.SaveChangesAsync();
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

        public IActionResult Remove(int id)
        {
            Player? p = _dbContext.Players
                .FirstOrDefault(el => el.Id == id);
            if (p == null)
                return RedirectToAction("Index");
            _dbContext.Players.Remove(p);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
