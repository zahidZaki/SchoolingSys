using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Teacherinfo.BLL.Concrete;
using Teacherinfo.BLL.Concrete.Base;
using Teacherinfo.BLL.Interfaces;
using Teacherinfo.DLL.Concrete.Base;
using Teacherinfo.DLL.Interfaces.IBase;

namespace Teacherinfo.BLL.RegisterServices
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(ITeacherService), typeof(TeacherService));

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient(typeof(IUsernameAndPasswordIssuesScenarioService), typeof(UsernameAndPasswordIssuesScenarioService));

            return services;
        }
    }
}
