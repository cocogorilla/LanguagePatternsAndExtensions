using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LanguagePatternsAndExtensions
{
    public struct Option<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _source;

        public Option(IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Count() > 1) throw new ArgumentException("source enumerates more than 1 item");
            _source = source;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Option<T> Some(IEnumerable<T> source)
        {
            return new Option<T>(source);
        }

        public static Option<T> Some(T source)
        {
            return new Option<T>(source.AsEnumerable());
        }

        public static Option<T> None()
        {
            return new Option<T>(Enumerable.Empty<T>());
        }
    }

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