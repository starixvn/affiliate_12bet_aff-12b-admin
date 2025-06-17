import { format } from "../../../../modules/numberFormat";
import { money } from "../../../../modules/constants";

export const FIRST_ITEM_INDEX = 0;
export const INFINITY_MONEY = money.AllowedMaxValue;
export const INFINITY_NUMBER = 30000;

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
    addCPASuccessfully: 'Add Deal Successfully.',
    requireGroupName: 'Deal name cannot be empty. Please check!',
    requireMinMaxValue: `{0} must be greater than 0.`,
    requireCountryCodes: 'At least 1 Target Country is selected.',
    requireActiveDays: `Activity Period must be between 1 and ${format(INFINITY_NUMBER)}.`,
    requireHoldingTicketsAndHoldingDays: `Holding tickets and Holding days must both be between 1 and ${format(INFINITY_NUMBER)}.`,
    requireQuota: `Quota must be between 1 and ${format(INFINITY_NUMBER)}.`,
    requireTierSetting: 'Missing or invalid Tier Setting. Please contact your system administrator to resolve this issue.',
    requireProductGroup: 'Missing or invalid Product List. Please contact your system administrator to resolve this issue.',
    requireProductGroupHasSelectedProduct: 'Product Group {0} - Must choose at least 1 product',
    requireDuplicateProductsInGroups: 'You\'ve selected the same Product in more than one Product Group. Please correct it.',
    requireIncreasing: 'The values in the {0} columns must increase sequentially.',
    requireAllTierSettingsMatchGroupProduct1: 'Product Group {0} has different Min FDM or Min Total Deposit values in some tiers compared to Product Group 1. Please check and update it.',
    requireCheckBox: 'Please select at least one condition: Min FDM, Min Total Deposit, or Min Turnover.'
}