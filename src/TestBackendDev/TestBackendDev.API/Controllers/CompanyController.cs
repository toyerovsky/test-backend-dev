using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestBackendDev.BLL.Dto;
using TestBackendDev.BLL.Dto.Response;
using TestBackendDev.BLL.Services.Company;
using ZNetCS.AspNetCore.Authentication.Basic;

namespace TestBackendDev.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme)]
    public class CompanyController : Controller
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(companyDto);
            }

            return Created("", await _companyService.CreateAsync(companyDto));
        }

        [HttpPost("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync([FromBody] SearchDto searchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(searchDto);
            }

            var result = new SearchResponseDto { Results = await _companyService.SearchAsync(searchDto) };

            return Json(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] CompanyDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(companyDto);
            }

            // prevent from foreign key fail
            try
            {
                CompanyDto resultDto = await _companyService.UpdateAsync(id, companyDto);

                if (resultDto == null)
                {
                    return NotFound(id);
                }

                return Json(resultDto);
            }
            catch (DbUpdateException)
            {
                return BadRequest(companyDto);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            if (!await _companyService.ContainsAsync(id))
            {
                return NotFound(id);
            }

            await _companyService.DeleteAsync(id);

            return NoContent();
        }
    }
}