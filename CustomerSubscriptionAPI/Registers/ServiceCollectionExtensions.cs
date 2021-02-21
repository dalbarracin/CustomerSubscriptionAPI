using CustomerSubscriptionAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerSubscriptionAPI.Registers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomerSubscriptionScoped(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ISubscriptionRepository, SubscriptionRepository>();
        }
    }
}
