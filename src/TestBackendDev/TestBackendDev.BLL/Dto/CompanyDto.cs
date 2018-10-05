using System.Collections.Generic;

namespace TestBackendDev.BLL.Dto
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public virtual ICollection<EmployeeDto> Employees { get; set; }
    }
}