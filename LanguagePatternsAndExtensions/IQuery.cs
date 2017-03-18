using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public interface IQuery<in TArgs, TResult>
    {
        Task<Outcome<TResult>> SendQuery(TArgs args);
    }
}
