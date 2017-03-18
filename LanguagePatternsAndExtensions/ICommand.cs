using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public interface ICommand<in TArgs>
    {
        Task<Outcome<Unit>> SendCommand(TArgs args);
    }
}
