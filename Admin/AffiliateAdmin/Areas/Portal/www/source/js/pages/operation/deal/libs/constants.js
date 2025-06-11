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
    CpaAmount: 1,
    HoldingTickets: 1,
    HoldingDays: 1,
    Quota: 1,
}

export const messages = {
    requireGroupName: 'Group Name cannot be empty. Please check!',
}