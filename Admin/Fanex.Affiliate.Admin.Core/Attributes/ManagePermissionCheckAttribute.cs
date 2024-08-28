using Fanex.Affiliate.Admin.Core.Constants;
using System;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ManagePermissionCheckAttribute : Attribute
    {
        public string PermissionName { get; }

        public string PermissionNameOption { get; }

        public ManagePermissionCheckAttribute(string permissionName)
        {
            PermissionName = permissionName;
            PermissionNameOption = Function.ManagePendingAff;
        }
    }
}
