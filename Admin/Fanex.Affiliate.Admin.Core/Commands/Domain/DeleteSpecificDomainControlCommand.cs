using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class DeleteSpecificDomainControlCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_SpecificDomainControl_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int SpecificDomainControlId { get; set; }
    }
}
