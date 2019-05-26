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
    public class TryAsyncOutcomeQuery<TArgs, TResult> : IAsyncOutcomeQuery<TArgs, IEnumerable<TResult>>
    {
        private readonly IAsyncOutcomeQuery<TArgs, IEnumerable<TResult>> _baseQuery;

        public TryAsyncOutcomeQuery(IAsyncOutcomeQuery<TArgs, IEnumerable<TResult>> baseQuery)
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

    public class TryAsyncOutcomeOptionQuery<TArgs, TResult> : IAsyncOutcomeQuery<TArgs, Option<TResult>>
    {
        private readonly IAsyncOutcomeQuery<TArgs, Option<TResult>> _baseQuery;

        public TryAsyncOutcomeOptionQuery(IAsyncOutcomeQuery<TArgs, Option<TResult>> baseQuery)
        {
            if (baseQuery == null) throw new ArgumentNullException(nameof(baseQuery));
            _baseQuery = baseQuery;
        }
        public async Task<Outcome<Option<TResult>>> SendQuery(TArgs args)
        {
            try
            {
                return await _baseQuery.SendQuery(args);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Failure.Of(Option<TResult>.None(), ex.Message));
            }
        }
    }

    /// <summary>
    /// Try the provided query and return Success of result or Failure of Empty of result type
    /// </summary>
    /// <typeparam name="TArgs">Query Arguments</typeparam>
    /// <typeparam name="TResult">Result Type</typeparam>
    public class TryOutcomeQuery<TArgs, TResult> : IOutcomeQuery<TArgs, IEnumerable<TResult>>
    {
        private readonly IOutcomeQuery<TArgs, IEnumerable<TResult>> _baseQuery;

        public TryOutcomeQuery(IOutcomeQuery<TArgs, IEnumerable<TResult>> baseQuery)
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

    public class TryOutcomeOptionQuery<TArgs, TResult> : IOutcomeQuery<TArgs, Option<TResult>>
    {
        private readonly IOutcomeQuery<TArgs, Option<TResult>> _baseQuery;

        public TryOutcomeOptionQuery(IOutcomeQuery<TArgs, Option<TResult>> baseQuery)
        {
            if (baseQuery == null) throw new ArgumentNullException(nameof(baseQuery));
            _baseQuery = baseQuery;
        }
        /// <summary>
        /// Try the provided query and return Success of result as an option or Failure of an empty option
        /// </summary>
        /// <param name="args">Query Arguments</param>
        /// <returns>Result Type</returns>
        public Outcome<Option<TResult>> SendQuery(TArgs args)
        {
            try
            {
                return _baseQuery.SendQuery(args);
            }
            catch (Exception ex)
            {
                return Failure.Of(Option<TResult>.None(), ex.Message);
            }
        }
    }
}
