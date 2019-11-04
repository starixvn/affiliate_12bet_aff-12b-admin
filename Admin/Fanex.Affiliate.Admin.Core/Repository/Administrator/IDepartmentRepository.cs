using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Administrator
{
    public interface IDepartmentRepository
    {
        IEnumerable<DepartmentInfo> GetDepartmentList();

        DepartmentInfo GetDepartmentDetail(int selectedDepartmentId);

        ResultInfo InsertDepartment(InsertUpdateDepartmentParam param);

        ResultInfo UpdateDepartment(InsertUpdateDepartmentParam param);

        ResultInfo DeleteDepartment(DeleteDepartmentParam param);

        IEnumerable<PolicyInfo> GetRolePolicy(int departmentId);
    }
}
