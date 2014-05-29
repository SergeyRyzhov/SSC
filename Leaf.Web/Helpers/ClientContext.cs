using System.Security.Principal;
using System.Web;

using Leaf.Web.Repositories;

namespace Leaf.Web.Helpers
{
    public sealed class ClientContext : IClientContext
    {
        private static IClientContext current;

        private IPrincipal m_user;

        private ClientContext()
        {
        }

        public static IClientContext Current
        {
            get
            {
                return current ?? (current = new ClientContext());
            }
        }

        public IPrincipal User
        {
            get
            {
                return this.m_user ?? (this.m_user = HttpContext.Current.User);
            }
        }
    }
}