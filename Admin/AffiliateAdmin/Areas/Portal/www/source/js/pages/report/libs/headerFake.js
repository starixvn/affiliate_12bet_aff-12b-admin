import { SingleHeader, AggregateHeader, HeaderRow } from './header';
import { reportKey, headerText } from './constants';

const aggregateHeaderChildrenOfMemberStatistics = [
    new SingleHeader(reportKey.Deposit, headerText[reportKey.Deposit], true),
    new SingleHeader(reportKey.Withdrawal, headerText[reportKey.Withdrawal], true),
    new SingleHeader(reportKey.Turnover, headerText[reportKey.Turnover], true),
    new SingleHeader(reportKey.Winlost, headerText[reportKey.Winlost], true),
    new SingleHeader(reportKey.Bonus, headerText[reportKey.Bonus], true),
    new SingleHeader(reportKey.PlatformFee, headerText[reportKey.PlatformFee], true),
    new SingleHeader(reportKey.OperationalFee, headerText[reportKey.OperationalFee], true),
    new SingleHeader(reportKey.BankingFee, headerText[reportKey.BankingFee], true),
    new SingleHeader(reportKey.TotalFee, headerText[reportKey.TotalFee], true)
];

const aggregateHeaderChildrenOfAffiliateStatistics = [
    new SingleHeader(reportKey.CommissionRate, headerText[reportKey.CommissionRate], true),
    new SingleHeader(reportKey.Earning, headerText[reportKey.Earning], true),
    new SingleHeader(reportKey.AffWithdrawal, headerText[reportKey.AffWithdrawal], true),
    new SingleHeader(reportKey.AffAdjustment, headerText[reportKey.AffAdjustment], true),
    new SingleHeader(reportKey.BalanceBf, headerText[reportKey.BalanceBf], true)
];

const cols = [
    new SingleHeader(reportKey.UserName, headerText[reportKey.UserName], true),
    new SingleHeader(reportKey.MonthDateString, '', false),
    new SingleHeader(reportKey.TotalClick, headerText[reportKey.TotalClick], true),
    new SingleHeader(reportKey.UniqueClick, headerText[reportKey.UniqueClick], true),
    new SingleHeader(reportKey.Signup, headerText[reportKey.Signup], true),
    new SingleHeader(reportKey.Fdm, headerText[reportKey.Fdm], true),
    new SingleHeader(reportKey.ActiveMember, headerText[reportKey.ActiveMember], true),
    new SingleHeader(reportKey.QualifiedMember, headerText[reportKey.QualifiedMember], true),
    new AggregateHeader(reportKey.MemberStatistics, headerText[reportKey.MemberStatistics], aggregateHeaderChildrenOfMemberStatistics),
    new AggregateHeader(reportKey.AffiliateStatistics, headerText[reportKey.AffiliateStatistics], aggregateHeaderChildrenOfAffiliateStatistics),
]

export const headerFake = new HeaderRow(cols);