using Application.Models.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Application.App_Start.Startup))]

namespace Application.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new UserManager(new DatabaseContext().GenerateUserStore()));
            // Установить аутентфиикацию на основе куки
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                // установить куки аутентификацию в приложении, как основную 
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                // URL по которому будут перенаправляться неавторизованные пользователи
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}