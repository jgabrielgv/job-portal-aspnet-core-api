using System;
using JobPortal.Core.Domain;
using JobPortal.Core.Repositories;

namespace JobPortal.Persistence.Repositories
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}