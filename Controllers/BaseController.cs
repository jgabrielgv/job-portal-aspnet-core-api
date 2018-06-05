using JobPortal.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using JobPortal.Core.Domain;

namespace JobPortal.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    public class BaseController : Controller
    {
        public BaseController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base()
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            UserManager = userManager;
        }

        public virtual IUnitOfWork UnitOfWork { get; private set; }
        public virtual IMapper Mapper { get; private set; }
        public virtual UserManager<ApplicationUser> UserManager { get; private set; }
    }
}