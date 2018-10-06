using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestBackendDev.BLL.Dto;
using TestBackendDev.BLL.Dto.Response;
using TestBackendDev.BLL.Services.Company;
using TestBackendDev.BLL.UnitOfWork;
using TestBackendDev.DAL;
using TestBackendDev.DAL.Models;

namespace TestBackendDev.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<TestBackendDevContext>(options =>
                {
                    options.UseMySql(Configuration.GetConnectionString("TestBackendDevConnectionString"));
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICompanyService, CompanyService>();

            #region Mapper config

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyModel, CompanyDto>();
                cfg.CreateMap<CompanyDto, CompanyModel>();

                cfg.CreateMap<EmployeeModel, EmployeeDto>();
                cfg.CreateMap<EmployeeDto, EmployeeModel>();

                // created response contains id only
                cfg.CreateMap<CompanyDto, CreatedResponseDto>();
                cfg.CreateMap<EmployeeDto, CreatedResponseDto>();
            });

            #endregion

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
