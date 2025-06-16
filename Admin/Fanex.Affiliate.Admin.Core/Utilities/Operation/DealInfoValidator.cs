namespace Fanex.Affiliate.Admin.Core.Utilities.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;

    public static class DealInfoValidator
    {
        private const int INFINITY_NUMBER = 1_000_000_000;
        private const decimal INFINITY_MONEY = Money.AllowedMaxValue;

        private const string MIN_FDM = "MinFdm";
        private const string MIN_TOTAL_DEPOSIT = "MinTotalDeposit";
        private const string MIN_TURNOVER = "MinTO";
        private const string CPA_AMOUNT = "CpaAmount";

        private static readonly IDictionary<string, string> mappingFields = new Dictionary<string, string>
        {
            [MIN_FDM] = "Min FDM",
            [MIN_TOTAL_DEPOSIT] = "Min Total Deposit",
            [MIN_TURNOVER] = "Min Turnover",
            [CPA_AMOUNT] = "Cpa Amount"
        };

        private static readonly IDictionary<string, string> messages = new Dictionary<string, string>
        {
            ["addCPASuccessfully"] = "Add Deal Successfully.",
            ["requireGroupName"] = "Deal name cannot be empty. Please check!",
            ["requireMinMaxValue"] = "{0} must be greater than 0.",
            ["requireCountryCodes"] = "At least 1 Target Country is selected.",
            ["requireActiveDays"] = "Activity Period must be greater than 0.",
            ["requireHoldingTicketsAndHoldingDays"] = "Holding Tickets and Holding Days must both be greater than 0.",
            ["requireQuota"] = "Quota must be greater than 0.",
            ["requireTierSetting"] = "Tier Setting must be at least 1.",
            ["requireProductGroup"] = "Product Group must be at least 1.",
            ["requireProductGroupHasSelectedProduct"] = "Product Group {0} - Must choose at least 1 product",
            ["requireDuplicateProductsInGroups"] = "You've selected the same Product in more than one Product Group. Please correct it.",
            ["requireIncreasing"] = "The values in the {0} columns must increase sequentially.",
            ["requireAllTierSettingsMatchGroupProduct1"] = "Group Product {0} has different Min FDM or Min Total Deposit values in some tiers compared to Group Product 1. Please check and update it.",
            ["requireCheckBox"] = "Please select at least one condition: Min FDM, Min Total Deposit, or Min Turnover."
        };


        public static (bool isValid, string message) Validate(DealSettingParam dealInfo)
        {
            var messageList = new List<string>();
            if (dealInfo == null)
            {
                return (false, "Deal information cannot be null.");
            }
            var groupNameValidation = ValidateGroupName(dealInfo.DealName);
            if (!groupNameValidation.isValid)
            {
                messageList.AddRange(groupNameValidation.messageList);
            }
            var generalDealInfoValidation = ValidateGeneralDealInfo(dealInfo);
            if (!generalDealInfoValidation.isValid)
            {
                messageList.AddRange(generalDealInfoValidation.messageList);
            }
            var checkBoxValidation = ValidateCheckBox(dealInfo.CheckedMinFdm, dealInfo.CheckedMinTotalDeposit, dealInfo.CheckedMinTurnover);
            if (!checkBoxValidation.isValid)
            {
                messageList.AddRange(checkBoxValidation.messageList);
            }
            var productGroupValidation = ValidateProductGroup(dealInfo.ProductGroup, dealInfo.CheckedMinTurnover);
            if (!productGroupValidation.isValid)
            {
                messageList.AddRange(productGroupValidation.messageList);
            }
            var tierSettingValidation = ValidateTierSetting(dealInfo.CPATierSetting, dealInfo.CheckedMinFdm, dealInfo.CheckedMinTotalDeposit, dealInfo.CheckedMinTurnover);
            if (!tierSettingValidation.isValid)
            {
                messageList.AddRange(tierSettingValidation.messageList);
            }
            var isValid = messageList.Count == 0;
            var message = isValid ? string.Empty : string.Join("</br>", messageList);

            return (isValid, message);
        }

        public static (bool isValid, List<string> messageList) ValidateGroupName(string groupName)
        {
            var messageList = new List<string>();

            if (string.IsNullOrWhiteSpace(groupName))
            {
                messageList.Add(messages["requireGroupName"]);
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        public static (bool isValid, List<string> messageList) ValidateGeneralDealInfo(DealSettingParam dealInfo)
        {
            var messageList = new List<string>();

            void AddIfInvalid(bool condition, string message)
            {
                if (condition)
                {
                    messageList.Add(message);
                }
            }

            AddIfInvalid(string.IsNullOrWhiteSpace(dealInfo.DealName), messages["requireCountryCodes"]);

            AddIfInvalid(dealInfo.ActiveDays < 1 || dealInfo.ActiveDays > INFINITY_NUMBER, messages["requireActiveDays"]);

            if (dealInfo.IsActivePlayerHoldingPeriod)
            {
                AddIfInvalid((dealInfo.HoldingTickets < 1 || dealInfo.HoldingTickets > INFINITY_NUMBER)
                    || (dealInfo.HoldingDays < 1 || dealInfo.HoldingDays > INFINITY_NUMBER),
                    messages["requireHoldingTicketsAndHoldingDays"]);
            }

            if (dealInfo.IsQuota)
            {
                AddIfInvalid(dealInfo.Quota < 1 || dealInfo.Quota > INFINITY_NUMBER, messages["requireQuota"]);
            }

            bool isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        public static (bool isValid, List<string> messageList) ValidateCheckBox(bool checkedMinFdm, bool checkedMinTotalDeposit, bool checkedMinTurnover)
        {
            var messageList = new List<string>();

            if (!checkedMinFdm && !checkedMinTotalDeposit && !checkedMinTurnover)
            {
                messageList.Add(messages["requireCheckBox"]);
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        public static (bool isValid, List<string> messageList) ValidateProductGroup(IEnumerable<CpaGroupProduct> productGroup, bool checkedMinTurnover)
        {
            var messageList = new List<string>();

            if (!checkedMinTurnover)
            {
                return (true, messageList);
            }

            if (EnsureProductGroupAtLeastOne(productGroup))
            {
                messageList.Add(messages["requireProductGroup"]);
                return (false, messageList);
            }

            var selectedProductValidation = EnsureProductGroupHasSelectedProduct(productGroup);
            if (!selectedProductValidation.isValid)
            {
                messageList.AddRange(selectedProductValidation.messageList);
            }

            var duplicateValidation = CheckDuplicateProductsInGroups(productGroup);
            if (!duplicateValidation.isValid)
            {
                messageList.AddRange(duplicateValidation.messageList);
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        private static bool EnsureProductGroupAtLeastOne(IEnumerable<CpaGroupProduct> productGroup)
        {
            return productGroup == null || !productGroup.Any();
        }

        private static (bool isValid, List<string> messageList) EnsureProductGroupHasSelectedProduct(IEnumerable<CpaGroupProduct> productGroup)
        {
            var messageList = new List<string>();

            var products = productGroup.ToList();

            for (int i = 0; i < products.Count; i++)
            {
                var item = products[i];
                if (string.IsNullOrWhiteSpace(item.ProductIds))
                {
                    messageList.Add(string.Format(messages["requireProductGroupHasSelectedProduct"], i + 1));
                }
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        private static (bool isValid, List<string> messageList) CheckDuplicateProductsInGroups(IEnumerable<CpaGroupProduct> productGroups)
        {
            var seen = new HashSet<string>();
            var messageList = new List<string>();

            var products = productGroups.ToList();
            for (int i = 0; i < products.Count; i++)
            {
                var group = products[i];
                var productIds = group.ProductIds?.Split(',') ?? Array.Empty<string>();

                foreach (var id in productIds)
                {
                    if (seen.Contains(id))
                    {
                        messageList.Add($"Product Group {i + 1}: {messages["requireDuplicateProductsInGroups"]}");
                        break;
                    }
                    seen.Add(id);
                }
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        public static (bool isValid, List<string> messageList) ValidateTierSetting(
            IEnumerable<CpaTierSetting> tierSetting,
            bool checkedMinFdm,
            bool checkedMinTotalDeposit,
            bool checkedMinTurnover)
        {
            var messageList = new List<string>();

            if (EnsureTierSettingAtLeastOne(tierSetting))
            {
                messageList.Add(messages["requireTierSetting"]);
                return (isValid: false, messageList);
            }

            var groupedTierSetting = tierSetting
                .GroupBy(x => x.GroupProductId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var groupedValues = groupedTierSetting.Select(kvp => kvp.Value).ToList();

            for (int index = 0; index < groupedValues.Count; index++)
            {
                var groupTiers = groupedValues[index];

                var increasingValidation = ValidateIncreasingTiers(index, groupTiers, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover);
                if (!increasingValidation.isValid)
                {
                    messageList.AddRange(increasingValidation.messageList);
                }
            }

            var matchValidation = ValidateAllTierSettingsMatchGroupProduct1(groupedTierSetting, checkedMinFdm, checkedMinTotalDeposit);
            if (!matchValidation.isValid)
            {
                messageList.AddRange(matchValidation.messageList);
            }

            var valueValidation = ValidateValueFieldOfTierSetting(groupedTierSetting, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover);
            if (!valueValidation.isValid)
            {
                messageList.AddRange(valueValidation.messageList);
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        private static bool EnsureTierSettingAtLeastOne(IEnumerable<CpaTierSetting> tierSetting)
        {
            return tierSetting == null || !tierSetting.Any();
        }

        private static (bool isValid, List<string> messageList) ValidateIncreasingTiers(
            int index,
            List<CpaTierSetting> groupTiers,
            bool checkedMinFdm,
            bool checkedMinTotalDeposit,
            bool checkedMinTurnover)
        {
            var isValid = true;
            var messageList = new List<string>();
            var groupProduct = $"Product Group {index + 1}";
            var fields = new List<string>();

            var validations = new List<(string Field, bool ShouldCheck)>
            {
                (MIN_FDM, checkedMinFdm && index == 0),
                (MIN_TOTAL_DEPOSIT, checkedMinTotalDeposit && index == 0),
                (MIN_TURNOVER, checkedMinTurnover)
            };

            foreach (var (field, shouldCheck) in validations)
            {
                if (shouldCheck)
                {
                    var result = ValidateIncreasingByField(groupTiers, field);
                    if (!result.isValid)
                    {
                        isValid = false;
                        fields.Add(mappingFields[result.field]);
                    }
                }
            }

            if (!isValid)
            {
                messageList.Add($"{groupProduct}: {string.Format(messages["requireIncreasing"], string.Join(", ", fields))}");
            }

            return (isValid, messageList);
        }

        private static (bool isValid, string field) ValidateIncreasingByField(List<CpaTierSetting> tiers, string field)
        {
            for (int i = 1; i < tiers.Count; i++)
            {
                var prev = GetValue(tiers[i - 1], field);
                var curr = GetValue(tiers[i], field);

                if (curr <= prev)
                {
                    return (false, field);
                }
            }

            return (true, string.Empty);
        }

        private static decimal GetValue(CpaTierSetting tier, string fieldName)
        {
            switch (fieldName)
            {
                case MIN_FDM:
                    return tier.MinFdm;
                case MIN_TOTAL_DEPOSIT:
                    return tier.MinTotalDeposit;
                case MIN_TURNOVER:
                    return tier.MinTO;
                case CPA_AMOUNT:
                    return tier.CpaAmount;
                default:
                    throw new ArgumentException("Invalid field", nameof(fieldName));
            }
        }

        private static (bool isValid, List<string> messageList) ValidateAllTierSettingsMatchGroupProduct1(
            Dictionary<int, List<CpaTierSetting>> grouped,
            bool checkFdm, bool checkDeposit)
        {
            var messageList = new List<string>();

            var firstGroup = grouped.First();
            int index = 1;

            foreach (var kvp in grouped)
            {
                if (kvp.Key != firstGroup.Key
                    && !IsGroup2MatchGroup1(kvp.Value, firstGroup.Value, checkFdm, checkDeposit))
                {
                    messageList.Add(messages["requireAllTierSettingsMatchGroupProduct1"].Replace("{0}", index.ToString()));
                }
                index++;
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        private static bool IsGroup2MatchGroup1(List<CpaTierSetting> group2, List<CpaTierSetting> group1, bool checkFdm, bool checkDeposit)
        {
            return group2.TrueForAll(t1 => group1.Exists(t2 =>
                (!checkFdm || t1.MinFdm == t2.MinFdm)
                && (!checkDeposit || t1.MinTotalDeposit == t2.MinTotalDeposit)));
        }

        private static (bool isValid, List<string> messageList) ValidateValueFieldOfTierSetting(
            Dictionary<int, List<CpaTierSetting>> groupedTierSetting,
            bool checkedMinFdm,
            bool checkedMinTotalDeposit,
            bool checkedMinTurnover)
        {
            var messageList = new List<string>();
            int index = 0;

            foreach (var kvp in groupedTierSetting)
            {
                var groupProductId = kvp.Key;
                var groupTiers = kvp.Value;

                var result = ValidateValueField(index, groupProductId, groupTiers, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover);

                if (!result.isValid)
                {
                    messageList.AddRange(result.messageList);
                }

                index++;
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

#pragma warning disable S1541 // Methods and properties should not be too complex
        private static (bool isValid, List<string> messageList) ValidateValueField(
#pragma warning restore S1541 // Methods and properties should not be too complex
            int index,
            int groupProductId,
            List<CpaTierSetting> groupTiers,
            bool checkedMinFdm,
            bool checkedMinTotalDeposit,
            bool checkedMinTurnover)
        {
            var isValid = true;
            var messageList = new List<string>();
            var fields = new List<string>();

            for (int tierIndex = 0; tierIndex < groupTiers.Count; tierIndex++)
            {
                var tier = groupTiers[tierIndex];

                void ValidateField(bool condition, string field)
                {
                    if (condition)
                    {
                        isValid = false;
                        fields.Add(mappingFields[field]);
                    }
                }

                if (index == 0)
                {
                    ValidateField(checkedMinFdm && (tier.MinFdm < 1 || tier.MinFdm > INFINITY_MONEY), MIN_FDM);
                    ValidateField(checkedMinTotalDeposit && (tier.MinTotalDeposit < 1 || tier.MinTotalDeposit > INFINITY_MONEY), MIN_TOTAL_DEPOSIT);
                }

                ValidateField(checkedMinTurnover && (tier.MinTO < 1 || tier.MinTO > INFINITY_MONEY), MIN_TURNOVER);
                ValidateField(tier.CpaAmount < 1 || tier.CpaAmount > INFINITY_MONEY, CPA_AMOUNT);
            }

            if(!isValid)
            {
                var fieldNames = string.Join(", ", fields);
                messageList.Add($"Product Group {groupProductId}: {string.Format(messages["requireMinMaxValue"], fieldNames)}");
            }

            return (isValid, messageList);
        }
    }
}
