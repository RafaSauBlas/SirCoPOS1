using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace SirCoPOS.Web.Helpers
{
    public static class SecurityHelper
    {
        public static int? GetUID(this IPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated)
                return null;
            var claims = ((ClaimsIdentity)principal.Identity);
            var item = claims.Claims.Where(i => i.Type == Claims.SirCoUID).SingleOrDefault();
            if (item != null)
                return int.Parse(item.Value);
            return null;
        }
    }
}