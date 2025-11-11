using SchoolingSysApiGateway.Concrete.Base;
using SchoolingSysApiGateway.Interfaces.IBase;

namespace SchoolingSysApiGateway.RegisterServices
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddTransient(typeof(ITeleCare1Data), typeof(TeleCare1Data));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
