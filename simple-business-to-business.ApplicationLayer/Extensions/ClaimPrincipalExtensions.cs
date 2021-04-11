using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.Email);

        public static string GetUsername(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.Name);

        public static int GetUserId(this ClaimsPrincipal principal) => Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier));

        public static bool IsCurentUser(this ClaimsPrincipal principal, string id) => string.Equals(GetUserId(principal).ToString(), id, StringComparison.OrdinalIgnoreCase);
    }
}
