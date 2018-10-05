﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBackendDev.BLL.Dto;
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

        public async Task<CompanyDto> CreateAsync(CompanyDto companyDto)
        {
            CompanyModel companyModel = _mapper.Map<CompanyModel>(companyDto);
            await _unitOfWork.CompaniesRepository.InsertAsync(companyModel);
            await _unitOfWork.SaveAsync();
            companyDto.Id = companyModel.Id;
            return companyDto;
        }

        public async Task<IEnumerable<CompanyDto>> SearchAsync(SearchDto searchDto)
        {
            IEnumerable<CompanyModel> result = await _unitOfWork.CompaniesRepository.GetAllAsync(
                null, i => i.Include(company => company.Employees));

            result = result.Where(company =>
                !searchDto.Keyword.IsNullOrEmpty() &&
                (company.Name.StartsWith(searchDto.Keyword) ||
                 company.Employees.Any(
                     employee => employee.FirstName.StartsWith(searchDto.Keyword) ||
                                 employee.LastName.StartsWith(searchDto.Keyword))) ||
                searchDto.EmployeeDateOfBirthFrom.HasValue &&
                searchDto.EmployeeDateOfBirthTo.HasValue &&
                company.Employees.Any(
                    employee => searchDto.EmployeeDateOfBirthFrom < employee.DateOfBirth &&
                                searchDto.EmployeeDateOfBirthTo > employee.DateOfBirth) ||
                company.Employees.Any(
                    employee => searchDto.EmployeeJobTitles.Contains(employee.JobTitle.ToString())));

            return _mapper.Map<IEnumerable<CompanyDto>>(result);
        }

        public async Task<CompanyDto> UpdateAsync(long id, CompanyDto companyDto)
        {
            CompanyModel companyModel = await _unitOfWork.CompaniesRepository.GetAsync(
                company => company.Id == id,
                i => i.Include(company => company.Employees));

            _mapper.Map(companyDto, companyModel);
            _unitOfWork.CompaniesRepository.Update(companyModel);
            await _unitOfWork.SaveAsync();
            return companyDto;
        }
    }
}