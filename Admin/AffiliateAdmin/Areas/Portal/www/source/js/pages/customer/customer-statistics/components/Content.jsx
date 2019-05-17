import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import MonthPicker from '@9softstudio/react-monthpicker';
import { Table } from 'reactstrap';
import { rowTitle, resource } from '../modules/constants';
import { shortedMonthNames, minMonthDate } from '../../../../modules/constants';
import { post } from '../../../../modules/ajaxRequest';
import { formatDecimalWithCurrencySymbol, format } from '../../../../modules/numberFormat';

import { toggleLoader } from '../../../../actions/loaderAction';
import { redirectToPlayerDetail, redirectToTrackingStatistics, redirectToReferrers } from '../../user-panel/actions';
import { deviceTypes } from '../../user-panel/modules/constants';
import { setMonthDate } from '../actions';

const API_URL = "Portal/CustomerStatistics/GetCustomerStatistics";
export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data: null
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
        this.props.setMonthDate(monthDate);
        this.props.toggleLoader(true);
        const { affId } = this.props;

        const param = {
            monthDate,
            affId
        }
        const response = await post(API_URL, param);
        if (response && response.status === 200) {
            this.setState({
                data: response.data
            });
        }

        this.props.toggleLoader(false);
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
                    <th style={{ overflow: 'visible' }}>
                        <div className="position-relative">
                            <MonthPicker minMonth={minMonthDate.month} minYear={minMonthDate.year}
                                selectedDropdownYear={monthDate.getFullYear()}
                                selectedMonth={monthDate.getMonth() + 1}
                                onSelect={this.handleSelectMonth}
                                onFormat={this.handleFormatMonthPicker}
                                iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                        </div>
                    </th>
                    <th>{rowTitle.UpToNow}</th>
                </tr>
            </thead>
        )
    }

    _renderData(monthDate, data) {
        const { affId, affName } = this.props;

        const DepositByMonth = formatDecimalWithCurrencySymbol(data.DepositByMonth);
        const DepositTotal = formatDecimalWithCurrencySymbol(data.DepositTotal);
        const TurnoverByMonth = formatDecimalWithCurrencySymbol(data.TurnoverByMonth);
        const TurnoverTotal = formatDecimalWithCurrencySymbol(data.TurnoverTotal);
        const WinlostByMonth = formatDecimalWithCurrencySymbol(data.WinlostByMonth);
        const WinlostTotal = formatDecimalWithCurrencySymbol(data.WinlostTotal);

        const startMonthString = `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`;

        const endMonth = new Date(monthDate.getFullYear(), monthDate.getMonth() + 1, 0);
        const endMonthString = `${endMonth.getMonth() + 1}/${endMonth.getDate()}/${endMonth.getFullYear()}`;

        return (
            <tbody>
                {
                    this._buildRow(rowTitle.NewSignups,
                        `${format(data.NewSignUpByMonth)}/${format(data.NewFdmByMonth)}`,
                        `${format(data.NewSignUpTotal)}/${format(data.NewFdmTotal)}`)
                }
                {
                    this._buildRow(rowTitle.TotalActive,
                        (
                            data.ActivePlayerByMonth > 0 ? (
                                <a href="javascript:;" onClick={() => this.props.redirectToPlayerDetail({ monthDate: startMonthString })}>
                                    {format(data.ActivePlayerByMonth)}
                                </a>) : format(data.ActivePlayerByMonth)
                        ),
                        resource.NA)
                }
                {this._buildRow(rowTitle.TotalQualified, format(data.QualifiedMemberByMonth), resource.NA)}
                {this._buildRow(rowTitle.TotalDeposits, DepositByMonth, DepositTotal)}
                {this._buildRow(rowTitle.Turnover, TurnoverByMonth, TurnoverTotal)}
                {this._buildRow(rowTitle.Winlost, WinlostByMonth, WinlostTotal)}
                {
                    this._buildRow(rowTitle.TrackingRate,
                        (<a href="javascript:;" onClick={() => this.props.redirectToTrackingStatistics()}>
                            {format(data.UniqueClickByMonth)}/{format(data.TotalClickByMonth)}
                        </a>),
                        resource.NA)
                }
                {
                    this._buildRow(rowTitle.TrackingRatePC,
                        (<a href="javascript:;" onClick={() => this.props.redirectToReferrers({ fromDate: startMonthString, toDate: endMonthString, deviceType: deviceTypes['PC/Laptop'] })}>
                            {format(data.UniqueClickPcLaptopByMonth)}/{format(data.TotalClickPcLaptopByMonth)}
                        </a>),
                        resource.NA)
                }
                {
                    this._buildRow(rowTitle.TrackingRateMobile,
                        (<a href="javascript:;" onClick={() => this.props.redirectToReferrers({ fromDate: startMonthString, toDate: endMonthString, deviceType: deviceTypes.Mobile })}>
                            {format(data.UniqueClickMobileByMonth)}/{format(data.TotalClickMobileByMonth)}
                        </a>),
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

        return data && (
            <div className="my-2">
                <Table id="statistic-data-table">
                    {this._renderHeader(monthDate)}
                    {this._renderData(monthDate, data)}
                </Table>
            </div>
        );
    }
}

function mapStateToProps({ affCommonInfo, today, customerStatistics }) {
    const { affId } = affCommonInfo;
    return Object.assign({}, { monthDate: customerStatistics.monthDate, today, affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setMonthDate, toggleLoader, redirectToPlayerDetail, redirectToTrackingStatistics, redirectToReferrers }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);