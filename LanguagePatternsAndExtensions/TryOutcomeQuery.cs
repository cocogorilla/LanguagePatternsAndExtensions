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

    public class TryAsyncOutcomeOptionQuery<TArgs, TResult> : IAsyncQuery<TArgs, Option<TResult>>
    {
        private readonly IAsyncQuery<TArgs, Option<TResult>> _baseQuery;

        public TryAsyncOutcomeOptionQuery(IAsyncQuery<TArgs, Option<TResult>> baseQuery)
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

    public class TryOutcomeOptionQuery<TArgs, TResult> : IQuery<TArgs, Option<TResult>>
    {
        private readonly IQuery<TArgs, Option<TResult>> _baseQuery;

        public TryOutcomeOptionQuery(IQuery<TArgs, Option<TResult>> baseQuery)
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
