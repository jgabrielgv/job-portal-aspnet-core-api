using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Core;
using WebAPI.Core.Domain;
using WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Controllers
{
    [Authorize]
    public class JobsController : BaseController
    {
        public JobsController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork, mapper, userManager) { }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() => Ok(await UnitOfWork.Jobs.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] JobDTO job)
        {
            if (!await ValidateJobToCreate(job, ModelState))
            {
                return BadRequest(ModelState);
            }
            Job jobEntity = Mapper.Map<JobDTO, Job>(job);
            await UnitOfWork.Jobs.AddAsync(jobEntity);
            await UnitOfWork.CompleteAsync();
            return new CreatedResult("/api/jobs", jobEntity);
        }

        protected async virtual Task<bool> ValidateJobToCreate(JobDTO job, ModelStateDictionary modelState)
        {
            bool returnValue = true;
            if (!modelState.IsValid)
            {
                return !returnValue;
            }
            if (!job.CompanyId.HasValue || job.CompanyId <= 0 || await UnitOfWork.Companies.GetAsync(job.CompanyId.Value) == null)
            {
                ModelState.AddModelError("CompanyId", "Invalid Company.");
                returnValue = false;
            }
            return returnValue;
        }
    }
}