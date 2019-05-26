using System;
using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    /// <summary>
    /// Try the provided query and return Success of result or Failure of Empty of result type
    /// </summary>
    /// <typeparam name="TArgs">Arguments to Command</typeparam>
    public class TryAsyncOutcomeCommand<TArgs> : IAsyncOutcomeCommand<TArgs>
    {
        private readonly IAsyncOutcomeCommand<TArgs> _baseCommand;

        public TryAsyncOutcomeCommand(IAsyncOutcomeCommand<TArgs> baseCommand)
        {
            if (baseCommand == null) throw new ArgumentNullException(nameof(baseCommand));
            _baseCommand = baseCommand;
        }
        public async Task<Outcome<Unit>> SendCommandAsync(TArgs args)
        {
            try
            {
                return await _baseCommand.SendCommandAsync(args); ;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Failure.Of(Unit.Default, ex.Message));
            }
        }
    }

    /// <summary>
    /// Try the provided query and return Success of result or Failure of Empty of result type
    /// </summary>
    /// <typeparam name="TArgs">Arguments to Command</typeparam>
    public class TryOutcomeCommand<TArgs> : IOutcomeCommand<TArgs>
    {
        private readonly IOutcomeCommand<TArgs> _baseCommand;

        public TryOutcomeCommand(IOutcomeCommand<TArgs> baseCommand)
        {
            if (baseCommand == null) throw new ArgumentNullException(nameof(baseCommand));
            _baseCommand = baseCommand;
        }
        public Outcome<Unit> SendCommand(TArgs args)
        {
            try
            {
                return _baseCommand.SendCommand(args);
            }
            catch (Exception ex)
            {
                return Failure.Of(Unit.Default, ex.Message);
            }
        }
    }
}