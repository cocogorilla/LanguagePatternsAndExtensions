using System.Collections.Generic;

namespace LanguagePatternsAndExtensions
{
    public static class OptionExtensions
    {
        public static Option<T> Some<T>(this T item)
        {
            return Option<T>.Some(item.AsEnumerable());
        }

        public static Option<T> None<T>()
        {
            return Option<T>.None();
        }

        public static Option<T> ToOption<T>(this IEnumerable<T> item)
        {
            return new Option<T>(item);
        }
    }
}