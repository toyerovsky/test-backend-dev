using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestBackendDev.BLL.Dto
{
    public class EmployeeDto
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public long CompanyId { get; set; }
        public virtual CompanyDto CompanyModel { get; set; }
    }
}