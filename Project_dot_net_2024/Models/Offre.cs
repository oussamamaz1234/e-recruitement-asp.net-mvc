using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project2024.Models
{
    public class Offre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OffreID { get; set; }

        [ForeignKey("Recruiter")]
        public int RecruiterID { get; set; }

        [Display(Name = "Recruiter")]
        public virtual Recruiter Recruiter { get; set; }

        [Required(ErrorMessage = "Contract Type is required.")]
        [Display(Name = "Contract Type")]
        public string ContractType { get; set; }

        [Required(ErrorMessage = "Sector is required.")]
        [Display(Name = "Sector")]
        public string Sector { get; set; }

        [Required(ErrorMessage = "Profile is required.")]
        [Display(Name = "Profile")]
        public string Profile { get; set; }

        [Required(ErrorMessage = "Job Post is required.")]
        [Display(Name = "Job Post")]
        public string Post { get; set; }

        [Required(ErrorMessage = "Remuneration is required.")]
        [Display(Name = "Remuneration")]
        public int Remuneration { get; set; }


        // Navigation property for foreign key relationship
        public virtual ICollection<OffreCandidate> candidateOffres { get; set; }
    }
}