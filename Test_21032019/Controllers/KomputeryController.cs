using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_21032019.Models;

namespace Test_21032019.Controllers
{
    public class KomputeryController : Controller
    {
        // GET: Komputery
        public ActionResult Index()
        {
            return View("ListaKomputerow");
        }
        //LISTA KOMPUTERÓW
        [HttpGet]

        public ActionResult ListaKomputerow()
        {
            List<Komputer> komp = new List<Komputer>();
            testowaEntities ent = new testowaEntities();
            komp = Komputer.fromKomputery(ent.komputeries.ToList());
            return View(komp);
        }
        //CREATE
        [HttpGet]
        public ActionResult Create()
        {
            Komputer k = new Komputer();
            return View(k);
        }
        [HttpPost]
            public ActionResult Create(Komputer k)
        {
            if (ModelState.IsValid)
            {
                testowaEntities ent = new testowaEntities();
                komputery x = Komputer.ConvertToKopmputery(k);
                ent.komputeries.Add(x);
                ent.SaveChanges();
                return RedirectToAction("ListaKomputerow");
            }
            return View(k);
        }
        //EDIT
        [HttpGet]
        public ActionResult Edit(int id)
        {

            testowaEntities ent = new testowaEntities();
            Komputer model = new Komputer(ent.komputeries.Where(x => x.komputerId == id).FirstOrDefault());
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Komputer model)
        {
            if (ModelState.IsValid)
            {
                testowaEntities ent = new testowaEntities();
                komputery komp = ent.komputeries.Where(x => x.komputerId == model.Id).FirstOrDefault();
                komp.dostawca = model.Dostawca;
                if(komp.firma!=null) komp.firma = model.Firma;
                komp.uzykownikId = model.UzytkownikId;
                ent.SaveChanges();

                return RedirectToAction("ListaKomputerow");
            }
            return View(model);
        }
        //DELETE
        [HttpGet]
        public ActionResult Delete (int id)
        {
            testowaEntities ent = new testowaEntities();
            Komputer model = new Komputer(ent.komputeries.Where(x => x.komputerId == id).FirstOrDefault());
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {    
                
                testowaEntities ent = new testowaEntities();
                komputery komp = ent.komputeries.Find(id);
                ent.komputeries.Remove(komp);
                ent.SaveChanges();

                return RedirectToAction("ListaKomputerow");
            }
        }
    }
