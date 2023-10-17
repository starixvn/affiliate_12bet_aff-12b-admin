using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class FdmSetting
    {
        public int GroupId { get; set; }

        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "The Fdm Id is not exists!")]
        public int FdmId { get; set; }

        [Range(0, Validation.InfinityQualifiedMember, ErrorMessage = "The Min New Fdm is not valid!")]
        public int MinNewFdm { get; set; }

        [Range(0, Validation.InfinityQualifiedMember, ErrorMessage = "The Max New Fdm is not valid!")]
        public int MaxNewFdm { get; set; }

        [Range(0, (double)Validation.InfinityAmount, ErrorMessage = "The Min Fdm's Deposit is not valid!")]
        public decimal MinFdmDeposit { get; set; }

        [Range(0, (double)Validation.InfinityAmount, ErrorMessage = "The Max Fdm's Deposit is not valid!")]
        public decimal MaxFdmDeposit { get; set; }

        public decimal Commission { get; set; }

        [Range(0, (double)Validation.InfinityAmount, ErrorMessage = "The Max Extra Comm is not valid!")]
        public decimal MaxExtraComm { get; set; }
    }
}
