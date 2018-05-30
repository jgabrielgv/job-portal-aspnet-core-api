using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortal.Core;
using JobPortal.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CompaniesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Company>> Get() {
            return await _unitOfWork.Companies.GetAllAsync();
        }

        [HttpPost]
        public async Task Create([FromBody] Company company) {
            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.CompleteAsync();
        }
    }
}