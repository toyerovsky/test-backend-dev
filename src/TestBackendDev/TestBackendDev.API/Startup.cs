using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using TestBackendDev.API.BasicAuthentication;
using TestBackendDev.BLL.Dto;
using TestBackendDev.BLL.Dto.Response;
using TestBackendDev.BLL.Services.Company;
using TestBackendDev.BLL.UnitOfWork;
using TestBackendDev.DAL;
using TestBackendDev.DAL.Enums;
using TestBackendDev.DAL.Models;
using ZNetCS.AspNetCore.Authentication.Basic;

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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddDbContext<TestBackendDevContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("TestBackendDevConnectionString"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICompanyService, CompanyService>();

            #region Mapper config

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

                cfg.CreateMap<CompanyModel, CompanyDto>()
                    .PreserveReferences()
                    .EqualityComparison((model, dto) => model.Id == dto.Id)
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<CompanyDto, CompanyModel>()
                    .PreserveReferences()
                    .EqualityComparison((dto, model) => model.Id == dto.Id)
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                cfg.CreateMap<EmployeeModel, EmployeeDto>()
                    .PreserveReferences()
                    .EqualityComparison((model, dto) => model.Id == dto.Id)
                    .ForMember(
                        employeeDto => employeeDto.JobTitle,
                        opt => opt.ResolveUsing(
                            model => model.JobTitle.ToString()))
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                cfg.CreateMap<EmployeeDto, EmployeeModel>()
                    .PreserveReferences()
                    .EqualityComparison((dto, model) => model.Id == dto.Id)
                    .ForMember(
                        employeeModel => employeeModel.JobTitle,
                        opt => opt.ResolveUsing(
                            model => Enum.Parse(typeof(JobTitle), model.JobTitle)))
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                // created response contains id only
                cfg.CreateMap<CompanyDto, CreatedResponseDto>();
                cfg.CreateMap<EmployeeDto, CreatedResponseDto>();
            });

            #endregion

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);

            services.AddScoped<AuthenticationEvents>();

            services
                .AddAuthentication()
                .AddBasicAuthentication(opt =>
                {
                    opt.Realm = "TestBackendDev";
                    opt.EventsType = typeof(AuthenticationEvents);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
