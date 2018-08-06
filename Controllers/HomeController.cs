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
            return RedirectToAction("Ninjas");
        }

        [HttpGet("dojos")]
        public IActionResult Dojos()
        {
            ViewBag.Dojos = dojoFactory.FindAll();
            return View();
        }

        [HttpPost("processdojo")]
        public IActionResult ProcessDojo(Dojo data)
        {
            if(ModelState.IsValid)
            {
                dojoFactory.Add(data);
                return RedirectToAction("Dojos");
            }
            return RedirectToAction("Dojos");
        }


        [HttpGet("ninja/{id}")]
        public IActionResult Ninja(int id)
        {
            ViewBag.Ninja = ninjaFactory.FindByID(id);
            return View();
        }

        [HttpGet("dojo/{id}")]
        public IActionResult Dojo(int id)
        {
            ViewBag.Dojo = ninjaFactory.NinjasWithDojo(id);
            return View();
        }

        [HttpGet("banish/{id}")]
        public IActionResult Banish(int id)
        {
            ViewBag.Dojo = ninjaFactory.Banish(id);
            return RedirectToAction("Dojo", id);
        }

        [HttpGet("recruit/{id}/{dojoid}")]
        public IActionResult Recruit(int id, int dojoid)
        {
            ViewBag.Dojo = ninjaFactory.Recruit(id, dojoid);
            return RedirectToAction("Dojo", id);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
