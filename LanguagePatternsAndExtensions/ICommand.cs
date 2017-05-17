using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public interface ICommand<in TArgs>
    {
        Outcome<Unit> SendCommand(TArgs args);
    }
    public interface IAsyncCommand<in TArgs>
    {
        Task<Outcome<Unit>> SendCommand(TArgs args);
    }
}
