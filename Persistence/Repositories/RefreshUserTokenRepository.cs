using System;
using System.Linq;
using System.Linq.Expressions;
using JobPortal.Core.Domain;
using JobPortal.Core.Repositories;

namespace JobPortal.Persistence.Repositories
{
    public class RefreshUserTokenRepository : Repository<RefreshUserToken>, IRefreshUserToken
    {
        public RefreshUserTokenRepository(ApplicationDbContext context) : base(context) { }
  }
}