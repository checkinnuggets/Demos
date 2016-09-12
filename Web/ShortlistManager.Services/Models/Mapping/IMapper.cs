namespace ShortlistManager.Services.Models.Mapping
{
    public interface IMapper<in TSrc, TDst>
        where TDst : new()
    {
        TDst Create(TSrc src);
        void Update(TDst dest, TSrc src);
    }
}
