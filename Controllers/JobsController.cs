using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortal.Core;
using JobPortal.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class JobsController : BaseController
    {
        public JobsController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await UnitOfWork.Jobs.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Job job) {
            await UnitOfWork.Jobs.AddAsync(job);
            await UnitOfWork.CompleteAsync();
            return new CreatedResult("/api/jobs", job);
        }
    }
}