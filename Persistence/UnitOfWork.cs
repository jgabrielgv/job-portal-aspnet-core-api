using System;
using System.Threading.Tasks;
using JobPortal.Core;
using JobPortal.Core.Repositories;
using JobPortal.Persistence.Repositories;

namespace JobPortal.Persistence
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
        }

        public IApplicationRepository Applications { get; private set; }
        public ICandidateRepository Candidates { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public IJobRepository Jobs { get; private set; }

        public int Complete() => _context.SaveChanges();
        public Task<int> CompleteAsync() => _context.SaveChangesAsync();
        public void Dispose() =>_context.Dispose();
    }
}