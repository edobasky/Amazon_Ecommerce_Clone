using Microsoft.EntityFrameworkCore;
using Ordering.Data;
using Ordering.Repositories;

namespace Ordering.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(
                configuration.GetConnectionstring("OrderConnection"),
                sqlServiceOptions => sqlServiceOptions.EnableRetryOnFailure()));
            services.AddScoped(typeof(IAsyncRepository<>),typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository,OrderRepository>();

            return services;
        }
    }
}
