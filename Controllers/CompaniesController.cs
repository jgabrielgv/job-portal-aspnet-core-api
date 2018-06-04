using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortal.Core;
using JobPortal.Core.Domain;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Company company) {
            await UnitOfWork.Companies.AddAsync(company);
            await UnitOfWork.CompleteAsync();
            return new CreatedResult("/api/companies", company);
        }
    }
}