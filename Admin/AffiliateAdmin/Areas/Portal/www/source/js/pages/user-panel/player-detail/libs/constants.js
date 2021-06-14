import { globalResource } from '../../../../modules/globalResource';
import { maxWidth, selectedAllProduct, minDate } from '../../../../modules/constants';

export { globalResource, maxWidth, selectedAllProduct, minDate };

export const headerText = {
    Index: globalResource.Index,
    UserName: 'User Name',
    IbcUserName: 'IBC User Name',
    RegisteredDate: 'Registered Date',
    Turnover: 'Turnover',
    Winloss: 'Winlost',
    Bonus: 'Bonus',
    Deposit: 'Deposit',
    Withdrawal: 'Withdrawal',
    BankingFee: 'Banking Fee'
}

export const SortByIdList = {
    Username: 1,
    RegisteredDate: 2,
    Deposit: 3,
    Withdrawal: 4,
    Turnover: 5,
    Winloss: 6,
    Bonus: 7,
    BankingFee: 8,
    IbcUsername: 9
};