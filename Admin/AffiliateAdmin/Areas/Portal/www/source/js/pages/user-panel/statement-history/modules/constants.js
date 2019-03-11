import { globalResource } from '../../../../modules/globalResource';

export { globalResource }

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

export const transTypeList = {
    [MONTHLY_WINLOST]: 'Monthly Winlost',
    [PAYMENT_REQUEST]: 'Payment Request',
    [ADJUSTMENT]: 'Adjustment'
}

export const transaction = 'Transaction'