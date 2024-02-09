using Project2024.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Projet2024V2.Models
{
    public class ERecruitmentDBContext : DbContext
    {
        public ERecruitmentDBContext()
           : base("name=ERecruitmentDBContext")
        {
        }
        public DbSet<Offre> offres { get; set; }
        public DbSet<Candidate> candidates { get; set; }
        public DbSet<Recruiter> recruiters { get; set; }
        public DbSet<OffreCandidate> candidateOffres { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}