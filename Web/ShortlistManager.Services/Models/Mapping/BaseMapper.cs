namespace ShortlistManager.Services.Models.Mapping
{
    public abstract class BaseMapper<TSrc, TDst> : IMapper<TSrc, TDst>
        where TDst : new()
    {
        public TDst Create(TSrc src)
        {
            var dst = new TDst();
            Update(dst, src);
            return dst;
        }

        public abstract void Update(TDst dest, TSrc src);
    }
}