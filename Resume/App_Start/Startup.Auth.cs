using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Provider;
using Owin;
using System.Configuration;
using Owin.Security.Providers.LinkedIn;
using Resume.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Resume
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            // App Harbor load balancers run HTTP internally. This will confuse ASP.NET into believing a secure connection is not
            app.Use(async (context, next) =>
            {
                if (string.Equals(context.Request.Headers["X-Forwarded-Proto"], "https", StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Request.Scheme = "https";
                }

                await next.Invoke();
            });

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            // Google Oauth2 provider.
            app.UseGoogleAuthentication(ConfigurationManager.AppSettings["GoogleAuthKey"], ConfigurationManager.AppSettings["GoogleAuthSecret"]);

            // LinkedIn
            var linkedInSettings = new LinkedInAuthenticationOptions()
            {
                ClientId = ConfigurationManager.AppSettings["LinkedInKey"],
                ClientSecret = ConfigurationManager.AppSettings["LinkedInSecret"]
            };
            linkedInSettings.Scope.Add("r_basicprofile");

            linkedInSettings.Provider = new LinkedInAuthenticationProvider()
            {
                OnAuthenticated = async context =>
                    {
                        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                        var user = userManager.FindByName(context.Request.User.Identity.Name);
                        userManager.AddClaim(user.Id, new Claim("LinkedIn_AccessToken", context.AccessToken));
                    }
            };

            linkedInSettings.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseLinkedInAuthentication(linkedInSettings);

            // Owin context for claims
        }
    }
}