import { globalResource } from '../../../../modules/globalResource';
import { selectedAllProduct, maxWidth } from '../../../../modules/constants';

export { globalResource, selectedAllProduct, maxWidth }

export const headerText = {
    Index: '#',
    Date: 'Date',
    Type: 'Type',
    Amount: 'Amount',
    Status: 'Status',
    Creator: 'Creator',
    Description: 'Description',
}

export const MONTHLY_WINLOST = 201;
export const PAYMENT_REQUEST = 202;
export const ADJUSTMENT = 203;
export const TRANSACTION = 0;

export const transTypeList = {
    [MONTHLY_WINLOST]: 'Monthly Winlost',
    [PAYMENT_REQUEST]: 'Payment Request',
    [ADJUSTMENT]: 'Adjustment',
    [TRANSACTION]: 'Transaction'
}

export const monthlyWinlost = 'Monthly Winlost'