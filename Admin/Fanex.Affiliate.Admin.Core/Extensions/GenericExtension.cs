using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Extensions
{
    public static class GenericExtension
    {
        public static IEnumerable<T> ExcludeEmpty<T>(this IEnumerable<T> generic)
        {
            return generic != null && generic.Any() ? generic : null;
        }
    }
}
