using JobPortal.Core.Repositories;
using System;

namespace JobPortal.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Companies { get; }
        int Complete();
    }
}