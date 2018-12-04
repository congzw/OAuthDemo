using System.Collections.Generic;
using System.Linq;
// ReSharper disable once CheckNamespace

namespace Demos.Common
{
    public static class EnumableExtensions
    {
        public static IList<T> ToListOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null ? new List<T>() : enumerable.ToList();
        }
    }
}