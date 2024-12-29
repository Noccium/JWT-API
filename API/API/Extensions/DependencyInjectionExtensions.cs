using System.Reflection;

namespace API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddServicesFromNamespace(this IServiceCollection services, string namespacePrefix, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            var serviceTypes = types.Where(type => type.Namespace != null && type.Namespace.StartsWith(namespacePrefix) && type.IsInterface);

            foreach (var serviceType in serviceTypes)
            {
                var implementationType = types.FirstOrDefault(type =>
                    type.GetInterfaces().Contains(serviceType) && !type.IsInterface && !type.IsAbstract);

                if (implementationType != null)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(serviceType, implementationType);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(serviceType, implementationType);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(serviceType, implementationType);
                            break;
                    }
                }
            }
        }
    }

}
