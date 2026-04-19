namespace PROG7311_PART_2.Factories
{
    public interface ICurrencyService
    {
        Task<decimal> GetRate();

    }
}
