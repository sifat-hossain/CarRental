namespace Campoverde.QMS.Interfaces;

public interface IQuoteService
{
    Task<string> CreateQuoteAsync(Quote quote);
}
