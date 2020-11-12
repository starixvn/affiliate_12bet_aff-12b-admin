using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Common;
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
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
    public class ResetDefaultUnauthorizedPropertyAttribute : ActionFilterAttribute
    {
        public string PermissionName { get; }
        private IEnumerable<Claim> Claims { get; set; }

        public ResetDefaultUnauthorizedPropertyAttribute(string permissionName = null)
        {
            PermissionName = permissionName;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            var userClaim = context.User as ClaimsPrincipal ;
            Claims = userClaim.Claims;
            var data = GetResultData(filterContext.Result);
            if (data != null && Claims != null)
            {
                ClearData(data);
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

        private void ClearData(object data)
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
                    ClearData(item);
                }
            }
            else
            {
                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var pro in properties)
                {
                    if(IsNeedResetPro(pro))
                    {
                        pro.SetValue(data, null, null);
                    }

                    var value = pro.GetValue(data, null);
                    ClearData(value);
                }
            }
        }

        private bool IsNeedResetPro(PropertyInfo pro)
        {
            var attribute = pro.GetCustomAttribute<PermissionCheckAttribute>();
            return attribute != null 
                && (string.IsNullOrWhiteSpace(PermissionName) || attribute.PermissionName == PermissionName) 
                && !Claims.Any(p => p.Type == attribute.PermissionName);
        }

        private static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type.IsValueType || type == typeof(string);
        }
    }
}
