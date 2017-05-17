using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    /// <summary>
    /// Try the provided query and return Success of result or Failure of Empty of result type
    /// </summary>
    /// <typeparam name="TArgs">Query Arguments</typeparam>
    /// <typeparam name="TResult">Result Type</typeparam>
    public class TryAsyncOutcomeQuery<TArgs, TResult> : IAsyncQuery<TArgs, IEnumerable<TResult>>
    {
        private readonly IAsyncQuery<TArgs, IEnumerable<TResult>> _baseQuery;

        public TryAsyncOutcomeQuery(IAsyncQuery<TArgs, IEnumerable<TResult>> baseQuery)
        {
            if (baseQuery == null) throw new ArgumentNullException(nameof(baseQuery));
            _baseQuery = baseQuery;
        }

        public async Task<Outcome<IEnumerable<TResult>>> SendQuery(TArgs args)
        {
            try
            {
                return await _baseQuery.SendQuery(args);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Failure.Of(Enumerable.Empty<TResult>(), ex.Message));
            }
        }
    }

    /// <summary>
    /// Try the provided query and return Success of result or Failure of Empty of result type
    /// </summary>
    /// <typeparam name="TArgs">Query Arguments</typeparam>
    /// <typeparam name="TResult">Result Type</typeparam>
    public class TryOutcomeQuery<TArgs, TResult> : IQuery<TArgs, IEnumerable<TResult>>
    {
        private readonly IQuery<TArgs, IEnumerable<TResult>> _baseQuery;

        public TryOutcomeQuery(IQuery<TArgs, IEnumerable<TResult>> baseQuery)
        {
            if (baseQuery == null) throw new ArgumentNullException(nameof(baseQuery));
            _baseQuery = baseQuery;
        }

        public Outcome<IEnumerable<TResult>> SendQuery(TArgs args)
        {
            try
            {
                return _baseQuery.SendQuery(args);
            }
            catch (Exception ex)
            {
                return Failure.Of(Enumerable.Empty<TResult>(), ex.Message);
            }
        }
    }
}
