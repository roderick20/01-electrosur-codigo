using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Electrosur
{
    public static class Register
    {        
        public static void ConfigureAppServices(this IServiceCollection self, IConfiguration configuration)
        {
            self.AddSingleton(typeof(ILoginService), typeof(LoginService));
        }
    }
}
