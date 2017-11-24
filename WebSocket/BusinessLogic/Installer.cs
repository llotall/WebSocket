using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Installer
    {
        public static void AddBuisnessServices(this IServiceCollection container)
        {
            //container.AddScoped<IAdvertService, AdvertService>();
        }
    }
}
