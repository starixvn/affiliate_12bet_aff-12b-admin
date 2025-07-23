namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    using System;

    public class PlayerCpaReport
    {
        public int MemberCPAId { get; set; }   

        public int AffId { get; set; }

        public string AffUserName { get; set; }

        public int CustId { get; set; }

        public string CustUserName { get; set; }

        public DateTime? RegisteredDate { get; set; }

        public int GroupId { get; set; }

        public string DealName { get; set; }

        public int ScheduleId { get; set; }

        public string ScheduleName { get; set; }

        public decimal? FTD { get; set; }

        public decimal TotalDeposit { get; set; }

        public bool IsTO { get; set; }

        public int? MemberCpaProductId { get; set; }

        public DateTime AQD { get; set; }

        public decimal CPAAmount { get; set; }

        public int HoldingBetTickets { get; set; }

        public int HoldingTickets { get; set; }

        public DateTime? HQD { get; set; }

        public DateTime? HED { get; set; }

        public decimal? EstimatedCPAAmount { get; set; }

        public decimal? ApprovedCPAAmount { get; set; }

        public string CPAStatus { get; set; }

        public DateTime? CPADate { get; set; }

        public bool IsAbnormal { get; set; }

        public string Description { get; set; }

        public string BetTicket => $"{HoldingBetTickets}/{HoldingTickets}";

    }
}
