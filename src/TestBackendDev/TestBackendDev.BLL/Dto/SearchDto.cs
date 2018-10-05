using System;
using System.Collections.Generic;

namespace TestBackendDev.BLL.Dto
{
    public class SearchDto
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public ICollection<string> EmployeeJobTitles { get; set; }
    }
}