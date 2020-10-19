using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class BaseService
    {
        protected IAuthService AuthService { get; }

        protected ISessionService SessionService { get; }

        protected BaseService(IAuthService authService, ISessionService sessionService)
        {
            AuthService = authService;
            SessionService = sessionService;
        }

        protected void SetNullForPropertiesNotHavePermission(object param)
        {
            var permission = AuthService.GetClaims(SessionService.UserId);
            var type = param.GetType();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<SpParamProtectingAttribute>();
                if (attr != null && !permission.Any(p => p.Type == attr.PermissionType && p.Value == attr.PermissionValue))
                {
                    prop.SetValue(param, null, null);
                }
            }
        }
    }
}
