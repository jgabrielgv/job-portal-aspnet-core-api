using System.Collections.Generic;
using JobPortal.Core;
using JobPortal.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CompaniesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Company> Get() {
            return _unitOfWork.Companies.GetAll();
        }

        [HttpPost]
        public void Create(Company company) {
            _unitOfWork.Companies.Add(company);
            _unitOfWork.Complete();
        }
    }
}