using System.Collections.Generic;
using System.Threading.Tasks;
using TestBackendDev.BLL.Dto;

namespace TestBackendDev.BLL.Services.Company
{
    public interface ICompanyService
    {
        Task<CompanyDto> CreateAsync(CompanyDto companyDto);
        Task<IEnumerable<CompanyDto>> SearchAsync(SearchDto searchDto);
        Task<CompanyDto> UpdateAsync(long id, CompanyDto companyDto);
    }
}