using Project2024.Models;
using Projet2024V2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projet2024V2.Controllers
{
    //[CustomAuthorizeAttributeRec]
    public class RecruitersController : Controller
    {
        private ERecruitmentDBContext db = new ERecruitmentDBContext();

        // GET: Recruiters
        public ActionResult Index()
        {
            return View(db.recruiters.ToList());
        }

        // GET: Recruiters//Recruiter Details
        // ============================ Recruiter Profile datails
        public ActionResult DetailsProfile()
        {
            Recruiter recruiter = db.recruiters.Find((int)Session["RecruiterID"]);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        // ============================ Recruiter Register 
        // GET: Recruiters/Sign up
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "RecruiterID,RecruiterName,RecruiterMail,RecruiterPassword,RecruiterTel")] Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                var ExistRec = db.recruiters.Where(x => x.RecruiterMail.Equals(recruiter.RecruiterMail));
                if (ExistRec.Any())
                {
                    ModelState.AddModelError("RecruiterMail", "User with this email already exists");
                    return View(recruiter); // Return here to show the form again with the error
                }
                db.recruiters.Add(recruiter);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(recruiter);
        }



        // GET: Recruiters/Sign up
        // ============================ Recruiter sign up 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "RecruiterMail,RecruiterPassword")] Recruiter recruiter)
        {
            // Clear model state errors for other properties
            ModelState.Clear();

            // Validate the specified properties
            ValidateRecruiterMail(recruiter.RecruiterMail);
            ValidateRecruiterPassword(recruiter.RecruiterPassword);


            if (ModelState.IsValid)
            {
                var existRec = db.recruiters.Where(x => x.RecruiterMail.Equals(recruiter.RecruiterMail) && x.RecruiterPassword.Equals(recruiter.RecruiterPassword));
                if (existRec.Count() > 0)
                {
                    Session["RecruiterID"] = existRec.FirstOrDefault().RecruiterID;

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(recruiter);
        }

        // GET: Recruiters/logout
        // ============================ Recruiter log out 
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }


        // GET: Recruiters/Edit 
        // ============================ Recruiter Edit Profile
        public ActionResult EditProfile()
        {
            Recruiter recruiter = db.recruiters.Find(Session["RecruiterID"]);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        // POST: Recruiters/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "RecruiterID,RecruiterName,RecruiterMail,RecruiterPassword,RecruiterTel")] Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recruiter);
        }


        // GET: Recruiters/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Recruiter recruiter = db.recruiters.Find(id);
        //    if (recruiter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(recruiter);
        //}

        // POST: Recruiters/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Recruiter recruiter = db.recruiters.Find(id);
        //    db.recruiters.Remove(recruiter);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //===========================Rec ajouter Offre
        //Get ajouter offre

        [CustomAuthorizeAttributeRec]
        public ActionResult AddOffre()
        {
            

            var profilList = new SelectList(new List<string> { "Bac+2", "Licence","Master","Engineering" });
            ViewBag.ProfilList = profilList;

            var contractList = new SelectList(new List<string> { "CDD", "CDI" });
            ViewBag.ContractList = contractList;
            return View();
        }
        [CustomAuthorizeAttributeRec]
        [HttpPost]
        public ActionResult AddOffre(Offre offre)
        {
            var profilList = new SelectList(new List<string> { "Bac+2", "Licence", "Master", "Engineering" });
            ViewBag.ProfilList = profilList;

            var contractList = new SelectList(new List<string> { "CDD", "CDI" });
            ViewBag.ContractList = contractList;

            offre.RecruiterID = (int)Session["RecruiterID"];
            if (ModelState.IsValid)
            {
                db.offres.Add(offre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // =========================Rec mes offre
        // GET: Offres
        public ActionResult MyOffres()
        {
            int recruiterId = (int)Session["RecruiterID"];
            var offres = db.offres.Where(x => x.RecruiterID.Equals(recruiterId));
            return View(offres.ToList());

        }

        [CustomAuthorizeAttributeRec]
        public ActionResult ConsulteCandidates()
        {
            int recruiterId = (int)Session["RecruiterID"];
            var candidatesForRecruiterOffers = db.offres
                .Where(o => o.RecruiterID.Equals(recruiterId))
                .Join(db.candidateOffres,
                    offre => offre.OffreID,
                    candidateOffre => candidateOffre.idOffre,
                    (offre, candidateOffre) => new { Offre = offre, CandidateOffre = candidateOffre })
                .Join(db.candidates,
                    combined => combined.CandidateOffre.idCandidate,
                    candidate => candidate.CandidateID,
                    (combined, candidate) => new CandidateOffreViewModel
                    {
                        OffreID = combined.Offre.OffreID,
                        CandidateID=candidate.CandidateID,
                        CandidateName= candidate.CandidateName,
                        CandidateEmail=candidate.CandidateEmail,
                        CandidateAge =candidate.CandidateAge,
                        CandidateNbExperiences =candidate.CandidateNbExperiences,
                        CandidateAddress=candidate.CandidateAddress,
                        CandidateDiploma =candidate.CandidateDiploma,
                        CVFile=candidate.CVFile
                    })
                .ToList();



            return View(candidatesForRecruiterOffers);

        }


        //===========================Rec modify Offre
        //Get modify offre
        // GET: Offres/Edit/5
        public ActionResult ModifyOffre(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyOffre(Offre offre)
        {
            offre.RecruiterID = (int)Session["RecruiterID"];
            if (ModelState.IsValid)
            {
                db.Entry(offre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offre);
        }

        //===========================Rec delete Offre
        //Get delete offre
        public ActionResult DeleteOffre(int? id)
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

        // POST:  delete offre
        [HttpPost, ActionName("DeleteOffre")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offre offre = db.offres.Find(id);
            db.offres.Remove(offre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void ValidateRecruiterMail(string recruiterMail)
        {
            if (string.IsNullOrWhiteSpace(recruiterMail))
            {
                ModelState.AddModelError("RecruiterMail", "Recruiter email is required.");
            }
            else if (!IsValidEmail(recruiterMail))
            {
                ModelState.AddModelError("RecruiterMail", "Invalid recruiter email format.");
            }
        }

        private void ValidateRecruiterPassword(string recruiterPassword)
        {
            if (string.IsNullOrWhiteSpace(recruiterPassword))
            {
                ModelState.AddModelError("RecruiterPassword", "Recruiter password is required.");
            }
            // Add more validation rules for the password if needed
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}