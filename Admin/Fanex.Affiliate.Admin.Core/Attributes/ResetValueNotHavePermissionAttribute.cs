using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
    public class ResetValueNotHavePermissionAttribute : ActionFilterAttribute
    {
        private string PermissionName { get; }
        private IEnumerable<UserPermission> Permission { get; set; }

        public ResetValueNotHavePermissionAttribute(string permissionName)
        {
            PermissionName = permissionName;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            Permission = context.Session[SessionKey.UserPermissionSessionKey] as IEnumerable<UserPermission>;
            var data = GetResultData(filterContext.Result);
            if (data != null && Permission != null)
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
                    var attribute = pro.GetCustomAttribute<PermissionCheckAttribute>();
                    if(attribute != null && attribute.PermissionName == PermissionName && !Permission.Any(p => p.FunctionName == attribute.PermissionName))
                    {
                        pro.SetValue(data, null, null);
                    }

                    var value = pro.GetValue(data, null);
                    ClearData(value);
                }
            }
        }

        private static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type.IsValueType || type == typeof(string);
        }
    }
}
