using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface ITextListRepository
    {
        EntityResult<TextInfo> GetTextList(TextParam param);

        TextInfo GetTextDetail(int selectedTextId);

        ResultInfo InsertText(InsertUpdateTextParam param);

        ResultInfo UpdateText(InsertUpdateTextParam param);

        ResultInfo DeleteTexts(DeleteTextParam param);
    }
}
