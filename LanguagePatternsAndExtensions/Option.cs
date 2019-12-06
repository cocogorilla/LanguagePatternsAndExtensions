using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public struct Option<T>
    {
        private readonly T _item;
        private readonly bool _hasItem;

        public Option(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _item = item;
            _hasItem = true;
        }

        public Option(Unit none)
        {
            _item = default(T);
            _hasItem = false;
        }

        public static Option<T> Some(IEnumerable<T> source)
        {
            if (source.Count() > 1)
                throw new ArgumentException("source for Option cannot enumerate more than once");

            var item = source.SingleOrDefault();

            return item != null
                ? new Option<T>(item)
                : new Option<T>(Unit.Default);
        }

        public static Option<T> Some(T source)
        {
            return source == null
                ? None()
                : new Option<T>(source);
        }

        public static Option<T> None()
        {
            return new Option<T>(Unit.Default);
        }

        public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
        {
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));
            if (just == null) throw new ArgumentNullException(nameof(just));

            return (_hasItem)
                ? just(_item)
                : nothing;
        }
    }
}