using JobPortal.Core;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IUnitOfWork unitOfWork) : base()
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork;
    }
}