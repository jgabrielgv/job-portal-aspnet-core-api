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
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CompaniesController : BaseController
    {
        public CompaniesController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork, mapper, userManager)
        {
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public async Task<IActionResult> GetAllAsync() => Ok(await UnitOfWork.Companies.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetByIdAsync([Required] int id)
        {
            Company company = await UnitOfWork.Companies.GetAsync(id);
            if(company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
        
        // will fail as long as the UserId is not assigned
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromBody] CompanyDTO company)
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