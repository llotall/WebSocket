using BusinessLogic.Interfaces.Base.CRUD;
using BusinessLogic.Services.Base.CRUD;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Installer
    {
        public static void AddBuisnessServices(this IServiceCollection container)
        {
            container.AddScoped<IUserService, UserService>();
            container.AddScoped<IUserSessionService, UserSessionService>();
        }
    }
}
