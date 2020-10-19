using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class SpParamProtectingAttribute : Attribute
    {
        public string PermissionType { get; }

        public string PermissionValue { get; }

        public SpParamProtectingAttribute(string permissionType, string permissionValue)
        {
            PermissionType = permissionType;
            PermissionValue = permissionValue;
        }
    }
}
