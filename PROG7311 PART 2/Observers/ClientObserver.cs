using PROG7311_PART_2.Models;

namespace PROG7311_PART_2.Observers
{
    public class ClientObserver : IContractObserver
    {
        public void Update(Contract contract)
        {
            // Example: client notification logic
            Console.WriteLine($"Client notified: Your contract is now {contract.Status}");
        }
    }
}
