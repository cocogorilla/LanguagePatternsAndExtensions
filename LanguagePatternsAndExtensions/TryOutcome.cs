using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public class TryOutcomeQuery<TArgs, TResult> : IQuery<TArgs, IEnumerable<TResult>>
    {
        private readonly IQuery<TArgs, IEnumerable<TResult>> _baseQuery;

        public TryOutcomeQuery(IQuery<TArgs, IEnumerable<TResult>> baseQuery)
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

    public class TryOutcomeCommand<TArgs> : ICommand<TArgs>
    {
        private readonly ICommand<TArgs> _baseCommand;

        public TryOutcomeCommand(ICommand<TArgs> baseCommand)
        {
            if (baseCommand == null) throw new ArgumentNullException(nameof(baseCommand));
            _baseCommand = baseCommand;
        }
        public async Task<Outcome<Unit>> SendCommand(TArgs args)
        {
            try
            {
                return await _baseCommand.SendCommand(args); ;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Failure.Of(Unit.Default, ex.Message));
            }
        }
    }
}
