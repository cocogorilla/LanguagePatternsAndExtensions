using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    /// <summary>
    ///  non generic
    /// </summary>
    public interface ICommand
    {
        Unit SendComand(object args);
    }
    public interface IOutcomeCommand
    {
        Outcome<Unit> SendCommand(object args);
    }
    public interface IAsyncCommand
    {
        Task<Unit> SendCommand(object args);
    }
    public interface IAsyncOutcomeCommand
    {
        Task<Outcome<Unit>> SendCommandAsync(object args);
    }
    
    /// <summary>
    /// generic
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public interface ICommand<TArgs>
    {
        Unit SendComand(TArgs args);
    }
    public interface IOutcomeCommand<TArgs>
    {
        Outcome<Unit> SendCommand(TArgs args);
    }
    public interface IAsyncCommand<TArgs>
    {
        Task<Unit> SendCommand(TArgs args);
    }
    public interface IAsyncOutcomeCommand<TArgs>
    {
        Task<Outcome<Unit>> SendCommandAsync(TArgs args);
    }
}

