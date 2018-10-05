using System;
using TestBackendDev.DAL.Enums;

namespace TestBackendDev.BLL.Dto
{
    public class EmployeeDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitle JobTitle { get; set; }
        public long CompanyId { get; set; }
        public virtual CompanyDto CompanyModel { get; set; }
    }
}