using JobPortal.Core.Domain;
using JobPortal.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Persistence.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context)
        {
        }
    }
}