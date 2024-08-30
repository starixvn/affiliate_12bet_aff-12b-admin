using System;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class SpParamProtectingAttribute : Attribute
    {
        public string PermissionType { get; }

        public SpParamProtectingAttribute(string permissionType)
        {
            PermissionType = permissionType;
        }
    }
}
