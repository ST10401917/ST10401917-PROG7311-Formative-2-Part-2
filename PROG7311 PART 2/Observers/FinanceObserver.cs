using PROG7311_PART_2.Models;

namespace PROG7311_PART_2.Observers
{
    public class FinanceObserver : IContractObserver
    {
        public void Update(Contract contract)
        {
            // Example: finance reacts to contract status change
            Console.WriteLine($"Finance notified: Contract {contract.ContractId} is now {contract.Status}");
        }
    }
}
