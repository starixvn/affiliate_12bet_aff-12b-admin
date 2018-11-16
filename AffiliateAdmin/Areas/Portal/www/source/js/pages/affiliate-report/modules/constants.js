const sortByCommon = {
    Turnover: 2,
    Winloss: 3,
    Earning: 4
}

export const defaultSortBy = 1;
export const sortByMainView = Object.assign({}, {
    Affiliate: 1
}, sortByCommon);
export const sortByMonthOnMonthView = Object.assign({}, {
    Month: 1
}, sortByCommon);

export const allProduct = '0';

export const viewLevel = {
    main: 1,
    monthOnMonth: 2
}

export const sortDirectionType = {
    ascending: 0,
    descending: 1
}

export const totalText = 'Total (all pages)';

export const headerText = {
    UserName: 'Affiliate',
    MonthDateString: 'Month',
    TotalClick: 'Total Click',
    UniqueClick: 'Unique Click',
    Signup: 'Signup',
    Fdm: 'FDM',
    ActiveMember: 'Active Member',
    QualifiedMember: 'Qualified Member',
    Deposit: 'Deposit',
    Withdrawal: 'Withdrawal',
    Turnover: 'Turnover',
    Winlost: 'Winloss',
    Bonus: 'Bonus',
    PlatformFee: 'Platform Fee',
    OperationalFee: 'Operational Fee',
    BankingFee: 'Banking Fees',
    TotalFee: 'Total Fee',
    CommissionRate: 'Comm%',
    Earning: 'Earning',
    AffWithdrawal: 'Withdrawal',
    AffAdjustment: 'Adjustment',
    BalanceBf: 'Balance B/F',
    MemberStatistics: 'Member Statistics',
    AffiliateStatistics: 'Affiliate Statistics',
    MonthOnMonth: 'Month on Month'
}