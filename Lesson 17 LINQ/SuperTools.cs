using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_17_LINQ {
    internal static class SuperTools {
        public static IEnumerable<T> MyFilter<T>(this IEnumerable<T> collection, T text) => collection.Where(item => item.Equals(text));
    }
}
