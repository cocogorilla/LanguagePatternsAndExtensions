using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public interface IQuery<in TArgs, TResult>
    {
        Outcome<TResult> SendQuery(TArgs args);
    }
    public interface IAsyncQuery<in TArgs, TResult>
    {
        Task<Outcome<TResult>> SendQuery(TArgs args);
    }
}
