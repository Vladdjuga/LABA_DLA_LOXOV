﻿using Microsoft.AspNetCore.Mvc;

namespace LAB_01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
    }
}
