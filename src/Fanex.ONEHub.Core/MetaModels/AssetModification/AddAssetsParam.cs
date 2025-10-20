using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AssetModification
{
    public class AddAssetsParam : UserInfoParam
    {
        public string RootFolder { get; set; }

        public string SubPath { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string InputFileNames { get; set; } = string.Empty;

        public List<string> FilePaths { get; } = new List<string>();
    }
}
