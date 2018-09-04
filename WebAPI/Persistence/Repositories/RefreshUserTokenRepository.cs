using System;
using System.Linq;
using System.Linq.Expressions;
using WebAPI.Core.Domain;
using WebAPI.Core.Repositories;

namespace WebAPI.Persistence.Repositories
{
    public class RefreshUserTokenRepository : Repository<RefreshUserToken>, IRefreshUserToken
    {
        public RefreshUserTokenRepository(ApplicationDbContext context) : base(context) { }
  }
}