using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestBackendDev.BLL.Dto;

namespace TestBackendDev.API.Tests
{
    public class BasicAuthenticationTests
    {
        [Test]
        public async Task BasicAuthentication_Returns401Unauthorized_When_UsernameAndPasswordAreNotSet()
        {
            var server = Program.CreateWebHostBuilder(new string[0]).Build();
            await server.StartAsync();

            CompanyDto companyDto = new CompanyDto();
            companyDto.Id = 1;
            HttpClient httpClient = new HttpClient();
            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(companyDto),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5000/company/create",
                httpContent);

            await server.StopAsync();

            Assert.That(response.StatusCode == HttpStatusCode.Unauthorized);
        }
    }
}
