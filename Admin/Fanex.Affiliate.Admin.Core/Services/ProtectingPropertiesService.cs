using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System;
using System.Linq;
using System.Reflection;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class ProtectingPropertiesService : ManagePermissionService
    {
        public ProtectingPropertiesService(
            IAuthService authService,
            ISessionService sessionService,
            IAffiliateService affiliateService) : base(authService, sessionService, affiliateService) { }

        protected void SetNullForPropertiesNotHavePermission(object param)
        {
            var type = param.GetType();
            var props = type.GetProperties();
            var hasManagePendingAffHostOnProps = HasManagePendingAffHostOnProps(param, props);
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<SpParamProtectingAttribute>();
                if (attr != null && !HasHostFunctionPermission(attr.PermissionType) && !hasManagePendingAffHostOnProps)
                {
                    prop.SetValue(param, null, null);
                }
            }
        }

        private bool HasManagePendingAffHostOnProps(object data, PropertyInfo[] props)
        {
            var isHost = HasHostFunctionPermission(Function.ManagePendingAff);

            return isHost && Convert.ToBoolean(props.FirstOrDefault(p => p.Name.MatchWith(Field.IsPendingAff))?.GetValue(data, null) ?? false);
        }
    }
}
