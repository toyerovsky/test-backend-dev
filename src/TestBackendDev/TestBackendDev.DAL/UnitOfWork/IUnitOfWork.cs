using System.Threading.Tasks;
using TestBackendDev.DAL.Repositories.Companies;
using TestBackendDev.DAL.Repositories.Employees;

namespace TestBackendDev.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICompaniesRepository CompaniesRepository { get; }
        IEmployeesRepository EmployeesRepository { get; }
        Task<int> SaveAsync();
        void Save();
    }
}