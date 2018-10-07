using System.Collections.Generic;
using System.Threading.Tasks;
using TestBackendDev.BLL.Dto;
using TestBackendDev.BLL.Dto.Response;

namespace TestBackendDev.BLL.Services.Company
{
    public interface ICompanyService
    {
        Task<CreatedResponseDto> CreateAsync(CompanyDto companyDto);
        Task<IEnumerable<CompanyDto>> SearchAsync(SearchDto searchDto);
        Task<CompanyDto> UpdateAsync(long id, CompanyDto companyDto);
    }
}