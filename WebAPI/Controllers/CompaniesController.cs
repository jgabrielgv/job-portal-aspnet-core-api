using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Core;
using WebAPI.Core.Domain;
using WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CompaniesController : BaseController
    {
        public CompaniesController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork, mapper, userManager)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await UnitOfWork.Companies.GetAllAsync());

        [HttpGet]
        [Route("getbyaddress")]
        public IActionResult GetByAddress(string address) => Ok(UnitOfWork.Companies.Find(e => e.Address == address));

        // will fail as long as the UserId is not assigned
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyDTO company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Company companyEntity = Mapper.Map<CompanyDTO, Company>(company);
            companyEntity.UserId = UserManager.GetUserId(User);

            await UnitOfWork.Companies.AddAsync(companyEntity);
            await UnitOfWork.CompleteAsync();
            return new CreatedResult("/api/companies", companyEntity);
        }
    }
}