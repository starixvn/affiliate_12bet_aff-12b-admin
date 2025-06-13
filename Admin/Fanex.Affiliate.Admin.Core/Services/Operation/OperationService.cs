namespace Fanex.Affiliate.Admin.Core.Services.Operation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Affiliate.Admin.Core.Commands.Operation;
    using Fanex.Affiliate.Admin.Core.Criterias.Operation;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
    using Fanex.Data.Repository;

    public class OperationService : BaseDynamicService, IOperationService
    {
        public OperationService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
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

        public ResultInfo AddCpaDeal(DealSettingParam param)
        {
            var command = new InsertCpaDealCommand
            {
                GroupId = param.DealId,
                GroupTypeId = param.DealType,
                GroupName = param.DealName,
                SubCommRate = param.SubCommRate,
                Description = param.Description,
                CountryCodes = param.CountryCodes,
                ActiveDays = param.ActiveDays,
                HoldingDays = param.HoldingDays,
                HoldingTickets = param.HoldingTickets,
                Quota = param.Quota,
                IsInstant = param.IsInstantPayment,
                CpaTierList = BuildCpaTierListXml(param.CPATierSetting, param.CheckedMinFdm, param.CheckedMinTotalDeposit, param.CheckedMinWagering),
                CpaProductList = BuildCpaProductListXml(param.ProductGroup),
                UserId = param.UserId,
                Ip = param.Ip
            };

            return ExecuteReturnResultInfo(command);
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

        private static string BuildCpaTierListXml(IEnumerable<CpaTierSettingParam> cpaTierSettingParams, bool checkedMinFdm, bool checkedMinTotalDeposit, bool checkedMinWagering)
        {
            var result = new StringBuilder("<root>");

            foreach (var item in cpaTierSettingParams)
            {
                result.AppendFormat("<row groupproductid=\"{0}\" tierid=\"{1}\" {2} {3} {4} cpaamount=\"{5}\" />",
                    item.GroupProductId, 
                    item.TierId, 
                    checkedMinFdm ? $"minfdm=\"{item.MinFdm}\"": "", 
                    checkedMinTotalDeposit ? $"mintotaldeposit=\"{item.MinTotalDeposit}\"" : "", 
                    checkedMinWagering ? $"minto=\"{item.MinWagering}\"" : "", 
                    item.CpaAmount);
            }

            result.Append("</root>");

            return result.ToString();
        }

        private static string BuildCpaProductListXml(IEnumerable<CpaProductGroupParam> cpaProductGroups)
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
