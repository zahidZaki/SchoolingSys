using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassInfo.BLL.Concrete;
using ClassInfo.BLL.Interface;
using ClassInfo.DLL.Concrete.Base;
using ClassInfo.DLL.Interfaces.IBase;
using Microsoft.Extensions.DependencyInjection;

namespace ClassInfo.BLL.RegisterServices
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IClassService), typeof(ClassService));

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient(typeof(IUsernameAndPasswordIssuesScenarioService), typeof(UsernameAndPasswordIssuesScenarioService));

            return services;
        }
    }
}
