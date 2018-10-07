using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestBackendDev.BLL.Dto
{
    public class CompanyDto
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; }
    }
}