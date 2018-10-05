using TestBackendDev.DAL.Models;

namespace TestBackendDev.DAL.Repositories.Companies
{
    public class CompaniesRepository : BaseRepository<TestBackendDevContext, CompanyModel>, ICompaniesRepository
    {
        public CompaniesRepository(TestBackendDevContext context) : base(context)
        {
        }
    }
}