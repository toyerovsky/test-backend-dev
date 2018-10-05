using System;
using System.ComponentModel.DataAnnotations;
using TestBackendDev.DAL.Enums;
using TestBackendDev.DAL.Interfaces;

namespace TestBackendDev.DAL.Models
{
    public class Employee : IEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [EnumDataType(typeof(JobTitle))]
        public JobTitle JobTitle { get; set; }
    }
}