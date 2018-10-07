using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBackendDev.BLL.Dto;
using TestBackendDev.BLL.Dto.Response;
using TestBackendDev.BLL.Extensions;
using TestBackendDev.BLL.UnitOfWork;
using TestBackendDev.DAL.Models;

namespace TestBackendDev.BLL.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedResponseDto> CreateAsync(CompanyDto companyDto)
        {
            CompanyModel companyModel = _mapper.Map<CompanyModel>(companyDto);
            await _unitOfWork.CompaniesRepository.InsertAsync(companyModel);
            await _unitOfWork.SaveAsync();
            companyDto.Id = companyModel.Id;
            return _mapper.Map<CreatedResponseDto>(companyDto);
        }

        public async Task<IEnumerable<CompanyDto>> SearchAsync(SearchDto searchDto)
        {
            IEnumerable<CompanyModel> result = await _unitOfWork.CompaniesRepository.GetAllAsync(
                null, i => i.Include(company => company.Employees));

            bool CheckKeyword(CompanyModel company)
            {
                return !searchDto.Keyword.IsNullOrEmpty() &&
                       (company.Name.Equals(searchDto.Keyword) || company.Employees.Any(
                         employee => employee.FirstName == searchDto.Keyword ||
                                     employee.LastName == searchDto.Keyword));
            }

            bool CheckDateOfBirth(CompanyModel company)
            {
                return searchDto.EmployeeDateOfBirthFrom.HasValue &&
                       searchDto.EmployeeDateOfBirthTo.HasValue &&
                       company.Employees.Any(
                           employee => searchDto.EmployeeDateOfBirthFrom < employee.DateOfBirth &&
                                       searchDto.EmployeeDateOfBirthTo > employee.DateOfBirth);
            }

            bool CheckJobTitle(CompanyModel company)
            {
                return searchDto.EmployeeJobTitles != null &&
                       searchDto.EmployeeJobTitles.Any(
                           jobTitle => company.Employees.Any(
                               employee => employee.JobTitle.ToString() == jobTitle));
            }

            bool CheckAll(CompanyModel company)
            {
                return CheckKeyword(company) || CheckDateOfBirth(company) || CheckJobTitle(company);
            }
            
            return _mapper.Map<List<CompanyDto>>(result.Where(CheckAll));
        }

        public async Task<CompanyDto> UpdateAsync(long id, CompanyDto companyDto)
        {
            CompanyModel companyModel = await _unitOfWork.CompaniesRepository.GetAsync(
                company => company.Id == id,
                i => i.Include(company => company.Employees));

            _mapper.Map(companyDto, companyModel);

            await _unitOfWork.SaveAsync();
            return companyDto;
        }

        public async Task DeleteAsync(long id)
        {
            CompanyModel companyModel = await _unitOfWork.CompaniesRepository.GetAsync(
                company => company.Id == id, null);
            _unitOfWork.CompaniesRepository.Delete(companyModel);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> ContainsAsync(long id)
        {
            return await _unitOfWork.CompaniesRepository.ContainsAsync(id);
        }
    }
}