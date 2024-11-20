import { SingleHeaderColumn, AggregateHeaderColumn, HeaderRow } from './headerConfigs';
import { reportKey, headerText } from './constants';

const aggregateHeaderChildrenOfMemberStatistics = [
    new SingleHeaderColumn(reportKey.Deposit, headerText[reportKey.Deposit], true),
    new SingleHeaderColumn(reportKey.Withdrawal, headerText[reportKey.Withdrawal], true),
    new SingleHeaderColumn(reportKey.Turnover, headerText[reportKey.Turnover], true),
    new SingleHeaderColumn(reportKey.Ggr, headerText[reportKey.Ggr], true),
    new SingleHeaderColumn(reportKey.Bonus, headerText[reportKey.Bonus], true),
    new SingleHeaderColumn(reportKey.Rebate, headerText[reportKey.Rebate], true),
    new SingleHeaderColumn(reportKey.PlatformFee, headerText[reportKey.PlatformFee], true),
    new SingleHeaderColumn(reportKey.DepositFee, headerText[reportKey.DepositFee], true),
    new SingleHeaderColumn(reportKey.WithdrawalFee, headerText[reportKey.WithdrawalFee], true),
    new SingleHeaderColumn(reportKey.TotalFee, headerText[reportKey.TotalFee], true)
];

const aggregateHeaderChildrenOfAffiliateStatistics = [
    new SingleHeaderColumn(reportKey.CommissionRate, headerText[reportKey.CommissionRate], true),
    new SingleHeaderColumn(reportKey.Earning, headerText[reportKey.Earning], true),
    new SingleHeaderColumn(reportKey.EarningBy, headerText[reportKey.EarningBy], true),
    new SingleHeaderColumn(reportKey.P2pEarning, headerText[reportKey.P2pEarning], true),
    new SingleHeaderColumn(reportKey.SubAffEarning, headerText[reportKey.SubAffEarning], true),
    new SingleHeaderColumn(reportKey.TotalEarning, headerText[reportKey.TotalEarning], true),
    new SingleHeaderColumn(reportKey.AffWithdrawal, headerText[reportKey.AffWithdrawal], true),
    new SingleHeaderColumn(reportKey.AffAdjustment, headerText[reportKey.AffAdjustment], true),
    new SingleHeaderColumn(reportKey.BalanceBf, headerText[reportKey.BalanceBf], true),
    new SingleHeaderColumn(reportKey.CurrentBBF, headerText[reportKey.CurrentBBF], true)
];

const cols = [
    new SingleHeaderColumn(reportKey.PaymentCurrency, headerText[reportKey.PaymentCurrency], true),
    new SingleHeaderColumn(reportKey.TotalClick, headerText[reportKey.TotalClick], true),
    new SingleHeaderColumn(reportKey.UniqueClick, headerText[reportKey.UniqueClick], true),
    new SingleHeaderColumn(reportKey.Signup, headerText[reportKey.Signup], true),
    new SingleHeaderColumn(reportKey.Fdm, headerText[reportKey.Fdm], true),
    new SingleHeaderColumn(reportKey.ActiveMember, headerText[reportKey.ActiveMember], true),
    new SingleHeaderColumn(reportKey.NoOfSubAff, headerText[reportKey.NoOfSubAff], true),
    new SingleHeaderColumn(reportKey.TotalDeposit, headerText[reportKey.TotalDeposit], true),
    new AggregateHeaderColumn(reportKey.MemberStatistics, headerText[reportKey.MemberStatistics], aggregateHeaderChildrenOfMemberStatistics),
    new AggregateHeaderColumn(reportKey.AffiliateStatistics, headerText[reportKey.AffiliateStatistics], aggregateHeaderChildrenOfAffiliateStatistics),
]

export const headerFake = new HeaderRow(cols);