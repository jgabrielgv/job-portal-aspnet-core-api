using System;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Core.Repositories;
using WebAPI.Persistence.Repositories;

namespace WebAPI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            this.Applications = new ApplicationRepository(_context);
            this.Candidates = new CandidateRepository(_context);
            this.Companies = new CompanyRepository(_context);
            this.Jobs = new JobRepository(_context);
            this.UserTokens = new RefreshUserTokenRepository(_context);
            this.Parties = new PartyRepository(_context);
        }

        public IApplicationRepository Applications { get; private set; }
        public ICandidateRepository Candidates { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public IJobRepository Jobs { get; private set; }
        public IRefreshUserToken UserTokens { get; private set; }
        public IPartyRepository Parties { get; private set; }

        public int Complete() => _context.SaveChanges();
        public Task<int> CompleteAsync() => _context.SaveChangesAsync();
        public void Dispose() =>_context.Dispose();
    }
}