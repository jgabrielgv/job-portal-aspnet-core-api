using JobPortal.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    public class BaseController : Controller
    {
        public BaseController(IUnitOfWork unitOfWork) : base()
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork;
    }
}