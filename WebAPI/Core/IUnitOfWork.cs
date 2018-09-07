using WebAPI.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace WebAPI.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepository Applications { get; }
        ICandidateRepository Candidates { get; }
        ICompanyRepository Companies { get; }
        IJobRepository Jobs { get; }
        IPartyRepository Parties { get; }
        IRefreshUserToken UserTokens { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}