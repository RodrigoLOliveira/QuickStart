using System.Reflection;
using Template.Business.Services.Generics;

namespace Template.Server.Configurations
{
    public static class ServiceRegistration
    {
        public static void AddScopedServices(this IServiceCollection services)
        {
            // Obtém o assembly atual ou pode especificar outro
            var assembly = Assembly.Load("Template.Business");

            // Procura por todas as classes que são subtipos de ServiceBase
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(ServiceBase)));

            foreach (var type in serviceTypes)
            {
                // Encontra a interface correspondente, assumindo que a interface segue uma convenção de nomenclatura I[NomeDaClasse]
                var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}");

                if (interfaceType != null)
                {
                    // Adiciona ao serviço usando AddScoped
                    services.AddScoped(interfaceType, type);
                }
                else
                {
                    // Se não houver interface correspondente, registra o tipo diretamente
                    services.AddScoped(type);
                }
            }
        }
    }
}
