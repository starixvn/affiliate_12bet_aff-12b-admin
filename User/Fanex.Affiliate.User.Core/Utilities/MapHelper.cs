namespace Fanex.Affiliate.User.Core.Utilities
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class MapHelper
    {
        private const string IsInternalPropName = "IsInternal";

        public static TDestination MapTo<TDestination>(this object source, bool isInternal = false)
        {
            if (source == null)
            {
                return default(TDestination);
            }

            var destination = Activator.CreateInstance<TDestination>();

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
