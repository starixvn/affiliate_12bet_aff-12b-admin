import { format } from "../../../../modules/numberFormat";
import { money } from "../../../../modules/constants";

export const FIRST_ITEM_INDEX = 0;
export const INFINITY_MONEY = money.AllowedMaxValue;
export const INFINITY_NUMBER = 1000000000;

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

export const messages = {
    addCPASuccessfully: 'Add Deal Successfully',
    requireGroupName: 'Deal name cannot be empty. Please check!',
    requireMinFdm: `Min FDM must be greater than 0 and less than ${format(INFINITY_MONEY)}.`,
    requireMinTotalDeposit: `Min Total Deposit must be greater than 0 and less than ${format(INFINITY_MONEY)}.`,
    requireMinTurnover: `Min Turnover must be greater than 0 and less than ${format(INFINITY_MONEY)}.`,
    requireCpaAmount: `Cpa Amount must be greater than 0 and less than ${format(INFINITY_MONEY)}.`,
    requireCountryCodes: 'At least 1 Target Country is selected.',
    requireActiveDays: `Activity Period must be greater than 0 and less than ${format(INFINITY_NUMBER)}.`,
    requireHoldingTicketsAndHoldingDays: `Holding Tickets and Holding Days must both be greater than 0 and less than ${format(INFINITY_NUMBER)}.`,
    requireQuota: `Quota must be greater than 0 and less than ${format(INFINITY_NUMBER)}.`,
    requireTierSetting: 'Tier Setting must be at least 1.',
    requireProductGroup: 'Product Group must be at least 1.',
    requireProductGroupHasSelectedProduct: 'Product Group {0} - Must choose at least 1 product',
    requireDuplicateProductsInGroups: 'You\'ve selected the same Product in more than one Product Group. Please correct it.',
    requireIncreasingMinFDM: 'The value in the Min FDM column must increase sequentially',
    requireIncreasingMinTotalDeposit: 'The value in the Min Total Deposit column must increase sequentially',
    requireIncreasingMinTurnover: 'The value in the Min Turnover column must increase sequentially',
    requireAllTierSettingsMatchGroupProduct1: 'Group Product {0} has different Min FDM or Min Total Deposit values in some tiers compared to Group Product 1. Please check and update it.',
    requireCheckBox: 'Please select at least one condition: Min FDM, Min Total Deposit, or Min Turnover.'
}