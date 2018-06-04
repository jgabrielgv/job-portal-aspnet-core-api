using System;
using System.Threading.Tasks;
using JobPortal.Core;
using JobPortal.Core.Domain;
using JobPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JobPortal.Controllers {
    public class ApplicationsController : BaseController {
        public ApplicationsController (IUnitOfWork unitOfWork) : base (unitOfWork) { }

        [HttpGet]
        public async Task<IActionResult> Get () => Ok (await UnitOfWork.Applications.GetAllAsync ());

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] ApplicationViewModel application) {
            try {
                if (!await ValidApplicationToCreate (application, ModelState)) {
                    return BadRequest (ModelState);
                }
                Application applicationEntity = ToApplicationEntity (application);
                await UnitOfWork.Applications.AddAsync (applicationEntity);
                await UnitOfWork.CompleteAsync ();
                return new CreatedResult ("/api/applications", applicationEntity);
            } catch (Exception) {
                return BadRequest ("An exception has occured while creating the application. Please try again.");
            }
        }

        public async virtual Task<bool> ValidApplicationToCreate (ApplicationViewModel viewModel, ModelStateDictionary modelState) {
            bool returnValue = true;
            if (!modelState.IsValid) {
                return !returnValue;
            }
            if (!viewModel.CandidateId.HasValue || viewModel.CandidateId <= 0 || await UnitOfWork.Candidates.GetAsync(viewModel.CandidateId.Value) == null) {
                modelState.AddModelError ("CandidateId", "Invalid Candidate");
                returnValue = false;
            }
            if (!viewModel.JobId.HasValue || viewModel.JobId <= 0 || await UnitOfWork.Jobs.GetAsync(viewModel.JobId.Value) == null) {
                modelState.AddModelError ("JobId", "Invalid Job");
                returnValue = false;
            }
            return returnValue;
        }

        public virtual Application ToApplicationEntity (ApplicationViewModel viewModel) => new Application {
            CandidateId = viewModel.CandidateId.Value,
            JobId = viewModel.JobId.Value
        };
    }
}