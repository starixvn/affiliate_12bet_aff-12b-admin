import { formatDecimal } from "../../../modules/numberFormat";
import { globalResource } from '../../../modules/globalResource';

export { globalResource };

export const INFINITY_AM = 100000000000000;
export const INFINITY_QM = 1000000000;
export const INFINITY_SYMBOL = '∞';

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

const _validationMessage = {
    requireGroupName: 'Group Name cannot be empty. Please check!',
    requireTier: 'Tier Setting must have at least 1 tier',
    requireInfinity: 'Last Tier Setting must have maximum value=∞',
    requireQmTurnover: 'QM Turnover value is required',
    amountConstrain: 'Max Amount must be greater than or equal to Min Amount',
    qmConstrain: 'Max QM must be greater than or equal to Min QM',
    requireSelectedProduct: 'At least 1 product must be selected per group',
    confirmChangeGroup: 'Are you sure you want to change the commission group for affiliate: {0}',
    choseAnotherGroup: 'Please choose another group (not old group)!',
    assignGroupSuccessfully: 'Assign affiliate to commission group successfully',
    addGroupSuccessfully: 'Add Commission Group Successfully',
    updateGroupSuccessfully: 'Update Commission Group Successfully'
}
export const messages = _validationMessage;

const _deductionNameList = {
    platformFee: 'Platform Fee',
    operationalFee: 'Operational Fee',
    bankingFee: 'Banking Fee',
    exchangeWinLossShare: 'Exchange Win Loss Share',
    exchangeCommissionShare: 'Exchange Commission Share'
}
export const deductionNameList = _deductionNameList;