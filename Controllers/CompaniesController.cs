using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortal.Core;
using JobPortal.Core.Domain;
using JobPortal.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    public class CompaniesController : BaseController
    {
        public CompaniesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await UnitOfWork.Companies.GetAllAsync());

        // will fail as long as the UserId is not assigned
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyDTO company) {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Company companyEntity = ToCompanyEntity(company);
            await UnitOfWork.Companies.AddAsync(companyEntity);
            await UnitOfWork.CompleteAsync();
            return new CreatedResult("/api/companies", companyEntity);
        }

        protected virtual Company ToCompanyEntity(CompanyDTO company) => new Company {
            Name = company.Name,
            City = company.City,
            Address = company.Address
        };
    }
}