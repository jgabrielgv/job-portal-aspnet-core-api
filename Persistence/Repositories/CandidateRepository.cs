using System;
using JobPortal.Core.Domain;
using JobPortal.Core.Repositories;

namespace JobPortal.Persistence.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}