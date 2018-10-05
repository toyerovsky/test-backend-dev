using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestBackendDev.DAL.Enums;
using TestBackendDev.DAL.Interfaces;

namespace TestBackendDev.DAL.Models
{
    public class Employee : IEntity
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
        [EnumDataType(typeof(JobTitle))]
        public JobTitle JobTitle { get; set; }

        // foreign keys
        [ForeignKey("CompanyModel")]
        public long CompanyId { get; set; }
        // navigation properties
        public virtual CompanyModel CompanyModel { get; set; }
    }
}