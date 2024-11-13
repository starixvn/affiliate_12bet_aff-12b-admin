import { defaultBaseCurrency, minMonthDate } from '../../../../modules/constants';

export { defaultBaseCurrency, minMonthDate };

const sortByCommon = {
    Turnover: 2,
    GGR: 3,
    Earning: 4
}

export const defaultSortBy = 1;
export const sortByMainView = Object.assign({}, {
    Affiliate: 1
}, sortByCommon);
export const sortByMonthOnMonthView = Object.assign({}, {
    Month: 1
}, sortByCommon);

export const viewLevel = {
    main: 1,
    monthOnMonth: 2
}

export const sortDirectionType = {
    ascending: 0,
    descending: 1
}

export const totalText = 'Total (all pages)';

export const resource = {
    reportName: 'AFFILIATE REPORT',
    reportDetailName: 'MONTH ON MONTH',
    space: ' - '
}

export const reportKey = {
    UserName: 'UserName',
    MonthDateString: 'MonthDateString',
    PaymentCurrency: 'PaymentCurrency',
    TotalClick: 'TotalClick',
    UniqueClick: 'UniqueClick',
    Signup: 'Signup',
    Fdm: 'Fdm',
    ActiveMember: 'ActiveMember',
    NoOfSubAff: 'NoOfSubAff',
    TotalDeposit: 'TotalDeposit',
    Deposit: 'Deposit',
    Withdrawal: 'Withdrawal',
    Turnover: 'Turnover',
    Ggr: 'Ggr',
    Bonus: 'Bonus',
    Rebate: 'Rebate',
    PlatformFee: 'PlatformFee',
    DepositFee: 'DepositFee',
    WithdrawalFee: 'WithdrawalFee',
    TotalFee: 'TotalFee',
    CommissionRate: 'CommissionRate',
    Earning: 'Earning',
    EarningBy: 'EarningBy',
    P2pEarning: 'P2pEarning',
    SubAffEarning: 'SubAffEarning',
    TotalEarning: 'TotalEarning',
    AffWithdrawal: 'AffWithdrawal',
    AffAdjustment: 'AffAdjustment',
    BalanceBf: 'BalanceBf',
    CurrentBBF: 'CurrentBBF',
    MemberStatistics: 'MemberStatistics',
    AffiliateStatistics: 'AffiliateStatistics',
    MonthOnMonth: 'MonthOnMonth',
    PlayerGgr: 'PlayerGgr'
}

export const headerText = {
    [reportKey.UserName]: 'Affiliate',
    [reportKey.MonthDateString]: 'Month',
    [reportKey.PaymentCurrency]: 'Linked Account Currency',
    [reportKey.TotalClick]: 'Total Click',
    [reportKey.UniqueClick]: 'Unique Click',
    [reportKey.Signup]: 'Signup',
    [reportKey.Fdm]: 'FDM',
    [reportKey.ActiveMember]: 'Active Player',
    [reportKey.NoOfSubAff]: 'Sub-Affiliate',
    [reportKey.TotalDeposit]: 'Total Deposit',
    [reportKey.Deposit]: 'Deposit',
    [reportKey.Withdrawal]: 'Withdrawal',
    [reportKey.Turnover]: 'Turnover',
    [reportKey.Ggr]: 'GGR',
    [reportKey.Bonus]: 'Bonus',
    [reportKey.Rebate]: 'Rebate',
    [reportKey.PlatformFee]: 'Royalty Fee',
    [reportKey.DepositFee]: 'Deposit Fee',
    [reportKey.WithdrawalFee]: 'Withdrawal Fee',
    [reportKey.TotalFee]: 'Total Fee',
    [reportKey.CommissionRate]: 'Share%',
    [reportKey.Earning]: 'Earning',
    [reportKey.EarningBy]: 'Earning By',
    [reportKey.P2pEarning]: 'P2P Earning',
    [reportKey.SubAffEarning]: 'Sub-Affiliate Earning',
    [reportKey.TotalEarning]: 'Total Earning',
    [reportKey.AffWithdrawal]: 'Withdrawal',
    [reportKey.AffAdjustment]: 'Adjustment',
    [reportKey.BalanceBf]: 'Balance B/F',
    [reportKey.CurrentBBF]: 'Current Balance B/F',
    [reportKey.MemberStatistics]: 'Player Statistics',
    [reportKey.AffiliateStatistics]: 'Affiliate Statistics',
    [reportKey.MonthOnMonth]: 'Month on Month',
    [reportKey.PlayerGgr]: 'Player GGR'
}

export const headerConfig = {
    [reportKey.UserName]: { label: headerText[reportKey.UserName], isShowable: (claimPrincipal, isMainView) => isMainView },
    [reportKey.MonthDateString]: { label: headerText[reportKey.MonthDateString], isShowable: (claimPrincipal, isMainView) => !isMainView },
    [reportKey.PaymentCurrency]: { label: headerText[reportKey.PaymentCurrency], isShowable: (claimPrincipal, isMainView) => isMainView },
    [reportKey.TotalClick]: { label: headerText[reportKey.TotalClick] },
    [reportKey.UniqueClick]: { label: headerText[reportKey.UniqueClick] },
    [reportKey.Signup]: { label: headerText[reportKey.Signup] },
    [reportKey.Fdm]: { label: headerText[reportKey.Fdm] },
    [reportKey.ActiveMember]: { label: headerText[reportKey.ActiveMember] },
    [reportKey.NoOfSubAff]: { label: headerText[reportKey.NoOfSubAff] },
    [reportKey.TotalDeposit]: { label: headerText[reportKey.TotalDeposit] },
    [reportKey.MemberStatistics]: {
        label: headerText[reportKey.MemberStatistics],
        cols: {
            [reportKey.Deposit]: { label: headerText[reportKey.Deposit] },
            [reportKey.Withdrawal]: { label: headerText[reportKey.Withdrawal] },
            [reportKey.Turnover]: { label: headerText[reportKey.Turnover] },
            [reportKey.Ggr]: { label: headerText[reportKey.Ggr] },
            [reportKey.Bonus]: { label: headerText[reportKey.Bonus] },
            [reportKey.Rebate]: { label: headerText[reportKey.Rebate] },
            [reportKey.PlatformFee]: { label: headerText[reportKey.PlatformFee], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.DepositFee]: { label: headerText[reportKey.DepositFee], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.WithdrawalFee]: { label: headerText[reportKey.WithdrawalFee], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.TotalFee]: { label: headerText[reportKey.TotalFee], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker }
        }
    },
    [reportKey.AffiliateStatistics]: {
        label: headerText[reportKey.AffiliateStatistics],
        cols: {
            [reportKey.CommissionRate]: { label: headerText[reportKey.CommissionRate], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.Earning]: { label: headerText[reportKey.Earning], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.EarningBy]: { label: headerText[reportKey.EarningBy], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isMainView },
            [reportKey.P2pEarning]: { label: headerText[reportKey.P2pEarning], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.SubAffEarning]: { label: headerText[reportKey.SubAffEarning], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.TotalEarning]: { label: headerText[reportKey.TotalEarning], isShowable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.AffWithdrawal]: { label: headerText[reportKey.AffWithdrawal] },
            [reportKey.AffAdjustment]: { label: headerText[reportKey.AffAdjustment], isShowable: (claimPrincipal, isMainView) => isMainView },
            [reportKey.BalanceBf]: { label: headerText[reportKey.BalanceBf] },
            [reportKey.CurrentBBF]: { label: headerText[reportKey.CurrentBBF], isShowable: (claimPrincipal, isMainView) => isMainView }
        }
    }
}