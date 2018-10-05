using TestBackendDev.DAL.Models;

namespace TestBackendDev.DAL.Repositories.Employees
{
    public class EmployeesRepository : BaseRepository<TestBackendDevContext, Employee>, IEmployeesRepository
    {
        public EmployeesRepository(TestBackendDevContext context) : base(context)
        {
        }
    }
}