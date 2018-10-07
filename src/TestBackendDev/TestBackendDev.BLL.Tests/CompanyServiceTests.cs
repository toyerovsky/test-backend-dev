using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBackendDev.BLL.Dto;
using TestBackendDev.BLL.Dto.Response;
using TestBackendDev.BLL.Services.Company;
using TestBackendDev.BLL.UnitOfWork;
using TestBackendDev.DAL.Enums;
using TestBackendDev.DAL.Models;

namespace TestBackendDev.BLL.Tests
{
    public class CompanyServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        private readonly ICompanyService _companyService;

        public CompanyServiceTests()
        {
            #region Mock data definition

            IEnumerable<CompanyModel> mockCompanies = new List<CompanyModel>()
            {
                new CompanyModel()
                {
                    Id = 1,
                    Name = "Microsoft",
                    EstablishmentYear = 1975,
                    Employees = new List<EmployeeModel>()
                    {
                        new EmployeeModel()
                        {
                            Id = 1,
                            CompanyId = 1,
                            DateOfBirth = new DateTime(1970, 01, 02),
                            FirstName = "Vlado",
                            LastName = "Fannar",
                            JobTitle = JobTitle.Administrator
                        },
                        new EmployeeModel()
                        {
                            Id = 2,
                            CompanyId = 1,
                            DateOfBirth = new DateTime(1972, 03, 22),
                            FirstName = "Vlado",
                            LastName = "Wit",
                            JobTitle = JobTitle.Administrator
                        },
                        new EmployeeModel()
                        {
                            Id = 3,
                            CompanyId = 1,
                            DateOfBirth = new DateTime(1980, 04, 12),
                            FirstName = "Halstein Fannar",
                            LastName = "Fannar",
                            JobTitle = JobTitle.Architect
                        }
                    }
                },
                new CompanyModel()
                {
                    Id = 2,
                    Name = "Google",
                    EstablishmentYear = 1998,
                    Employees = new List<EmployeeModel>()
                    {
                        new EmployeeModel()
                        {
                            Id = 4,
                            CompanyId = 2,
                            DateOfBirth = new DateTime(1988, 06, 21),
                            FirstName = "Vlado",
                            LastName = "Boniface",
                            JobTitle = JobTitle.Manager
                        },
                        new EmployeeModel()
                        {
                            Id = 5,
                            CompanyId = 2,
                            DateOfBirth = new DateTime(1976, 01, 25),
                            FirstName = "Cláudio",
                            LastName = "Eberhard",
                            JobTitle = JobTitle.Developer
                        },
                        new EmployeeModel()
                        {
                            Id = 6,
                            CompanyId = 2,
                            DateOfBirth = new DateTime(1985, 12, 12),
                            FirstName = "Theodotos",
                            LastName = "Loki",
                            JobTitle = JobTitle.Developer
                        }
                    }
                },
                new CompanyModel()
                {
                    Id = 3,
                    Name = "Samsung",
                    EstablishmentYear = 1938,
                    Employees = new List<EmployeeModel>()
                    {
                        new EmployeeModel()
                        {
                            Id = 7,
                            CompanyId = 3,
                            DateOfBirth = new DateTime(1988, 06, 21),
                            FirstName = "Jung-Hee",
                            LastName = "Byeong-Ho",
                            JobTitle = JobTitle.Architect
                        },
                        new EmployeeModel()
                        {
                            Id = 8,
                            CompanyId = 4,
                            DateOfBirth = new DateTime(1976, 01, 25),
                            FirstName = "Su-Bin",
                            LastName = "Yeong-Cheol",
                            JobTitle = JobTitle.Administrator
                        },
                        new EmployeeModel()
                        {
                            Id = 9,
                            CompanyId = 5,
                            DateOfBirth = new DateTime(1985, 12, 12),
                            FirstName = "Seong-Hyeon",
                            LastName = "Suk",
                            JobTitle = JobTitle.Developer
                        }
                    }
                }
            };

            #endregion

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyModel, CompanyDto>();
                cfg.CreateMap<CompanyDto, CompanyModel>();

                cfg.CreateMap<EmployeeModel, EmployeeDto>();
                cfg.CreateMap<EmployeeDto, EmployeeModel>();

                // created response contains id only
                cfg.CreateMap<CompanyDto, CreatedResponseDto>();
                cfg.CreateMap<EmployeeDto, CreatedResponseDto>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            _uowMock.Setup(uow => uow
                .CompaniesRepository.GetAllAsync(null,
                        It.IsAny<Func<IQueryable<CompanyModel>, IQueryable<CompanyModel>>>()))
                .Returns(Task.FromResult(mockCompanies));

            _companyService = new CompanyService(_uowMock.Object, mapper);
        }

        [Test]
        public async Task Search_ReturnsMicrosoft_When_KeywordIsMicrosoft()
        {
            SearchDto searchDto = new SearchDto()
            {
                Keyword = "Microsoft"
            };

            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.That(result.Count == 1 &&
                        result.All(c => c.Name == "Microsoft"));
        }

        [Test]
        public async Task Search_ReturnsMicrosoftAndGoogle_When_KeywordIsVlado()
        {
            SearchDto searchDto = new SearchDto()
            {
                Keyword = "Vlado"
            };

            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.That(result.Count == 2
                        && result.Any(c => c.Name == "Microsoft")
                        && result.Any(c => c.Name == "Google"));
        }

        [Test]
        public async Task Search_ReturnsGoogleAndSamsung_When_JobTitleIsDeveloper()
        {
            SearchDto searchDto = new SearchDto()
            {
                EmployeeJobTitles = new List<string>()
                {
                    "Developer"
                }
            };

            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.That(result.Count == 2
                        && result.Any(c => c.Name == "Samsung")
                        && result.Any(c => c.Name == "Google"));
        }

        [Test]
        public async Task Search_ReturnsMicrosoft_When_DateFrom1970To1972()
        {
            SearchDto searchDto = new SearchDto()
            {
                EmployeeDateOfBirthFrom = new DateTime(1970, 1, 1),
                EmployeeDateOfBirthTo = new DateTime(1972, 1, 1),
            };

            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.That(result.Count == 1
                        && result.Any(c => c.Name == "Microsoft"));
        }
    }
}
