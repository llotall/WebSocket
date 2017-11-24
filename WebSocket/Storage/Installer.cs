using Storage.Interfaces;
using Storage.Repsitories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Storage
{
    public static class Installer
    {
        public static void AddNHibernate(this IServiceCollection container, IConfiguration config)
        {
            container.AddSingleton(typeof(NHibernateConfigurator.ISessionFactory), new NHibernateConfigurator.NHibernateConfiguration(config));
            container.AddScoped<ISessionStorage, SessionStorage>();
            container.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
