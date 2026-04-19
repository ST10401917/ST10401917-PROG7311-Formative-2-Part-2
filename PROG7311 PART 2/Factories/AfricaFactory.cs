
namespace PROG7311_PART_2.Factories
{
    public class AfricaFactory : IRegionalFactory
    {
        public ICurrencyService CreateCurrencyService()
        {
            return new ZARCurrencyService();
        }
    }
}
