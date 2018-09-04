using System;
using WebAPI.Core.Domain;
using WebAPI.Core.Repositories;

namespace WebAPI.Persistence.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}