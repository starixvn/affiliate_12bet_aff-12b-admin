import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import MonthPicker from '@9softstudio/react-monthpicker';
import MyLink from '../../../common/components/MyLink';
import { Table, Spinner } from 'reactstrap';
import { rowTitle, resource } from '../libs/constants';
import { shortedMonthNames, minMonthDate, selectedAllProduct, deviceTypes, pcLaptop, mobile } from '../../../../modules/constants';
import { formatDecimalWithCurrencySymbol, format } from '../../../../modules/numberFormat';

import { redirectToPlayerDetail, redirectToReferrers } from '../../user-panel/actions';
import { setMonthDate, fetchData } from '../actions';
import { claimResource } from '../../../../modules/claim';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data: null,
            isLoading: false
        }
    }

    componentDidMount() {
        this.fetchData(this.props.today);
    }

    componentDidUpdate(prevProps) {
        const { affId, today } = this.props;

        if (prevProps.affId !== affId) {
            this.fetchData(today);
        }
    }

    fetchData = async (monthDate) => {
        this.setState({ isLoading: true });
        this.props.setMonthDate(monthDate);
        const { affId } = this.props;

        const param = {
            monthDate,
            affId
        }
        const data = await this.props.fetchData(param);

        this.setState({ data, isLoading: false });
    }

    handleSelectMonth = (month, year) => {
        const monthDate = `${month}/1/${year}`;
        this.fetchData(monthDate);
    }

    handleFormatMonthPicker = (month, year) => {
        return `${shortedMonthNames[month - 1]}-${year}`
    }

    _renderHeader(monthDate) {
        return (
            <thead>
                <tr>
                    <th>{rowTitle.Statistics}</th>
                    <th className="width-30 month-picker">
                        <div className="position-relative d-inline-block">
                            <MonthPicker minMonth={minMonthDate.month} minYear={minMonthDate.year}
                                selectedDropdownYear={monthDate.getFullYear()}
                                selectedMonth={monthDate.getMonth() + 1}
                                onSelect={this.handleSelectMonth}
                                onFormat={this.handleFormatMonthPicker}
                                iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                        </div>
                        <Spinner className={this.state.isLoading ? "" : "d-none"} size="sm" color="secondary" />
                    </th>
                    <th className="width-30">{rowTitle.UpToNow}</th>
                </tr>
            </thead>
        )
                                }

    _renderData(monthDate, data) {
        data = data || {};
        const { isDisplayTurnOver, isDisplayActivePlayer, today } = this.props;
        const DepositByMonth = formatDecimalWithCurrencySymbol(data.DepositByMonth);
        const DepositTotal = formatDecimalWithCurrencySymbol(data.DepositTotal);
        const TurnoverByMonth = isDisplayTurnOver && formatDecimalWithCurrencySymbol(data.TurnoverByMonth);
        const TurnoverTotal = isDisplayTurnOver && formatDecimalWithCurrencySymbol(data.TurnoverTotal);
        const WinlostByMonth = formatDecimalWithCurrencySymbol(data.WinlostByMonth);
        const WinlostTotal = formatDecimalWithCurrencySymbol(data.WinlostTotal);

        const startMonthString = `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`;

        const date = new Date(today);
        const endMonth = new Date(monthDate.getFullYear(), monthDate.getMonth() + 1, 0);
        const isCurrentMonth = date.getFullYear() === endMonth.getFullYear() && date.getMonth() === endMonth.getMonth();
        const endMonthString = `${endMonth.getMonth() + 1}/${isCurrentMonth ? date.getDate() : endMonth.getDate()}/${endMonth.getFullYear()}`;

        return (
            <tbody>
                {
                    this._buildRow(rowTitle.NewSignups,
                        `${format(data.NewSignUpByMonth)}/${format(data.NewFdmByMonth)}`,
                        `${format(data.NewSignUpTotal)}/${format(data.NewFdmTotal)}`)
                }
                {
                    isDisplayActivePlayer && this._buildRow(rowTitle.TotalActive,
                        (
                            data.ActivePlayerByMonth > 0 ? (
                                <MyLink onClick={() => this.props.redirectToPlayerDetail({ productId: selectedAllProduct, monthDate: endMonthString, isClickLink: true })}>
                                    {format(data.ActivePlayerByMonth)}
                                </MyLink>
                            ) : format(data.ActivePlayerByMonth)
                        ),
                        resource.NA)
                }
                { isDisplayActivePlayer && this._buildRow(rowTitle.TotalQualified, format(data.QualifiedMemberByMonth), resource.NA)}
                { isDisplayActivePlayer && this._buildRow(rowTitle.TotalDeposits, DepositByMonth, DepositTotal)}
                { isDisplayTurnOver && this._buildRow(rowTitle.Turnover, TurnoverByMonth, TurnoverTotal)}
                {this._buildRow(rowTitle.Winlost, WinlostByMonth, WinlostTotal)}
                {
                    this._buildRow(rowTitle.TrackingRatePC,
                        (
                            <MyLink onClick={() => this.props.redirectToReferrers({ fromDate: startMonthString, toDate: endMonthString, deviceType: deviceTypes[pcLaptop], isClickLink: true })}>
                                {format(data.UniqueClickPcLaptopByMonth)}/{format(data.TotalClickPcLaptopByMonth)}
                            </MyLink>
                        ),
                        resource.NA)
                }
                {
                    this._buildRow(rowTitle.TrackingRateMobile,
                        (
                            <MyLink onClick={() => this.props.redirectToReferrers({ fromDate: startMonthString, toDate: endMonthString, deviceType: deviceTypes[mobile], isClickLink: true })}>
                                {format(data.UniqueClickMobileByMonth)}/{format(data.TotalClickMobileByMonth)}
                            </MyLink>
                         ),
                        resource.NA)
                }
            </tbody>
        );
    }

    _buildRow(title, dataByMonth, dataUpToNow) {
        return (
            <tr key={title}>
                <td className="font-weight-bold">{title}</td>
                <td className="text-right">{dataByMonth}</td>
                <td className="text-right">{dataUpToNow}</td>
            </tr>
        )
    }

    render() {
        const { data } = this.state;
        const monthDate = new Date(this.props.monthDate);

        return (
            <div className="my-2 table-responsive">
                <Table id="statistic-data-table">
                    {this._renderHeader(monthDate)}
                    {this._renderData(monthDate, data)}
                </Table>
            </div>
        );
    }
}

function mapStateToProps({ affCommonInfo, today, customerStatistics, claimsPrincipal }) {
    const { affId } = affCommonInfo;
    const isDisplayTurnOver = claimsPrincipal.hasReadClaim(claimResource.TurnOver);
    const isDisplayActivePlayer = claimsPrincipal.hasReadClaim(claimResource.AffPlayerDetail)
    return Object.assign({}, { monthDate: customerStatistics.monthDate, today, affId, isDisplayTurnOver, isDisplayActivePlayer });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setMonthDate, redirectToPlayerDetail, redirectToReferrers, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);