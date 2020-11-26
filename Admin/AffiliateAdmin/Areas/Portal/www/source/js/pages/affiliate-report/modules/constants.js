import { claimResource } from "../../../modules/claim";

export const defaultSortBy = 1;
export const affiliate = 1;
export const month = 1;
export const turnover = 2;
export const sortByCommon = {
    Winloss: 3,
    Earning: 4
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

export const dataHeader = {
    UserName: { label: headerText.UserName, isVisable: (claimPrincipal, isMainView) => isMainView },
    MonthDateString : { label: headerText.MonthDateString, isVisable: (claimPrincipal, isMainView) => !isMainView},
    TotalClick: { label: headerText.TotalClick},
    UniqueClick: {label: headerText.UniqueClick},
    Signup: {label: headerText.Signup},
    Fdm: {label: headerText.Fdm},
    ActiveMember: { label: headerText.ActiveMember, isVisable: (claimPrincipal) => claimPrincipal.hasReadClaim(claimResource.AffPlayerDetail)},
    QualifiedMember: { label: headerText.QualifiedMember, isVisable: (claimPrincipal) => claimPrincipal.hasReadClaim(claimResource.AffPlayerDetail)},
    MemberStatistics: {
        label: headerText.MemberStatistics,
        cols: {
            Deposit: { label: headerText.Deposit},
            Withdrawal: { label: headerText.Withdrawal},
            Turnover: { label: headerText.Turnover, isVisable: (claimPrincipal) => claimPrincipal.hasReadClaim(claimResource.TurnOver)},
            Winlost: { label: headerText.Winlost},
            Bonus: {label: headerText.Bonus},
            PlatformFee: {label: headerText.PlatformFee},
            OperationalFee: {label: headerText.OperationalFee},
            BankingFee: {label: headerText.BankingFee},
            TotalFee: { label: headerText.TotalFee},
        }
    },
    AffiliateStatistics : {
        label: headerText.AffiliateStatistics,
        cols: {
            CommissionRate: {label: headerText.CommissionRate},
            Earning: {label: headerText.Earning},
            AffWithdrawal: {label: headerText.AffWithdrawal},
            AffAdjustment: {label: headerText.AffAdjustment, isVisable: (claimPrincipal, isMainView) => claimPrincipal.hasReadClaim(claimResource.Adjustment) && isMainView},
            BalanceBf: {label: headerText.BalanceBf}
        }
    }


}
