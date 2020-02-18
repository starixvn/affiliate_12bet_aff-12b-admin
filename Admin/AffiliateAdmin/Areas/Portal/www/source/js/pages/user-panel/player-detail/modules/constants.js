import { maxWidth } from '../../../../modules/constants';
import { selectedAllProduct } from '../../../../modules/constants';
import { globalResource } from '../../../../modules/globalResource';
import { shortedMonthNames, minMonthDate } from '../../../../modules/constants';

export { globalResource, maxWidth, selectedAllProduct, shortedMonthNames, minMonthDate };

export const headerText = {
    Index: '#',
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

export const resource = {
    All: globalResource.All,
    Search: globalResource.Search,
    Product: globalResource.Product,
    Month: 'Month'
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