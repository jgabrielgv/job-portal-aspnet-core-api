using System;
using JobPortal.Core.Domain;
using JobPortal.Core.Repositories;

namespace JobPortal.Persistence.Repositories
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}