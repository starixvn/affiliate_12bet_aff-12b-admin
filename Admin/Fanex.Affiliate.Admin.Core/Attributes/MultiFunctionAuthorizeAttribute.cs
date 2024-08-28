using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class MultiFunctionAuthorizeAttribute : FunctionAuthorizeAttribute
    {
        public string FunctionNameOption { get; set; }

        protected override bool HasPerimission(IPrincipal principal)
        {
            if (principal is ClaimsPrincipal claims)
            {
                if (string.IsNullOrEmpty(FunctionNameOption))
                {
                    return HasFuncClaim(claims, FunctionName);
                }

                return HasFuncClaim(claims, FunctionName) || HasFuncClaim(claims, FunctionNameOption);
            }

            return false;
        }
    }
}
