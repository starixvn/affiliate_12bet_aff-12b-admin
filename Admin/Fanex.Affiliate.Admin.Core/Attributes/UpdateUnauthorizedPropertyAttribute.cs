using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class UpdateUnauthorizedPropertyAttribute : ActionFilterAttribute
    {
        public string PermissionName { get; }
        private IEnumerable<Claim> Claims { get; set; }

        private bool IsPendingAff { get; set; }

        public UpdateUnauthorizedPropertyAttribute(string permissionName = null)
        {
            PermissionName = permissionName;
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            var userClaim = context.User as ClaimsPrincipal;
            Claims = userClaim.Claims;
            var data = GetResultData(filterContext.Result);
            if (data != null && Claims != null)
            {
                CensorData(data);
            }
            base.OnResultExecuting(filterContext);
        }

        private static object GetResultData(ActionResult result)
        {
            switch (result)
            {
                case JsonResult jsonResult:
                    return jsonResult.Data;
                case JsonNetResult jsonNetResult:
                    return jsonNetResult.Data;
                case ViewResultBase viewResult:
                    return viewResult.Model;
                default:
                    throw new InvalidOperationException(result.GetType().Name);
            }
        }

        private void CensorData(object data)
        {
            var type = data?.GetType();
            if (data == null || IsPrimitiveType(type))
            {
                return;
            }

            if (data is IEnumerable)
            {
                foreach (var item in data as IEnumerable)
                {
                    CensorData(item);
                }
            }
            else
            {
                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (!IsPendingAff)
                {
                    CheckPendingAff(data, properties);
                }

                foreach (var pro in properties)
                {
                    var value = UpdateData(data, pro);
                    CensorData(value);
                }
            }
        }

        private object UpdateData(object data, PropertyInfo pro)
        {
            if (IsNeedResetPro(pro) || IsNeedResetProMultiPermissions(pro))
            {
                pro.SetValue(data, null, null);
            }
            else
            {
                if (IsNeedCensoredPro(pro))
                {
                    var valueOfPro = pro.GetValue(data, null);
                    var newValueOfPro = valueOfPro?.ToString().ReplaceAHalfOfString() ?? string.Empty;
                    pro.SetValue(data, newValueOfPro, null);
                }
            }

            return pro.GetValue(data, null);
        }

        private bool IsNeedResetPro(PropertyInfo pro)
        {
            var attribute = pro.GetCustomAttribute<PermissionCheckAttribute>();
            return attribute != null && !HasPermission(attribute.PermissionName);
        }

        private bool IsNeedResetProMultiPermissions(PropertyInfo pro)
        {
            var attribute = pro.GetCustomAttribute<ManagePermissionCheckAttribute>();
            return attribute != null && !(HasPermission(attribute.PermissionName) || (IsPendingAff && HasPermission(attribute.PermissionNameOption)));
        }

        private bool IsNeedCensoredPro(PropertyInfo pro)
        {
            var attribute = pro.GetCustomAttribute<CensoredPermissionCheckAttribute>();
            return attribute != null && !HasPermission(attribute.PermissionName);
        }

        private void CheckPendingAff(object data, PropertyInfo[] props)
        {
            IsPendingAff = Convert.ToBoolean(props.FirstOrDefault(p => p.Name.MatchWith(Field.IsPendingAff))?.GetValue(data, null) ?? false);
        }

        private bool HasPermission(string permissionName)
        {
            return (string.IsNullOrWhiteSpace(PermissionName) && permissionName == PermissionName)
                || Claims.Any(p => p.Type == permissionName);
        }

        private static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type.IsValueType || type == typeof(string);
        }
    }
}
