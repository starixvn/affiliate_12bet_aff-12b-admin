using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.Utilities.CommGroup;

namespace Fanex.Affiliate.Admin.Core.Services.CommGroup
{
    public class CommGroupAssignmentService : ICommGroupAssignmentService
    {
        private readonly ICommissionGroupRepository _commGroupRepository;

        private readonly IAffiliateService _affiliateService;

        private readonly IEmailService _emailService;

        private readonly ICommGroupCommonService _commGroupCommonService;

        public CommGroupAssignmentService(
            ICommissionGroupRepository commGroupRepository,
            IAffiliateService affiliateService,
            IEmailService emailService,
            ICommGroupCommonService commGroupCommonService)
        {
            _commGroupRepository = commGroupRepository;
            _affiliateService = affiliateService;
            _emailService = emailService;
            _commGroupCommonService = commGroupCommonService;
        }

        public IEnumerable<SelectItem> GetNormalCommGroupList(GetGroupListParam param)
        {
            return _commGroupRepository.GetNormalCommGroupList(param);
        }

        public ResultInfo AssignGroup(AssignGroupParam param, AffiliateBasicInfo affInfo, AffInformationEmail emailParam)
        {
            return ProcessAssignGroup(param, affInfo, emailParam);
        }

        public ResultInfo AssignPrivateGroup(CreateUpdateCommissionGroupParam param, AffiliateBasicInfo affInfo, AffInformationEmail emailParam)
        {
            var result = CommGroupValidationHelper.ValidateTierSettingList(param.TierSettingList);

            if (result.IsSuccessful)
            {
                var isCreateNewPrivateGroup = affInfo.GroupId == 0 || param.GroupId != affInfo.GroupId;

                var commGroupResult = isCreateNewPrivateGroup ? _commGroupCommonService.CreateCommissionGroup(param) :
                                                                _commGroupCommonService.UpdateCommissionGroup(param);

                if (commGroupResult.IsSuccessful)
                {
                    var assignparam = new AssignGroupParam
                    {
                        UserId = param.UserId,
                        AffId = affInfo.AffId,
                        GroupId = param.GroupId,
                        Ip = param.Ip
                    };

                    return ProcessAssignGroup(assignparam, affInfo, emailParam);
                }

                return commGroupResult;
            }

            return result;
        }

        private ResultInfo ProcessAssignGroup(AssignGroupParam param, AffiliateBasicInfo affInfo, AffInformationEmail emailParam)
        {
            var assignResult = _commGroupRepository.AssignGroup(param);
            var isApproved = affInfo.GroupId != 0;
            if (!isApproved && assignResult.IsSuccessful)
            {
                return ProcessApproveAffiliate(param, affInfo, emailParam);
            }

            return assignResult;
        }

        private ResultInfo ProcessApproveAffiliate(AssignGroupParam param, AffiliateBasicInfo affInfo, AffInformationEmail emailParam)
        {
            var password = Generator.GetRandomString(10);
            var approvedResult = ApproveAffiliate(param.UserId, param.AffId, param.Ip, password);

            if (approvedResult.IsSuccessful)
            {
                SendEmailApprove(emailParam, affInfo, password);
            }

            return approvedResult;
        }

        private ResultInfo ApproveAffiliate(int userId, int affId, string ip, string password)
        {
            var approveParam = new ApproveAffiliateParam
            {
                UserId = userId,
                AffId = affId,
                Password = CryptographyHelper.Encrypt(password),
                Ip = ip
            };

            return _affiliateService.ApproveAffiliate(approveParam);
        }

        private void SendEmailApprove(AffInformationEmail emailParam, AffiliateBasicInfo affInfo, string password)
        {
            emailParam.UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(affInfo.AffType);
            emailParam.Language = affInfo.PreferredLanguage ?? LanguageSystem.DefaultLanguage;
            emailParam.FirstName = affInfo.FirstName;
            emailParam.UserName = affInfo.UserName;
            emailParam.MailTo = affInfo.Email;
            emailParam.IsHtml = !affInfo.MailOption;
            emailParam.Password = password;

            _emailService.SendApprovedEmail(emailParam);
        }
    }
}