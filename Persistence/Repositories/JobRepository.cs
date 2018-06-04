using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortal.Core.Domain;
using JobPortal.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Persistence.Repositories
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async override Task<IEnumerable<Job>> GetAllAsync() {
            return await Context.Jobs
            .Include(p => p.Company)
            .ToListAsync();
        }
    }
}