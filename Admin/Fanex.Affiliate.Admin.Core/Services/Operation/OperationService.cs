namespace Fanex.Affiliate.Admin.Core.Services.Operation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Affiliate.Admin.Core.Commands.Operation;
    using Fanex.Affiliate.Admin.Core.Criterias.Operation;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Utilities.Operation;
    using Fanex.Data.Repository;

    public class OperationService : BaseDynamicService, IOperationService
    {
        private readonly ISystemService _systemService;

        public OperationService(
            IDynamicRepository dynamicRepository,
            ISystemService systemService) : base(dynamicRepository)
        {
            _systemService = systemService;
        }

        public IEnumerable<DealType> GetDealTypes()
        {
            var data = FetchTData<DealType>("Aff_Admin_CommGroup_GroupTypeSel");
            return data.OrderBy(_ => _.GroupTypeName);
        }

        public EntityResult<DealInfo> GetDeals(DealParam param)
        {
            var criteria = new DealCriteria
            {
                GroupId = param.GroupId,
                GroupName = param.GroupName,
                GroupTypeId = param.GroupTypeId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize
            };

            return FetchEntityResult<DealInfo>(criteria);
        }

        public IEnumerable<DeductionItem> GetDeductionList()
        {
            const string SpKey = "Aff_Admin_CommGroup_Deductions_Sel";

            return FetchTData<DeductionItem>(SpKey);
        }

        public DealSettingDetail GetDealSettingDetail(int groudId)
        {
            var criteria = new DealDetailCriteria
            {
                GroupId = groudId
            };

            var (dealSettingInfoList, revevueTierSettingList, revenueDeductionSettingList,  cpaTierSettingList, cpaGroupProductList)
                = DynamicRepository.FetchMultiple<DealSettingDetail, RevevueTierSetting, RevenueDeductionSetting, CpaTierSetting, CpaGroupProduct>(criteria);

            var dealSettingInfo = dealSettingInfoList.FirstOrDefault();
            if (dealSettingInfo == null)
            {
                return null;
            }

            dealSettingInfo.CPATierSetting = cpaTierSettingList;
            dealSettingInfo.ProductGroup = cpaGroupProductList; 
            dealSettingInfo.RevevueTierSetting = revevueTierSettingList;

            if (dealSettingInfo.DealType != Constants.DealType.CPA && revenueDeductionSettingList != null)
            {
                dealSettingInfo.PlatformFeeSetting = revenueDeductionSettingList.Where(p => 
                    p.DeductionId == Constants.Deduction.PlatformFeeId);
                    
                dealSettingInfo.DepositFeeSetting = revenueDeductionSettingList.FirstOrDefault(p => 
                    p.DeductionId == Constants.Deduction.DepositFeeId);
                    
                dealSettingInfo.WithdrawalFeeSetting = revenueDeductionSettingList.FirstOrDefault(p => 
                    p.DeductionId == Constants.Deduction.WithdrawalFeeId);
            }

            return dealSettingInfo;
        }

        public ResultInfo AddDeal(DealSettingParam param)
        {
            var (isValid, message) = ValidateDeal(param);
            if (!isValid)
            {
                return ResultInfo.GenerateErr(message);
            }

            if (param.DealType == Constants.DealType.CPA)
            {
                return AddCpaDeal(param);
            }

            return ResultInfo.GenerateErr("Only support add CPA Deal");
        }

        public ResultInfo UpdateDeal(DealSettingParam param)
        {
            var (isValid, message) = ValidateDeal(param);
            if (!isValid)
            {
                return ResultInfo.GenerateErr(message);
            }

            if (param.DealType == Constants.DealType.CPA)
            {
                return UpdateCpaDeal(param);
            }

            return ResultInfo.GenerateErr("Only support update CPA Deal");
        }

        public ResultInfo DeleteDeal(DeleteDealParam param)
        {
            var command = new DeleteDealCommand
            {
                GroupId = param.DealId,
                UserId = param.UserId,
                Ip = param.Ip
            };

            return ExecuteReturnResultInfo(command);
        }

        private (bool isValid, string message) ValidateDeal(DealSettingParam dealInfo)
        {
            var messageList = new List<string>();
            if (dealInfo == null)
            {
                return (false, "Deal information cannot be null.");
            }
            var validateResult = DealInfoValidator.ValidateGroupName(dealInfo.DealName);
            if (!validateResult.isValid)
            {
                messageList.AddRange(validateResult.messageList);
            }

            if (dealInfo.DealType == Constants.DealType.CPA)
            {
                validateResult = DealInfoValidator.ValidateCPAInfo(dealInfo);
            }
            else if (dealInfo.DealType == Constants.DealType.RevenueShare)
            {
                validateResult = DealInfoValidator.ValidateRevenueShare(dealInfo, _systemService.GetProductList());
            }
            else
            {
                validateResult = DealInfoValidator.ValidateHybirdDeal(dealInfo, _systemService.GetProductList());
            }

            if (!validateResult.isValid)
            {
                messageList.AddRange(validateResult.messageList);
            }

            var isValid = messageList.Count == 0;
            var message = isValid ? string.Empty : string.Join("</br>", messageList);

            return (isValid, message);
        }

        private ResultInfo AddCpaDeal(DealSettingParam param)
        {
            var command = new InsertDealCommand();
            AssignDeal(command, param);

            return ExecuteReturnResultInfo(command);
        }

        private ResultInfo UpdateCpaDeal(DealSettingParam param)
        {
            var command = new UpdateDealCommand();
            AssignDeal(command, param);

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignDeal(BaseDealCommand command, DealSettingParam param)
        {
            command.GroupId = param.GroupId;
            command.GroupTypeId = param.DealType;
            command.GroupName = param.DealName;
            command.SubCommRate = param.SubCommRate;
            command.Description = param.Description;
            command.CountryCodes = param.CountryCodes;
            command.ActiveDays = param.ActiveDays;
            command.HoldingDays = param.HoldingDays;
            command.HoldingTickets = param.HoldingTickets;
            command.Quota = param.Quota;
            command.IsInstant = param.IsInstant;
            command.CpaTierList = BuildCpaTierListXml(param.CPATierSetting, param.CheckedMinFdm, param.CheckedMinTotalDeposit, param.CheckedMinTurnover);
            command.CpaProductList = BuildCpaProductListXml(param.ProductGroup);
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }

        private static string BuildCpaTierListXml(IEnumerable<CpaTierSetting> cpaTierSettingParams, bool checkedMinFdm, bool checkedMinTotalDeposit, bool checkedMinTurnover)
        {
            var result = new StringBuilder("<root>");

            foreach (var item in cpaTierSettingParams)
            {
                result.AppendFormat("<row groupproductid=\"{0}\" tierid=\"{1}\" {2} {3} {4} cpaamount=\"{5}\" />",
                    item.GroupProductId, 
                    item.TierId, 
                    checkedMinFdm ? $"minfdm=\"{item.MinFdm}\"": "", 
                    checkedMinTotalDeposit ? $"mintotaldeposit=\"{item.MinTotalDeposit}\"" : "", 
                    checkedMinTurnover ? $"minto=\"{item.MinTO}\"" : "", 
                    item.CpaAmount);
            }

            result.Append("</root>");

            return result.ToString();
        }

        private static string BuildCpaProductListXml(IEnumerable<CpaGroupProduct> cpaProductGroups)
        {
            var result = new StringBuilder("<root>");
            foreach (var item in cpaProductGroups)
            {
                result.AppendFormat(" <row groupproductid=\"{0}\" productids=\"{1}\" />", item.GroupProductId, item.ProductIds);
            }
            result.Append("</root>");
            return result.ToString();
        }
    }
}
