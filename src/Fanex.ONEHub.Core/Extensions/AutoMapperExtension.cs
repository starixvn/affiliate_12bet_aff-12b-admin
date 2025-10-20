using System.Linq;
using System.Reflection;

namespace Fanex.Affiliate.Admin.Core.Extensions
{
    public static class AutoMapperExtension
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source) where TDestination : class, new()
        {
            var destination = new TDestination();

            if (Equals(source, default(TSource)))
            {
                return destination;
            }

            var sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var destinationPropertyDict = destinationProperties.ToDictionary(p => p.Name);

            foreach (var sourceProp in sourceProperties)
            {
                if (destinationPropertyDict.TryGetValue(sourceProp.Name, out var destinationProp) && destinationProp.CanWrite)
                {
                    var value = sourceProp.GetValue(source);
                    if (value != null)
                    {
                        destinationProp.SetValue(destination, value);
                    }
                }
            }

            return destination;
        }
    }
}
