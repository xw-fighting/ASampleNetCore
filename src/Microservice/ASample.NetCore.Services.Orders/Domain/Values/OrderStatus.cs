namespace ASample.NetCore.Services.Orders.Domain.Values
{
    public enum OrderStatus : byte
    {
        Created = 0,
        Approved = 1,
        Completed = 2,
        Canceled = 3,
        Revoked = 4
    }
}
