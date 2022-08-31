import { claimResource } from "../../../modules/claim";

export const defaultSortBy = 1;
export const affiliate = 1;
export const month = 1;
export const turnover = 2;
export const earning = 4;
export const sortByCommon = {
    Winloss: 3
}

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

export const reportKey = {
    UserName: 'UserName',
    PreferredLanguage: 'PreferredLanguage',
    MonthDateString: 'MonthDateString',
    TotalClick: 'TotalClick',
    UniqueClick: 'UniqueClick',
    Signup: 'Signup',
    Fdm: 'Fdm',
    ActiveMember: 'ActiveMember',
    QualifiedMember: 'QualifiedMember',
    Deposit: 'Deposit',
    Withdrawal: 'Withdrawal',
    Turnover: 'Turnover',
    Winlost: 'Winlost',
    Bonus: 'Bonus',
    PlatformFee: 'PlatformFee',
    OperationalFee: 'OperationalFee',
    BankingFee: 'BankingFee',
    TotalFee: 'TotalFee',
    CommissionRate: 'CommissionRate',
    PreviousBalanceBf: 'PreviousBalanceBf',
    Earning: 'Earning',
    AffWithdrawal: 'AffWithdrawal',
    AffAdjustment: 'AffAdjustment',
    BalanceBf: 'BalanceBf',
    MemberStatistics: 'MemberStatistics',
    AffiliateStatistics: 'AffiliateStatistics',
    MonthOnMonth: 'MonthOnMonth'
}

export const headerText = {
    [reportKey.UserName]: 'Affiliate',
    [reportKey.PreferredLanguage]: 'Language',
    [reportKey.MonthDateString]: 'Month',
    [reportKey.TotalClick]: 'Total Click',
    [reportKey.UniqueClick]: 'Unique Click',
    [reportKey.Signup]: 'Signup',
    [reportKey.Fdm]: 'FDM',
    [reportKey.ActiveMember]: 'Active Member',
    [reportKey.QualifiedMember]: 'Qualified Member',
    [reportKey.Deposit]: 'Deposit',
    [reportKey.Withdrawal]: 'Withdrawal',
    [reportKey.Turnover]: 'Turnover',
    [reportKey.Winlost]: 'Winloss',
    [reportKey.Bonus]: 'Bonus',
    [reportKey.PlatformFee]: 'Platform Fee',
    [reportKey.OperationalFee]: 'Operational Fee',
    [reportKey.BankingFee]: 'Banking Fees',
    [reportKey.TotalFee]: 'Total Fee',
    [reportKey.CommissionRate]: 'Comm%',
    [reportKey.PreviousBalanceBf]: "Previous Month's Balance B/F",
    [reportKey.Earning]: 'Earning',
    [reportKey.AffWithdrawal]: 'Withdrawal',
    [reportKey.AffAdjustment]: 'Adjustment',
    [reportKey.BalanceBf]: 'Balance B/F',
    [reportKey.MemberStatistics]: 'Member Statistics',
    [reportKey.AffiliateStatistics]: 'Affiliate Statistics',
    [reportKey.MonthOnMonth]: 'Month on Month'
}

export const headerConfig = {
    [reportKey.UserName]: { label: headerText[reportKey.UserName], isVisable: (claimPrincipal, isMainView) => isMainView },
    [reportKey.MonthDateString]: { label: headerText[reportKey.MonthDateString], isVisable: (claimPrincipal, isMainView) => !isMainView },
    [reportKey.PreferredLanguage]: { label: headerText[reportKey.PreferredLanguage], isVisable: (claimPrincipal, isMainView) => claimPrincipal.hasReadClaim(claimResource.LanguageAmendment) && isMainView },
    [reportKey.TotalClick]: { label: headerText[reportKey.TotalClick] },
    [reportKey.UniqueClick]: { label: headerText[reportKey.UniqueClick] },
    [reportKey.Signup]: { label: headerText[reportKey.Signup] },
    [reportKey.Fdm]: { label: headerText[reportKey.Fdm] },
    [reportKey.ActiveMember]: { label: headerText[reportKey.ActiveMember], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => claimPrincipal.hasReadClaim(claimResource.AffPlayerDetail) && !isCheckedDatePicker },
    [reportKey.QualifiedMember]: { label: headerText[reportKey.QualifiedMember], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => claimPrincipal.hasReadClaim(claimResource.AffPlayerDetail) && !isCheckedDatePicker },
    [reportKey.MemberStatistics]: {
        label: headerText[reportKey.MemberStatistics],
        cols: {
            [reportKey.Deposit]: { label: headerText[reportKey.Deposit] },
            [reportKey.Withdrawal]: { label: headerText[reportKey.Withdrawal] },
            [reportKey.Turnover]: { label: headerText[reportKey.Turnover], isVisable: (claimPrincipal) => claimPrincipal.hasReadClaim(claimResource.TurnOver) },
            [reportKey.Winlost]: { label: headerText[reportKey.Winlost] },
            [reportKey.Bonus]: { label: headerText[reportKey.Bonus] },
            [reportKey.PlatformFee]: { label: headerText[reportKey.PlatformFee], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.OperationalFee]: { label: headerText[reportKey.OperationalFee], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.BankingFee]: { label: headerText[reportKey.BankingFee], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.TotalFee]: { label: headerText[reportKey.TotalFee], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
        }
    },
    [reportKey.AffiliateStatistics]: {
        label: headerText.AffiliateStatistics,
        cols: {
            [reportKey.CommissionRate]: { label: headerText[reportKey.CommissionRate], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.PreviousBalanceBf]: { label: headerText[reportKey.PreviousBalanceBf], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.Earning]: { label: headerText[reportKey.Earning], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker },
            [reportKey.AffWithdrawal]: { label: headerText[reportKey.AffWithdrawal] },
            [reportKey.AffAdjustment]: { label: headerText[reportKey.AffAdjustment], isVisable: (claimPrincipal, isMainView) => claimPrincipal.hasReadClaim(claimResource.AffAdjustment) && isMainView },
            [reportKey.BalanceBf]: { label: headerText[reportKey.BalanceBf], isVisable: (claimPrincipal, isMainView, isCheckedDatePicker) => !isCheckedDatePicker }
        }
    }
}
