using System.Collections.Generic;

namespace TestBackendDev.BLL.Dto.Response
{
    public class SearchResponseDto
    {
        public IEnumerable<CompanyDto> Results { get; set; }
    }
}