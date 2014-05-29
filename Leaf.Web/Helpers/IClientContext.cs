using System.Security.Principal;

namespace Leaf.Web.Helpers
{
    public interface IClientContext
    {
        IPrincipal User { get; }
    }
}