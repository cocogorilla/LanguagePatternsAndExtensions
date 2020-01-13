using System;

namespace LanguagePatternsAndExtensions
{
    public struct Option<T>
    {
        private readonly T _item;

        public Option(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _item = item;
            IsSome = true;
        }

        public Option(Unit none)
        {
            _item = default;
            IsSome = false;
        }

        public bool IsSome { get; }
        public bool IsNone => !IsSome;

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

            return (IsSome)
                ? just(_item)
                : nothing;
        }
    }
}