using System.Collections.Generic;

namespace LanguagePatternsAndExtensions
{
    public static class OutcomeFactory
    {
        public static TryAsyncOutcomeQuery<TArgs, TResult> TryAsyncOutcome<TArgs, TResult>(
            IAsyncQuery<TArgs, IEnumerable<TResult>> baseQuery)
        {
            return new TryAsyncOutcomeQuery<TArgs, TResult>(baseQuery);
        }

        public static TryAsyncOutcomeCommand<TArgs> TryAsyncOutcome<TArgs>(
            IAsyncCommand<TArgs> baseCommand)
        {
            return new TryAsyncOutcomeCommand<TArgs>(baseCommand);
        }

        public static TryOutcomeQuery<TArgs, TResult> TryOutcome<TArgs, TResult>(
            IQuery<TArgs, IEnumerable<TResult>> baseQuery)
        {
            return new TryOutcomeQuery<TArgs, TResult>(baseQuery);
        }

        public static TryOutcomeCommand<TArgs> TryOutcome<TArgs>(
            ICommand<TArgs> baseCommand)
        {
            return new TryOutcomeCommand<TArgs>(baseCommand);
        }

        public static TryAsyncOutcomeOptionQuery<TArgs, TResult> TryAsyncOutcome<TArgs, TResult>(
            IAsyncQuery<TArgs, Option<TResult>> baseQuery)
        {
            return new TryAsyncOutcomeOptionQuery<TArgs, TResult>(baseQuery);
        }

        public static TryOutcomeOptionQuery<TArgs, TResult> TryOutcome<TArgs, TResult>(
            IQuery<TArgs, Option<TResult>> baseQuery)
        {
            return new TryOutcomeOptionQuery<TArgs, TResult>(baseQuery);
        }
    }
}