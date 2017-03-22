using System.Collections.Generic;

namespace LanguagePatternsAndExtensions
{
    public static class OutcomeFactory
    {
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
    }
}