using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JobPortal.Core;
using JobPortal.Core.Domain;
using JobPortal.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JobPortal.Controllers
{
    public class JobsController : BaseController {
        public JobsController (IUnitOfWork unitOfWork) : base (unitOfWork) { }

        [HttpGet]
        public async Task<IActionResult> Get () => Ok (await UnitOfWork.Jobs.GetAllAsync ());

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] JobDTO job) {
            try {
                if (!await ValidateJobToCreate (job, ModelState)) {
                    return BadRequest (ModelState);
                }
                Job jobEntity = ToJobEntity(job);
                await UnitOfWork.Jobs.AddAsync (jobEntity);
                await UnitOfWork.CompleteAsync ();
                return new CreatedResult ("/api/jobs", jobEntity);
            } catch(Exception) {
                return BadRequest("An exception has occured while creating the job. Please try again.");
            }
        }

        protected async virtual Task<bool> ValidateJobToCreate (JobDTO job, ModelStateDictionary modelState) {
            bool returnValue = true;
            if (!modelState.IsValid) {
                return !returnValue;
            }
            if (!job.CompanyId.HasValue || job.CompanyId <= 0 || await UnitOfWork.Companies.GetAsync (job.CompanyId.Value) == null) {
                ModelState.AddModelError ("CompanyId", "Invalid Company.");
                returnValue = false;
            }
            return returnValue;
        }

        public virtual Job ToJobEntity(JobDTO jobDTO) => new Job {
            Title = jobDTO.Title,
            CompanyId = jobDTO.CompanyId.Value
        };
    }
}