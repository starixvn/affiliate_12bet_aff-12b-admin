export const defaultPagingOption = {
    PageIndex: 1,
    PageSize: 20,
    TotalItem: 0,
    PageList: [10, 20, 50, 100, 500]
};

export const minMonthDate = { month: 12, year: 2009 };

export const shortedMonthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

export const money = {
    MaxValue: 922337203685477,
    MinValue: -922337203685477
}

export const selectedAllProduct = 0;

const maxRightPanelWidth = window.innerWidth - 278;
const customerStatisticWidth = 1010;

export const maxWidth = customerStatisticWidth > maxRightPanelWidth ? customerStatisticWidth : maxRightPanelWidth;