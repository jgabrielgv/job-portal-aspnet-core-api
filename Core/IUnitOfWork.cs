using JobPortal.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace JobPortal.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepository Applications { get; }
        ICandidateRepository Candidates { get; }
        ICompanyRepository Companies { get; }
        IJobRepository Jobs { get; }
        IRefreshUserToken UserTokens { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}