using Project2024.Models;
using Projet2024V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Projet2024V2.Controllers
{
    //[CustomAuthorizeAttributeCand]
    public class CandidatesController : Controller
    {
        private ERecruitmentDBContext db = new ERecruitmentDBContext();

        // GET: Candidates
        // GET: Recruiters//Recruiter Details
        // ============================ Recruiter Profile datails
        public ActionResult DetailsProfile()
        {
            Candidate candidate = db.candidates.Find((int)Session["CandidateID"]);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // ============================ Recruiter Register 
        // GET: Recruiters/Sign up


        public ActionResult Register()
        {
            var diplomaList = new SelectList(new List<string> { "Bac", "Bac+2", "Licence", "Master", "Ing" });
            ViewBag.DiplomaList = diplomaList;
            return View();  
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Candidate candidate)
        {
            var diplomaList = new SelectList(new List<string> { "Bac", "Bac+2", "Licence", "Master", "Ing" });
            ViewBag.DiplomaList = diplomaList;
            if (ModelState.IsValid)
            {
                
                var ExistCandi = db.candidates.Where(x => x.CandidateEmail.Equals(candidate.CandidateEmail));
                if (ExistCandi.Any())
                {
                    ModelState.AddModelError("CandidateEmail", "User with this email already exists");
                    return View(candidate); // Return here to show the form again with the error
                }
                if (candidate.UploadedCVFile != null && candidate.UploadedCVFile.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(candidate.UploadedCVFile.InputStream))
                    {
                        candidate.CVFile = reader.ReadBytes(candidate.UploadedCVFile.ContentLength);
                    }
                }
                db.candidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(candidate);
        }



        // GET: Recruiters/Sign up
        // ============================ Recruiter sign up 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "CandidateEmail,CandidatePassword")] Candidate candidate)
        {
            // Clear model state errors for other properties
            ModelState.Clear();

            // Validate the specified properties
            ValidateRecruiterMail(candidate.CandidateEmail);
            ValidateRecruiterPassword(candidate.CandidatePassword);


            if (ModelState.IsValid)
            {
                var existRec = db.candidates.Where(x => x.CandidateEmail.Equals(candidate.CandidateEmail) && x.CandidatePassword.Equals(candidate.CandidatePassword));
                if (existRec.Count() > 0)
                {
                    Session["CandidateID"] = existRec.FirstOrDefault().CandidateID;

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(candidate);
        }

        // GET: Recruiters/logout
        // ============================ Recruiter log out 
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
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

        //  =============================GET FILE BYTE

        public ActionResult Postuler(int id)
        {
            if (id == null )
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int candidateId = Convert.ToInt32(Session["CandidateID"]);
           
            var ExistcandidateOffers = db.candidateOffres.Where(x => x.idCandidate.Equals(candidateId) && x.idOffre.Equals(id));

            if (!ExistcandidateOffers.Any())
            {
                OffreCandidate offCand = new OffreCandidate();
                offCand.idCandidate = (int)Session["CandidateID"];
                offCand.idOffre = (int)id;

                db.candidateOffres.Add(offCand);
                db.SaveChanges();
            }
      

            return RedirectToAction("MyOffres","Candidates");
        }
        public ActionResult MyOffres()
        {
            int candidateId = Convert.ToInt32(Session["CandidateID"]);

            var candidateOffers = db.candidateOffres.Where(x => x.idCandidate.Equals(candidateId))
                .Join(db.offres ,
                co => co.idOffre,
                o => o.OffreID,
                (co, o) => o)                                 // Directly selecting the Offre object
                .ToList();

            return View(candidateOffers);
        }

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

        // GET: Offres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Find the candidate-offer association(s) for the given offer ID
            var candidateOffreRecords = db.candidateOffres
                                          .Where(co => co.idOffre == id)
                                          .ToList();

            // If there are any records, remove them
            if (candidateOffreRecords.Any())
            {
                foreach (var record in candidateOffreRecords)
                {
                    db.candidateOffres.Remove(record);
                }

                db.SaveChanges();
            }

            return RedirectToAction("MyOffres", "Candidates");
        }

        //public ActionResult Search()
        //{
            
        //    return View(new Offre);
        //}
        //public ActionResult Search(string contractType, string sector, string profile)
        //{
        //    var query = db.offres.AsQueryable();

        //    // Filtering based on all three criteria at once
        //    query = query.Where(o => (string.IsNullOrEmpty(contractType) || o.ContractType.Contains(contractType)) &&
        //                             (string.IsNullOrEmpty(sector) || o.Sector.Contains(sector)) &&
        //                             (string.IsNullOrEmpty(profile) || o.Profile.Contains(profile)));

        //    var results = query.ToList();
        //    return View(results);
        //}




        public ActionResult DownloadCV(int id)
        {
            var candidate = db.candidates.FirstOrDefault(c => c.CandidateID == id);
            if (candidate == null || candidate.CVFile == null)
            {
                return new HttpNotFoundResult();
            }

            return File(candidate.CVFile, "application/pdf", "CandidateCV.pdf");
        }

        //<a href="@Url.Action("DownloadCV", new { id = item.CandidateID })">Download CV</a>


    }
}