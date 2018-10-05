const sortByCommon = {
    Turnover: 2,
    Winloss: 3,
    Earning: 4
}

export const defaultSortBy = 1;
export const sortByMainView = Object.assign({}, sortByCommon, { Affiliate: 1 });
export const sortByMonthOnMonthView = Object.assign({}, sortByCommon, { Month: 1 });

export const allProduct = '0';

export const minMonthRange = { month: 12, year: 2009 };

export const viewLevel = {
    main: 1,
    monthOnMonth: 2
}