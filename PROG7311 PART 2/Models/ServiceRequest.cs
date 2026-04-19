namespace PROG7311_PART_2.Models
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }

        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        public string Description { get; set; }

        public decimal CostUSD { get; set; }
        public decimal CostZAR { get; set; }

        public RequestStatus Status { get; set; }
    }
}
