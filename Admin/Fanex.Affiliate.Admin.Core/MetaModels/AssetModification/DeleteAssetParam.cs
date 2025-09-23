namespace Fanex.Affiliate.Admin.Core.MetaModels.AssetModification
{
    public class DeleteAssetParam : UserInfoParam
    {
        public string RootFolder { get; set; }

        public string SubPath { get; set; }

        public string FileName { get; set; }
    }
}
