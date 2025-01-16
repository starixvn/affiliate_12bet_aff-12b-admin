namespace Fanex.Affiliate.User.Core.Utilities
{
    using System.Linq;
    using System.Reflection;

    public static class MapHelper
    {
        private const string IsInternalPropName = "IsInternal";

        public static TDestination MapTo<TSource, TDestination>(this TSource source, bool isInternal = false) where TDestination : class, new()
        {
            var destination = new TDestination();

            if (Equals(source, default(TSource)))
            {
                return destination;
            }

            var sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var sourcePropertyDict = sourceProperties.ToDictionary(p => p.Name);
            var destinationPropertyDict = destinationProperties.ToDictionary(p => p.Name);

            if (!sourcePropertyDict.ContainsKey(IsInternalPropName) && destinationPropertyDict.TryGetValue(IsInternalPropName, out var isInternalProp))
            {
                isInternalProp.SetValue(destination, isInternal);
            }

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
