using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBackendDev.API.Controllers;
using TestBackendDev.BLL.Dto;
using TestBackendDev.BLL.Dto.Response;
using TestBackendDev.BLL.Services.Company;

namespace TestBackendDev.API.Tests
{
    public class CompanyControllerTests
    {
        private readonly CompanyController _companyController;
        private readonly Mock<ICompanyService> _mock = new Mock<ICompanyService>();

        public CompanyControllerTests()
        {
            var mockDto = new CompanyDto() { Id = 1 };
            var mockCreatedDto = new CreatedResponseDto() { Id = 1 };

            IEnumerable<CompanyDto> mockDtoList = new List<CompanyDto> { mockDto };

            _mock.Setup(service => service
                .CreateAsync(null))
                .Returns(Task.FromResult(mockCreatedDto));

            _mock.Setup(service => service
                .UpdateAsync(1, null))
                .Returns(Task.FromResult(mockDto));

            _mock.Setup(service => service
                .SearchAsync(null))
                .Returns(Task.FromResult(mockDtoList));

            _companyController = new CompanyController(_mock.Object);
        }

        [Test]
        public async Task Create_Returns201Created_When_EverythingCorrect()
        {
            IActionResult actionResult = await _companyController.CreateAsync(null);
            Assert.That(actionResult is CreatedResult result &&
                result.StatusCode == 201);
        }

        [Test]
        public async Task Create_ReturnsId_When_EverythingCorrect()
        {
            CreatedResult actionResult = (CreatedResult)await _companyController.CreateAsync(null);
            CreatedResponseDto dto = (CreatedResponseDto)actionResult.Value;
            Assert.AreEqual(1, dto.Id);
        }
    }
}
