using System;
using System.Collections;
using System.Collections.Generic;

namespace LanguagePatternsAndExtensions
{
    public static class OptionExtensions
    {
        public static Option<T> ToOption<T>(this T item)
        {
            return Option<T>.Some(item);
        }

        public static Option<TResult> SelectMany<TSource, TResult>(
            this Option<TSource> source,
            Func<TSource, Option<TResult>> selector)
        {
            return source.Match(
                Option<TResult>.None(),
                selector);
        }

        public static Option<TResult> SelectMany<TSource, TIntermediate, TResult>(
            this Option<TSource> source,
            Func<TSource, Option<TIntermediate>> intermediateProjection,
            Func<TSource, TIntermediate, TResult> resultSelector)
        {

            return source.Match(
                Option<TResult>.None(),
                x =>
                {
                    var elem = intermediateProjection(x);
                    return elem.Match(
                        Option<TResult>.None(),
                        y => resultSelector(x, y).ToOption());
                });
        }
    }
}