import { globalResource } from '../../../../modules/globalResource';
import { selectedAllProduct, maxWidth, minDate } from '../../../../modules/constants';

export { globalResource, selectedAllProduct, maxWidth, minDate }

export const headerText = {
    Index: globalResource.Index,
    Date: 'Date',
    Type: 'Type',
    Amount: 'Amount',
    Status: 'Status',
    Creator: 'Creator',
    Description: 'Description',
}

export const MONTHLY_TOTAL = 0;
export const MONTHLY_EARNING = 201;
export const PAYMENT_REQUEST = 202;
export const ADJUSTMENT = 203;
export const NEW_FDM_COMM_BONUS = 205;

export const monthlyEarning = 'Monthly Earning'

export const transTypeList = {
    [MONTHLY_EARNING]: 'Monthly Earning',
    [PAYMENT_REQUEST]: 'Payment Request',
    [ADJUSTMENT]: 'Adjustment',
    [NEW_FDM_COMM_BONUS]: 'New FDM',
    [MONTHLY_TOTAL]: 'Transaction'
}