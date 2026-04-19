namespace PROG7311_PART_2.Service
{
    public interface ICurrencyService
    {
        Task<decimal> GetUsdToZarRate();

    }
}
