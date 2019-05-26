using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public interface IQuery<in TArgs, out TResult>
    {
        TResult SendQuery(TArgs args);
    }
    public interface IAsyncQuery<in TArgs, TResult>
    {
        Task<TResult> SendQuery(TArgs args);
    }
    public interface IOutcomeQuery<in TArgs, TResult>
    {
        Outcome<TResult> SendQuery(TArgs args);
    }
    public interface IAsyncOutcomeQuery<in TArgs, TResult>
    {
        Task<Outcome<TResult>> SendQuery(TArgs args);
    }
    public interface IOptionQuery<in TArgs, TResult>
    {
        Option<TResult> SendQuery(TArgs args);
    }
    public interface IAsyncOptionQuery<in TArgs, TResult>
    {
        Task<Option<TResult>> SendQuery(TArgs args);
    }
}
