using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_21032019.Models;

namespace Test_21032019.Controllers
{
 
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult About(string text)
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View("About");
        }

        //CREATE
        [HttpGet][Authorize]
        public ActionResult Osoba()
        { 
            Person p = new Person();
            return View(p);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Osoba(Person p, string opis)
        {
            if(ModelState.IsValid)
            {
                testowaEntities ent = new testowaEntities();
                uzytkownicy u = Person.ConvertToUzytkownicy(p);
                ent.uzytkownicies.Add(u);
                ent.SaveChanges();
                return RedirectToAction("ListaOsob");
            }
            return View(p);
        }

        public ActionResult Test()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult ListaOsob()
        {
            List<Person> pl = new List<Person>();
            testowaEntities ent = new testowaEntities();
            pl = Person.FromUzytkownicy(ent.uzytkownicies.ToList());
          
            return View(pl);
        }
    }
}