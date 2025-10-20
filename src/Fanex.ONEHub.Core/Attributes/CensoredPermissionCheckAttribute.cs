using System;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class CensoredPermissionCheckAttribute : Attribute
    {
        public string PermissionName { get; }

        public CensoredPermissionCheckAttribute(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
