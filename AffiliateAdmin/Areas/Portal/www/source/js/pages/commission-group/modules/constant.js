import { formatDecimal } from "../../../modules/numberFormat";

export const INFINITY_AM = 100000000000000;
export const INFINITY_QM = 1000000000;
export const INFINITY_SYMBOL = '∞';

export const defaultPagingOption = {
    PageIndex: 1,
    PageSize: 20,
    TotalItem: 0,
    PageList: [10, 20, 50, 100, 500]
};

function generateCommissionValues() {
    let min = 0,
        max = 1;
    const commissionList = ['0.00'];

    while (min < max) {
        min = min + 0.01;
        commissionList.push(formatDecimal(min));
    }

    return commissionList;
}
const _commValueList = generateCommissionValues();

export const commissionValueList = _commValueList;

export const messages = {
    requireGroupName: 'Group Name cannot be empty. Please check!',
    requireTier: 'Tier Setting must have at least 1 tier',
    requireInfinity: 'Last Tier Setting must have maximum value=∞',
    requireQmTurnover: 'QM Turnover value is required',
    amountConstrain: 'Max Amount must be greater than or equal to Min Amount',
    qmConstrain: 'Max QM must be greater than or equal to Min QM'
}

export const deductionNameList = {
    platformFee: 'Platform Fee',
    operationalFee: 'Operational Fee',
    bankingFee: 'Banking Fee'
}