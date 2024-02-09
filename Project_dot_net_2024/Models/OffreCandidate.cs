using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project2024.Models
{
    public class OffreCandidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Offre")]
        public int idOffre { get; set; }
        public virtual Offre Offre { get; set; }

        [ForeignKey("Candidate")]
        public int idCandidate { get; set; }
        public virtual Candidate Candidate { get; set; }

        // Navigation properties for foreign key relationships 

    }
}