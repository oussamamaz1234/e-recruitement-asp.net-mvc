using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project2024.Models
{
    public class Recruiter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecruiterID { get; set; }


        [Required(ErrorMessage = "Recruiter Name is required.")]
        [Display(Name = "Full Name")]
        public string RecruiterName { get; set; }


        [Required(ErrorMessage = "Recruiter Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email")]
        public string RecruiterMail { get; set; }


        [Required(ErrorMessage = "Recruiter Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RecruiterPassword { get; set; }


        [Required(ErrorMessage = "Telephone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Display(Name = "Telephone")]
        public string RecruiterTel { get; set; }

        public virtual ICollection<Offre> recruiterOffres { get; set; }
    }
}