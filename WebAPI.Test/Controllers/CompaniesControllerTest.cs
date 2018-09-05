using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Core;
using WebAPI.Core.Domain;
using WebAPI.DTOs;
using WebAPI.Test.Mocks;
using Xunit;

namespace WebAPI.Test.Controllers
{
    public class CompaniesControllerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private Mock<FakeUserManager> _userManager;
        private readonly CompaniesController controller;

        public CompaniesControllerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _userManager = new Mock<FakeUserManager>();

            controller = new CompaniesController(_unitOfWorkMock.Object, _mapperMock.Object, _userManager.Object);
        }

        [Fact]
        public async Task GetAllAsyncTest_ReturnsOk()
        {
            IEnumerable<Company> companiesList = new List<Company>();
            _unitOfWorkMock.Setup(s => s.Companies.GetAllAsync())
                .Returns(Task.FromResult(companiesList));

            var result = await controller.GetAllAsync();

            var assert = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdAsyncTest_ReturnsNotFound()
        {
            _unitOfWorkMock.Setup(s => s.Companies.GetAsync(1))
                .Returns(Task.FromResult((Company)null));

            var result = await controller.GetByIdAsync(1);

            var assert = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostAsyncTest_ReturnsCreated()
        {
            var companyDTO = new CompanyDTO();
            var company = new Company();

            _mapperMock.Setup(m => m.Map<CompanyDTO, Company>(companyDTO))
                .Returns(company);
            _unitOfWorkMock.Setup(s => s.Companies.AddAsync(company))
                .Returns(Task.FromResult(company));
            _unitOfWorkMock.Setup(s => s.CompleteAsync())
                .Returns(Task.FromResult(1));

            var result = await controller.PostAsync(companyDTO);

            var assert = Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task PostAsyncTest_ReturnsBadRequest()
        {
            var companyDTO = new CompanyDTO();
            controller.ModelState.AddModelError("MockError", "Model is invalid");

            var result = await controller.PostAsync(companyDTO);

            var assert = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
