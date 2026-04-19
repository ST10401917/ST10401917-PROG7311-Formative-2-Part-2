using PROG7311_PART_2.Models;

namespace PROG7311_PART_2.Observers
{
    public class ContractSubject
    {
        private readonly List<IContractObserver> _observers = new();

        // Attach observer
        public void Attach(IContractObserver observer)
        {
            _observers.Add(observer);
        }

        // Detach observer (optional but good practice)
        public void Detach(IContractObserver observer)
        {
            _observers.Remove(observer);
        }

        // Notify all observers
        public void Notify(Contract contract)
        {
            foreach (var observer in _observers)
            {
                observer.Update(contract);
            }
        }
    }
}
