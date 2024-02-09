using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet2024V2.Models
{
    public class CandidateOffreViewModel
    {
        public int OffreID { get; set; }

        public int CandidateID { get; set; }
        public string CandidateName { get; set; }

        public string CandidateEmail { get; set; }

        public int CandidateAge { get; set; }

        public int CandidateNbExperiences { get; set; }

        public string CandidateAddress { get; set; }

        public string CandidateDiploma { get; set; }

        public byte[] CVFile { get; set; }
    }
}