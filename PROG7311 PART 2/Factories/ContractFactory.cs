namespace PROG7311_PART_2.Factories
{

    public class ContractFactory
    {
        public static IContract GetContract(string type)
        {
            return type switch
            {
                "Premium" => new PremiumContract(),
                _ => new StandardContract()
            };
        }
    }
}
