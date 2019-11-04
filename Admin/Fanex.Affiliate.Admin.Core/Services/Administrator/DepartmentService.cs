using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Affiliate.Admin.Core.Repository.Administrator;

namespace Fanex.Affiliate.Admin.Core.Services.Administrator
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentInfo> GetDepartmentList()
        {
            return _departmentRepository.GetDepartmentList();
        }

        public DepartmentInfo GetDepartmentDetail(int selectedDepartmentId)
        {
            return _departmentRepository.GetDepartmentDetail(selectedDepartmentId);
        }

        public ResultInfo InsertDepartment(InsertUpdateDepartmentParam param)
        {
            return _departmentRepository.InsertDepartment(param);
        }

        public ResultInfo UpdateDepartment(InsertUpdateDepartmentParam param)
        {
            return _departmentRepository.UpdateDepartment(param);
        }

        public ResultInfo DeleteDepartment(DeleteDepartmentParam param)
        {
            return _departmentRepository.DeleteDepartment(param);
        }

        public IEnumerable<PolicyInfo> GetRolePolicy(int departmentId)
        {
            return _departmentRepository.GetRolePolicy(departmentId);
        }
    }
}
