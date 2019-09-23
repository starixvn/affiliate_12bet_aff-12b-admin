using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class TextListService : ITextListService
    {
        private readonly ITextListRepository _textListRepository;

        public TextListService(ITextListRepository textListRepositoryy)
        {
            _textListRepository = textListRepositoryy;
        }

        public ResultInfo DeleteTexts(DeleteTextParam param)
        {
            return _textListRepository.DeleteTexts(param);
        }

        public TextInfo GetTextDetail(int selectedTextId)
        {
            return _textListRepository.GetTextDetail(selectedTextId);
        }

        public EntityResult<TextInfo> GetTextList(TextParam param)
        {
            return _textListRepository.GetTextList(param);
        }

        public ResultInfo InsertText(InsertUpdateTextParam param)
        {
            return _textListRepository.InsertText(param);
        }

        public ResultInfo UpdateText(InsertUpdateTextParam param)
        {
            return _textListRepository.UpdateText(param);
        }
    }
}
