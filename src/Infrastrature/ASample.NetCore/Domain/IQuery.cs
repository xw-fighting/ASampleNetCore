
namespace ASample.NetCore.Domain
{
    //Marker
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}
