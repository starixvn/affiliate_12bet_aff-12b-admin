using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Utilities;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class CommissionGroupService : ICommissionGroupService
    {
        private readonly ICommissionGroupRepository _commGroupRepository;

        private readonly IAffiliateService _affiliateService;

        private readonly IEmailService _emailService;

        public CommissionGroupService(
            ICommissionGroupRepository commGroupRepository,
            IAffiliateService affiliateService,
            IEmailService emailService)
        {
            _commGroupRepository = commGroupRepository;
            _affiliateService = affiliateService;
            _emailService = emailService;
        }

        public IEnumerable<CommissionGroupSetting> GetCommissionGroupList(GetCommissionGroupParam param)
        {
            var rawData = _commGroupRepository.GetCommissionGroupList(param);

            var commGroupList = rawData.CommGroupList;
            var tierSettingList = rawData.TierSettingList;
            var deductionSettingList = rawData.DeductionSettingList;

            var result = from c in commGroupList
                         join t in tierSettingList on c.GroupId equals t.GroupId into groupTierSetting
                         join d in deductionSettingList on c.GroupId equals d.GroupId into groupDeduction
                         select new CommissionGroupSetting
                         {
                             RowIdx = c.RowIdx,
                             GroupId = c.GroupId,
                             GroupName = c.GroupName,
                             CreatedDate = c.CreatedDate,
                             Creator = c.Creator,
                             Description = c.Description,
                             QmTurnover = c.QmTurnover,
                             NextQmTurnover = c.NextQmTurnover,
                             IsAdjustment = c.IsAdjustment,
                             NoAffs = c.NoAffs,
                             TierSettingList = groupTierSetting,
                             PlatformFeeList = groupDeduction.Where(p => IsPlatformFee(p.ProductIds)),
                             OperationalFee = groupDeduction.FirstOrDefault(p => p.DeductionName == DeductionName.OperationalFee),
                             BankingFee = groupDeduction.FirstOrDefault(p => p.DeductionName == DeductionName.BankingFee)
                         };

            return result;
        }

        public ResultInfo CreateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            var deductionSettingList = param.PlatformFeeList.Concat(new[] { param.OperationalFee, param.BankingFee });
            param.TierList = BuildTierSettingXml(param.TierSettingList);
            param.DeductionList = BuildDeductionSettingXml(deductionSettingList, true);

            return _commGroupRepository.CreateCommissionGroup(param);
        }

        public ResultInfo UpdateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            var deductionSettingList = param.PlatformFeeList.Concat(new[] { param.OperationalFee, param.BankingFee });
            param.TierList = BuildTierSettingXml(param.TierSettingList);
            param.DeductionList = BuildDeductionSettingXml(deductionSettingList, false);

            return _commGroupRepository.UpdateCommissionGroup(param);
        }

        public ResultInfo DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            return _commGroupRepository.DeleteCommissionGroup(param);
        }

        public IEnumerable<Deduction> GetDeductionList()
        {
            return _commGroupRepository.GetDeductionList();
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
            var isCreateNewPrivateGroup = affInfo.GroupId == 0 || param.GroupId != affInfo.GroupId;

            var commGroupResult = isCreateNewPrivateGroup ? CreateCommissionGroup(param) : UpdateCommissionGroup(param);

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

        private static bool IsPlatformFee(string productIdList)
        {
            return productIdList != "0";
        }

        private static string BuildTierSettingXml(IEnumerable<TierSetting> tierSettingList)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in tierSettingList)
            {
                var row = $"<row id=\"{item.TierId}\" minamount=\"{item.MinAmount}\" maxamount=\"{item.MaxAmount}\" minqm=\"{item.MinQm}\" maxqm=\"{item.MaxQm}\" commission=\"{item.Commission}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }

        private static string BuildDeductionSettingXml(IEnumerable<DeductionSetting> deductionSettingList, bool isInsert)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in deductionSettingList)
            {
                var percent = isInsert ? item.DeductedPercentage : item.NextDeductedPercentage;
                var row = $"<row id=\"{item.DeductionId}\" productids=\"{item.ProductIds}\" percent=\"{percent}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }
    }
}