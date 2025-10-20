
namespace Fanex.Affiliate.Admin.Core.Commands.Affiliate
{
    public class ToggleFavoriteCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_Affiliate_Favorite_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public bool IsFavorite { get; set; }
    }
}
