using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobPortal.Core;
using JobPortal.Core.Domain;
using JobPortal.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JobPortal.Controllers {
    public class ApplicationsController : BaseController {
        public ApplicationsController (IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base (unitOfWork, mapper, userManager) { }

        [HttpGet]
        public async Task<IActionResult> Get () => Ok (await UnitOfWork.Applications.GetAllAsync ());

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] ApplicationDTO application) {
            if (!await ValidApplicationToCreate (application, ModelState)) {
                return BadRequest (ModelState);
            }
            Candidate candidate = await UnitOfWork.Candidates.SingleOrDefaultAsync (c => c.Email == application.Email);
            if (candidate == null) {
                candidate = Mapper.Map<ApplicationDTO, Candidate>(application);
                await UnitOfWork.Candidates.AddAsync (candidate);
            }

            Application applicationEntity = await UnitOfWork.Applications.SingleOrDefaultAsync (app =>
                app.CandidateId == candidate.CandidateId && app.JobId == application.JobId);
            if (applicationEntity == null) {
                applicationEntity = CreateApplicationEntity (candidate, application);
                await UnitOfWork.Applications.AddAsync (applicationEntity);
                await UnitOfWork.CompleteAsync ();
            }

            return new CreatedResult ("/api/applications", new {
                candidateId = applicationEntity.CandidateId,
                jobId = applicationEntity.JobId
            });
        }

        protected virtual Application CreateApplicationEntity (Candidate candidate, ApplicationDTO application) => new Application {
            DateCreated = DateTimeOffset.Now,
            CandidateId = candidate.CandidateId,
            JobId = application.JobId.Value
        };

        protected async virtual Task<bool> ValidApplicationToCreate (ApplicationDTO application, ModelStateDictionary modelState) {
            bool returnValue = true;
            if (!modelState.IsValid) {
                return !returnValue;
            }
            if (application.JobId <= 0 || await UnitOfWork.Jobs.GetAsync (application.JobId.Value) == null) {
                modelState.AddModelError ("JobId", "Invalid Job");
                returnValue = false;
            }
            return returnValue;
        }
    }
}