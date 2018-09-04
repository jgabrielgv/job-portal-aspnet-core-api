using System;
using WebAPI.Core.Domain;
using WebAPI.Core.Repositories;

namespace WebAPI.Persistence.Repositories
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}