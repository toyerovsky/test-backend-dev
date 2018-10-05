﻿using Microsoft.EntityFrameworkCore;
using TestBackendDev.DAL.Models;

namespace TestBackendDev.DAL
{
    public class TestBackendDevContext : DbContext
    {
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public TestBackendDevContext(DbContextOptions options) : base(options)
        {    
        }
    }
}
