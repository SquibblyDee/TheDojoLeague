using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheDojoLeague.Models;
using DapperApp.Factories;

namespace TheDojoLeague.Controllers
{
    public class HomeController : Controller
    {

        private readonly DojoFactory dojoFactory;
        private readonly NinjaFactory ninjaFactory;
        public HomeController()
        {
            dojoFactory = new DojoFactory();
            ninjaFactory = new NinjaFactory();
        }

        [HttpGet("ninjas")]
        public IActionResult Ninjas()
        {
            ViewBag.Ninjas = ninjaFactory.NinjasWithDojos();
            return View();
        }

        [HttpPost("processninja")]
        public IActionResult ProcessNinja(Ninja data)
        {
            if(ModelState.IsValid)
            {
                ninjaFactory.Add(data);
                return RedirectToAction("Ninjas");
            }
            return View("Ninjas");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
