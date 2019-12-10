using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.ChangeAdminPassword;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.ChangeAdminPassword
{
    public class ChangeAdminPasswordService : BaseRepository, IChangeAdminPasswordService
    {
        public ResultInfo ChangePassword(ChangeAdminPasswordParam param)
        {
            const string SpKey = "Aff_Admin_ChangePassword";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("oldpwd", DbType.String, param.OldPassword);
                objectDb.AddInParameter("NewPwd", DbType.String, param.NewPassword);
                objectDb.AddInParameter("CustId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }
    }
}
