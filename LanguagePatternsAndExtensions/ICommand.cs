using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public interface ICommand
    {
        Outcome<Unit> SendCommand(object args);
    }
    public interface IAsyncCommand
    {
        Task<Outcome<Unit>> SendCommandAsync(object args);
    }
    public interface ICommand<in TArgs>
    {
        Outcome<Unit> SendCommand(TArgs args);
    }
    public interface IAsyncCommand<in TArgs>
    {
        Task<Outcome<Unit>> SendCommandAsync(TArgs args);
    }
}

