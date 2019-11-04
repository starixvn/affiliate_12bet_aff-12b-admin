
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Administrator
{
    public class InsertUpdateDepartmentParam: UserInfoParam
    {
        public int DepartmentId { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Please input department name!")]
        public string DepartmentName { get; set; }

        public string Description { get; set; }
    }
}
