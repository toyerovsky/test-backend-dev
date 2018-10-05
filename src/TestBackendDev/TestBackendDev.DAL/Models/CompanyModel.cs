using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestBackendDev.DAL.Interfaces;

namespace TestBackendDev.DAL.Models
{
    public class CompanyModel : IEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }

        // navigation properties
        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}