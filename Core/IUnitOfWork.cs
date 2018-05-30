using JobPortal.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace JobPortal.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Companies { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}