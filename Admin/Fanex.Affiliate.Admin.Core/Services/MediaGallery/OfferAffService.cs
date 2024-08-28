using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using System.Text.RegularExpressions;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class OfferAffService : IOfferAffService
    {
        private const int MaxAffiliates = 501;
        private readonly IOfferAffRepository _OfferAffiliateRepository;

        public OfferAffService(IOfferAffRepository OfferAffiliateRepository)
        {
            _OfferAffiliateRepository = OfferAffiliateRepository;
        }

        public ResultInfo DeleteOfferAffs(DeleteOfferAffParam param) => _OfferAffiliateRepository.DeleteOfferAffs(param);

        public EntityResult<OfferAff> GetOfferAffList(SelectOfferAffParam param) => _OfferAffiliateRepository.GetOfferAffList(param);

        public ResultInfo InsertOfferAff(InsertOfferAffParam param)
        {
            var result = ValidateAffNames(param.AffNameList);

            if (!result.IsSuccessful)
            {
                return result;
            }

            return _OfferAffiliateRepository.InsertOfferAff(param);
        }

        private static ResultInfo ValidateAffNames(string affNames)
        {
            var regex = new Regex("[^0-9a-zA-Z,]");
            if (regex.IsMatch(affNames))
            {
                return ResultInfo.GenerateErr("Affiliate(s) must be letters and digits, separated by [Comma]");
            }

            if (affNames.Split(',').Length > MaxAffiliates)
            {
                return ResultInfo.GenerateErr("The function can only create assignment for up to 500 affiliates per request.");
            }

            return ResultInfo.Success;
        }
    }
}
