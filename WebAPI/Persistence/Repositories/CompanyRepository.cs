using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Core.Domain;
using WebAPI.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Persistence.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async override Task<IEnumerable<Company>> GetAllAsync() {
            return await Context.Companies
            .Include(p => p.Jobs)
            .ToListAsync();
        }
    }
}