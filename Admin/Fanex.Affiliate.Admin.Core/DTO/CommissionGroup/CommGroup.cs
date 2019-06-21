using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class CommGroup
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "RowIdx not exists!")]
        public int RowIdx { get; set; }

        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "GroupId not exists!")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Group Name cannot be empty. Please check!")]
        [MaxLength(100)]
        public string GroupName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Creator { get; set; }

        [MaxLength(100, ErrorMessage = "Description should short 100 characters!")]
        public string Description { get; set; }

        [Range(0, (double)Money.MaxValue, ErrorMessage = "QmTurnover not valid!")]
        public decimal QmTurnover { get; set; }

        [Range(0, (double)Money.MaxValue, ErrorMessage = "NextQmTurnover not valid!")]
        public decimal NextQmTurnover { get; set; }

        public int NoAffs { get; set; }

        public string CreatedDateText => CreatedDate.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
