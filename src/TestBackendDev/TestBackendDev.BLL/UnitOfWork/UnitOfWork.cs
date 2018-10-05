using System;
using System.Threading.Tasks;
using TestBackendDev.DAL;
using TestBackendDev.DAL.Repositories.Companies;
using TestBackendDev.DAL.Repositories.Employees;

namespace TestBackendDev.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private TestBackendDevContext _dbContext;

        public ICompaniesRepository CompaniesRepository { get; }
        public IEmployeesRepository EmployeesRepository { get; }

        public UnitOfWork(TestBackendDevContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(TestBackendDevContext));
            CompaniesRepository = new CompaniesRepository(dbContext);
            EmployeesRepository = new EmployeesRepository(dbContext);
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}