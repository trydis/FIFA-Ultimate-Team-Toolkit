namespace UltimateTeam.Toolkit.Models.Store
{
    public class LoansInfo
    {
        public string? LoanId { get; set; }
        public string? LoanType { get; set; }
        public int? LoanValue { get; set; }
        public decimal? Amount { get; set; }
        public decimal? InterestRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }
        public List<RepaymentSchedule>? RepaymentSchedules { get; set; }
    }
}
