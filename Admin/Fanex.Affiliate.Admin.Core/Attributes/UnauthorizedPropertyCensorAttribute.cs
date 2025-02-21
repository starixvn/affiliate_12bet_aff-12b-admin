using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class UnauthorizedPropertyCensorAttribute : ActionFilterAttribute
    {
        public string PermissionName { get; set; }
        private IEnumerable<UserPermission> PermissionList { get; set; }

        public UnauthorizedPropertyCensorAttribute(string permissionName = null)
        {
            PermissionName = permissionName;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();
            PermissionList = sessionService.UserPermissionList;

            var data = GetResultData(filterContext.Result);

            if (data != null && PermissionList.Any())
            {
                CensorData(data);
            }
            base.OnResultExecuting(filterContext);
        }

        private void CensorData(object data)
        {
            var type = data?.GetType();
            if (data == null || IsPrimitiveType(type))
            {
                return;
            }

            if (data is IEnumerable dataItems)
            {
                foreach (var item in dataItems)
                {
                    CensorData(item);
                }
            }
            else
            {
                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var pro in properties)
                {
                    var value = UpdateData(data, pro);
                    CensorData(value);
                }
            }
        }

        private object UpdateData(object data, PropertyInfo pro)
        {
            if (IsNeedResetPro(pro))
            {
                pro.SetValue(data, null, null);
            }
            else
            {
                if (IsNeedCensoredPro(pro))
                {
                    var valueOfPro = pro.GetValue(data, null)?.ToString() ?? string.Empty;
                    var newValueOfPro = valueOfPro.ReplaceAHalfOfString();

                    if (pro.Name.MatchWith(Field.Email))
                    {
                        newValueOfPro = valueOfPro.HideAnEmailPart();
                    }

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

        private bool IsNeedCensoredPro(PropertyInfo pro)
        {
            var attribute = pro.GetCustomAttribute<CensoredPermissionCheckAttribute>();
            return attribute != null && !HasPermission(attribute.PermissionName);
        }

        private bool HasPermission(string permissionName)
        {
            return (string.IsNullOrWhiteSpace(PermissionName) && permissionName == PermissionName)
                || PermissionList.Any(p => p.FunctionName == permissionName);
        }

        private static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type.IsValueType || type == typeof(string);
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
    }
}
