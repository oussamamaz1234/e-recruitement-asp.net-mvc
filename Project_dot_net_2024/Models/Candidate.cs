using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project2024.Models
{
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CandidateID { get; set; }

        [Required(ErrorMessage = "Candidate Name is required.")]
        [Display(Name = "First Name")]
        public string CandidateName { get; set; }

        [Display(Name = "Second Name")]
        public string CandidatePrename { get; set; }

        [Required(ErrorMessage = "Candidate Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email")]
        public string CandidateEmail { get; set; }

        [Required(ErrorMessage = "Candidate Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string CandidatePassword { get; set; }

        [Required(ErrorMessage = "Candidate Age is required.")]
        [Range(18, 99, ErrorMessage = "Candidate Age must be between 18 and 99.")]
        [Display(Name = "Age")]
        public int CandidateAge { get; set; }

        [Required(ErrorMessage = "Candidate years of experience are required.")]
        [Display(Name = "Years of Experiences")]
        public int CandidateNbExperiences { get; set; }

        [Display(Name = "Address")]
        public string CandidateAddress { get; set; }

        [Required(ErrorMessage = "diploma file is required.")]
        [Display(Name = "Diploma")]

        public string CandidateDiploma { get; set; }


        [NotMapped] // Ne pas mapper ce champ à la base de données
        [Display(Name = "CV File")]
        [Required(ErrorMessage = "CV file is required.")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UploadedCVFile { get; set; }

        public byte[] CVFile { get; set; }

        // Navigation property for foreign key relationship
        public virtual ICollection<OffreCandidate> candidateOffres { get; set; }

    }
}