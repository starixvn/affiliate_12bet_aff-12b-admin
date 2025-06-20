import { format } from "../../../../modules/numberFormat";
import { money } from "../../../../modules/constants";

export const FIRST_ITEM_INDEX = 0;
export const INFINITY_MONEY = money.AllowedMaxValue;
export const INFINITY_NUMBER = 30000;
export const DEAL_REVENUE_SHARE = 1;
export const DEAL_CPA = 3;
export const DEAL_HYBRID = 4;

const _subAffRateValueList = () => {
    let min = 0.0,
        max = 20.0;

    const subAffRateList = { 0.05: '5.0%' };

    while (min < max) {
        min = min + 10;
        subAffRateList[format(min / 100, 3)] = format(min, 1) + '%';
    }

    return subAffRateList;
}

export const subAffRateValueList = _subAffRateValueList();

export const defaultSubAffRate = 5.0;

export const mappingFields = {
    MinFdm: 'Min FDM',
    MinTotalDeposit: 'Min Total Deposit',
    MinTO: 'Min Turnover',
    CpaAmount: 'CPA Amount'
}

export const messages = {
    addDealSuccessfully: 'Add Deal Successfully.',
    updateDealSuccessfully: 'Update Deal Successfully.',
    requireGroupName: 'Deal name cannot be empty.',
    requireMinValue: `{0} must be greater than 0.`,
    requireMaxValue: `{0} must be less than ${format(INFINITY_MONEY)}.`,
    requireCountryCodes: 'At least one Target Country must be selected.',
    requireActiveDays: `Activity Period must be between 1 and ${format(INFINITY_NUMBER)}.`,
    requireHoldingTicketsAndHoldingDays: `Holding tickets and Holding days must both be between 1 and ${format(INFINITY_NUMBER)}.`,
    requireQuota: `Quota must be between 1 and ${format(INFINITY_NUMBER)}.`,
    requireTierSetting: 'Missing or invalid Tier Setting. Please contact your system administrator to resolve this issue.',
    requireProductGroup: 'Missing or invalid Product List. Please contact your system administrator to resolve this issue.',
    requireProductGroupHasSelectedProduct: 'Product Group {0} - At least one product must be selected.',
    requireDuplicateProductsInGroups: 'You\'ve selected the same Product in more than one Product Group. Please correct it.',
    requireIncreasing: 'The values in the {0} columns must increase sequentially.',
    requireAllTierSettingsMatchGroupProduct1: 'Product Group {0} has different Min FDM or Min Total Deposit values in some tiers compared to Product Group 1.',
    requireCheckBox: 'Please select at least one condition: Min FDM, Min Total Deposit, or Min Turnover.',
    requireMinCpaAmount: 'CPA Amount cannot be negative.'
}

export const cpaTierSettingDefault = {
    TierId: 1,
    GroupProductId: 1,
    MinFdm: 0,
    MinTotalDeposit: 0,
    MinTO: 0,
    CpaAmount: 1,
}

export const productGroupDefault = {
    ProductIds: '',
    GroupProductId: 1,
}