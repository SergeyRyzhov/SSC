using System.Linq;
using System.Web.ApplicationServices;
using System.Web.Security;

using WebMatrix.WebData;

namespace Leaf.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();

            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
            const string userName = "sryzhov";
            const string admin = "Admin";
            const string manager = "Manager";


            if (WebSecurity.UserExists(userName))
            {
                var roles = Roles.GetAllRoles();

                if (!roles.Contains(admin))
                {
                    Roles.CreateRole(admin);
                }

                if (!Roles.IsUserInRole(userName, admin))
                {
                    Roles.AddUserToRole(userName, admin);
                }

                if (!roles.Contains(manager))
                {
                    Roles.CreateRole(manager);
                }

                if (!Roles.IsUserInRole(userName, manager))
                {
                    Roles.AddUserToRole(userName, manager);
                }
            }
        }
    }
}