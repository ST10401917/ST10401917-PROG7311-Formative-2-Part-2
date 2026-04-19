using PROG7311_PART_2.Models;
using PROG7311_PART_2.Observers;

namespace PROG7311_PART_2.Service
{
    public class ContractNotificationService
    {
        private readonly ContractSubject _subject;

        public ContractNotificationService()
        {
            _subject = new ContractSubject();

            // Attach observers
            _subject.Attach(new FinanceObserver());
            _subject.Attach(new ClientObserver());
        }

        public void NotifyContractChange(Contract contract)
        {
            _subject.Notify(contract);
        }
    }
}
