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

export const propsGreaterThanThreshold = {
    MinFdm: 0,
    MinTotalDeposit: 0,
    MinWagering: 0,
    ActiveDays: 1,
    CpaAmount: 1
}

export const messages = {
    requireGroupName: 'Group Name cannot be empty. Please check!',
    requireMinFdm: `Min FDM cannot be less than 0 or greater than ${format(INFINITY_MONEY)}. Please check!`,
    requireMinTotalDeposit: `Min Total Deposit cannot be less than 0 or greater than ${format(INFINITY_MONEY)}. Please check!`,
    requireMinWagering: `Min Wagering cannot be less than 0 or greater than ${format(INFINITY_MONEY)}. Please check!`,
    requireCpaAmount: `Cpa Amount cannot be less than 1 or greater than ${format(INFINITY_MONEY)}. Please check!`,
    requireActiveDays: `Active Days cannot be less than 1 or greater than ${format(INFINITY_NUMBER)}. Please check!`,
    requireHoldingTickets: `Holding Tickets cannot be less than 1 or greater than ${format(INFINITY_NUMBER)}. Please check!`,
    requireHoldingDays: `Holding Days cannot be less than 1 or greater than ${format(INFINITY_NUMBER)}. Please check!`,
    requireQuota: `Quota cannot be less than 1 or greater than ${format(INFINITY_NUMBER)}. Please check!`,
    requireTierSetting: 'Tier Setting must be at least 1. Please check!',
    requireProductGroup: 'Product Group must be at least 1. Please check!',
    requireProductGroupHasSelectedProduct: 'Product Group {0} must have at least 1 selected product. Please check!',
    requireDuplicateProductsInGroups: 'Duplicate products found in groups. Please check!',
    requireIncreasingMinFdm: 'Min FDM must have increasing values across tiers. Please check!',
    requireIncreasingMinTotalDeposit: 'Min Total Deposit must have increasing values across tiers. Please check!',
    requireIncreasingMinWagering: 'Min Wagering must have increasing values across tiers. Please check!',
    requireIncreasingCpaAmount: 'Cpa Amount must have increasing values across tiers. Please check!',
    requireAllTierSettingsMatchGroupProduct1: 'Group Product {0} has different Min FDM or Min Total Deposit values in some tiers compared to Group Product 1. Please check and update it.'
}