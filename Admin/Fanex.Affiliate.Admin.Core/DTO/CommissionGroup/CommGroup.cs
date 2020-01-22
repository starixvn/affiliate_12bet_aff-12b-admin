using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class CommGroup
    {
        public int RowIdx { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Creator { get; set; }

        [MaxLength(Validation.MaxLengthCommGroupDescription, ErrorMessage = "Description should shorter than 100 characters!")]
        public string Description { get; set; }

        [Range(0, (double)Money.MaxValue, ErrorMessage = "QmTurnover not valid!")]
        public decimal QmTurnover { get; set; }

        [Range(0, (double)Money.MaxValue, ErrorMessage = "NextQmTurnover not valid!")]
        public decimal NextQmTurnover { get; set; }

        [Range(0, Validation.MaxId, ErrorMessage = "NextQmTurnover not valid!")]
        public int NoAffs { get; set; }

        public string CreatedDateText => CreatedDate.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
