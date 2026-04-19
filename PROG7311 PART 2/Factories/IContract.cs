using PROG7311_PART_2.Models;

namespace PROG7311_PART_2.Factories
{
    public interface IContract
    {
        Contract Create(int clientId, DateTime startDate, DateTime endDate, ContractStatus status);

    }
}
