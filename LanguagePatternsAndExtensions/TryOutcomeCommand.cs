using System;
using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
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