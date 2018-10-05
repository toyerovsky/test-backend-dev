using System.Collections.Generic;
using TestBackendDev.DAL.Interfaces;

namespace TestBackendDev.DAL.Models
{
    public class Company : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}