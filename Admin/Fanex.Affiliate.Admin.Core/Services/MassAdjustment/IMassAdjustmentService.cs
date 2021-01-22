using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.MetaModels.MassAdjustment;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MassAdjustment
{
    public interface IMassAdjustmentService
    {
        AdjustmentResultInfo InsertMassAdjusment(HttpPostedFileBase file, MassAdjustmentParam param);
    }
}
