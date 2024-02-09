using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project2024.Models;
using Projet2024V2.Models;

namespace Projet2024V2.Controllers
{
    public class OffresController : Controller
    {
        private ERecruitmentDBContext db = new ERecruitmentDBContext();

        // GET: Offres
       
        public ActionResult Index()
        {
            var offres = db.offres.Include(o => o.Recruiter);
            return View(offres.ToList());
        }

        // GET: Offres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // GET: Offres/Create
        public ActionResult Create()
        {
            ViewBag.RecruiterID = new SelectList(db.recruiters, "RecruiterID", "RecruiterName");
            return View();
        }

        // POST: Offres/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OffreID,RecruiterID,ContractType,Sector,Profile,Post,Remuneration")] Offre offre)
        {
            if (ModelState.IsValid)
            {
                db.offres.Add(offre);
                db.SaveChanges();
                return RedirectToAction("MyOffres","Recruiters");
            }

            ViewBag.RecruiterID = new SelectList(db.recruiters, "RecruiterID", "RecruiterName", offre.RecruiterID);
            return View(offre);
        }

        // GET: Offres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecruiterID = new SelectList(db.recruiters, "RecruiterID", "RecruiterName", offre.RecruiterID);
            return View(offre);
        }

        // POST: Offres/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OffreID,RecruiterID,ContractType,Sector,Profile,Post,Remuneration")] Offre offre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyOffres","Recruiters");
            }
            ViewBag.RecruiterID = new SelectList(db.recruiters, "RecruiterID", "RecruiterName", offre.RecruiterID);
            return View(offre);
        }

        // GET: Offres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }
        
        // POST: Offres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offre offre = db.offres.Find(id);
            db.offres.Remove(offre);
            db.SaveChanges();
            return RedirectToAction("MyOffres", "Recruiters");
        } 

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
