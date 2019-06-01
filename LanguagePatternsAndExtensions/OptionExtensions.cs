using System.Collections.Generic;

namespace LanguagePatternsAndExtensions
{
    public static class OptionExtensions
    {
        public static Option<T> ToOption<T>(this T item)
        {
            return Option<T>.Some(item);
        }
        public static Option<T> ToOption<T>(this IEnumerable<T> item)
        {
            return Option<T>.Some(item);
        }
    }
}