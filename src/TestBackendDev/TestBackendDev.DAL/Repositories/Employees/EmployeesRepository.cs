using TestBackendDev.DAL.Models;

namespace TestBackendDev.DAL.Repositories.Employees
{
    public class EmployeesRepository : BaseRepository<TestBackendDevContext, EmployeeModel>, IEmployeesRepository
    {
        public EmployeesRepository(TestBackendDevContext context) : base(context)
        {
        }
    }
}