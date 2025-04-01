using System.Reflection;
using LAB02_DLL.Context;
using LAB02_DLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_01.Controllers
{
    public class PlayerController(FootballDbContext dbContext) : Controller
    {
        public IActionResult Index()
        {
            return View(dbContext.Players.Include(p => p.Team).Include(p => p.Positions).ToList());
        }

        public IActionResult AddSubmitForm()
        {
            Player p = new Player();
            p.BirthDate= System.DateTime.Now;
            ViewBag.Teams = dbContext.Teams.ToList();
            ViewBag.Positions = dbContext.Positions.ToList();
            return View("SubmitForm", p);
        }

        public async Task<IActionResult> Submit(Player player, int[] positions)
        {
            dbContext.Submit(player);
            dbContext.AttachConnection<Player, Position>(player,
                await dbContext.Positions
                .Where(p => positions.Contains(p.Id))
                .ToListAsync(), player.GetType().GetProperty("Positions"));
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Player? p = await dbContext.Players.Include(p => p.Positions)
                .FirstOrDefaultAsync(el => el.Id == id);
            if (p == null)
                return RedirectToAction("Index");
            ViewBag.Teams = dbContext.Teams.ToList();
            ViewBag.Positions = dbContext.Positions.ToList();
            return View("SubmitForm", p);
        }

        public IActionResult Remove(int id)
        {
            Player? p = dbContext.Players
                .FirstOrDefault(el => el.Id == id);
            if (p == null)
                return RedirectToAction("Index");
            dbContext.Players.Remove(p);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
