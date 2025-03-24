using LAB02_DLL.Context;
using LAB02_DLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_01.Controllers
{
    public class ArticleController(FootballDbContext footballDbContext) : Controller
    {
        private FootballDbContext _dbContext = footballDbContext;
        public IActionResult Index()
        {
            return View(_dbContext.Articles
                .Include(el=>el.Author)
                .Include(el=>el.Category)
                .Include(el=>el.Match)
            .ToList()
            );
        }

        public IActionResult Add()
        {
            Article a = new Article();
            ViewBag.Authors = _dbContext.Authors.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Matches = _dbContext.Matches.ToList();
            return View("SubmitForm",a);
        }
        public IActionResult Edit(int id) {
            Article? a=_dbContext.Articles
                .Include(el=>el.Author)
                .Include(el=>el.Category)
                .Include(el=>el.Match)
                .FirstOrDefault(el=>el.Id == id);
            if (a == null)
                return RedirectToAction("Index");
            ViewBag.Authors = _dbContext.Authors.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Matches = _dbContext.Matches.ToList();
            return View("SubmitForm", a);
        }

        public IActionResult Delete(int id)
        {
            Article? a = _dbContext.Articles
                .FirstOrDefault(el => el.Id == id);
            if (a == null)
                return RedirectToAction("Index");
            _dbContext.Remove(a);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Submit(Article article)
        {
            if (article.Id == 0)
            {
                _dbContext.Add(article);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Article? a= _dbContext.Articles
                    .FirstOrDefault(el => el.Id == article.Id);
                if (a == null)
                    return RedirectToAction("Index");
                a.Title = article.Title;
                a.Content = article.Content;
                a.AuthorId = article.AuthorId;
                a.CategoryId = article.CategoryId;
                a.MatchId = article.MatchId;
                _dbContext.Update(a);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
