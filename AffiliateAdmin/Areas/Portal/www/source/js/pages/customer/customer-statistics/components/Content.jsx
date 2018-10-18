import React, { Component } from 'react';

import Loader from '../../../../components/Loader';
import MonthPicker from '@9softstudio/react-monthpicker';
import { Table } from 'reactstrap';
import { rowTitle } from '../modules/constants';
import { shortedMonthNames } from '../../../../modules/constants';
import { post } from '../../../../modules/ajaxRequest';
import { resolveClientUrl } from '../../../../modules/utils';
import { formatDecimalWithCurrencySymbol, format } from '../../../../modules/numberFormat';

const API_URL = "Portal/CustomerStatistics/GetCustomerStatistics";
export default class Content extends Component {
    constructor(props) {
        super(props);
        const { monthDate } = this.props;

        this.state = {
            monthDate: monthDate,
            data: null,
            isShowLoader: false
        }
    }

    componentDidMount() {
        const { monthDate } = this.state;
        this.fetchData(monthDate);
    }

    fetchData = async (monthDate) => {
        this.setState({ isShowLoader: true });
        const param = {
            monthDate: `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`
        }
        const response = await post(API_URL, param);
        if (response && response.status === 200) {
            this.setState({
                monthDate: monthDate,
                data: response.data
            });
        }

        this.setState({ isShowLoader: false });
    }

    handleSelectMonth = (month, year) => {
        const monthDate = new Date(year, month - 1);
        this.setState({
            monthDate: monthDate
        });

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
                            <MonthPicker minMonth={12} minYear={2009}
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
                <tr>
                    <td className="font-weight-bold">{rowTitle.NewSignups}</td>
                    <td className="text-right">{format(data.NewSignUpByMonth)}/{format(data.NewFdmByMonth)}</td>
                    <td className="text-right">{format(data.NewSignUpTotal)}/{format(data.NewFdmTotal)}</td>
                </tr>
                <tr>
                    <td className="font-weight-bold">{rowTitle.TotalActive}</td>
                    <td className="text-right">
                        {
                            data.ActivePlayerByMonth > 0 ? (
                                <a href={`${resolveClientUrl('Portal/UserPanel/PlayerDetail/Index?isFromStatistics=true')}&monthDate=${startMonthString}`} target="fContent">
                                    {format(data.ActivePlayerByMonth)}
                                </a>
                            ) : format(data.ActivePlayerByMonth)
                        }
                    </td>
                    <td className="text-right">N/A</td>
                </tr>
                <tr>
                    <td className="font-weight-bold">{rowTitle.TotalQualified}</td>
                    <td className="text-right">{format(data.QualifiedMemberByMonth)}</td>
                    <td className="text-right">N/A</td>
                </tr>
                <tr>
                    <td className="font-weight-bold">{rowTitle.TotalDeposits}</td>
                    <td className="text-right">{DepositByMonth}</td>
                    <td className="text-right">{DepositTotal}</td>
                </tr>
                <tr>
                    <td className="font-weight-bold">{rowTitle.Turnover}</td>
                    <td className="text-right">{TurnoverByMonth}</td>
                    <td className="text-right">{TurnoverTotal}</td>
                </tr>
                <tr>
                    <td className="font-weight-bold">{rowTitle.Winlost}</td>
                    <td className="text-right">{WinlostByMonth}</td>
                    <td className="text-right">{WinlostTotal}</td>
                </tr>
                <tr>
                    <td className="font-weight-bold">{rowTitle.TrackingRate}</td>
                    <td className="text-right">
                        <a href={resolveClientUrl(`UserInfo/MediaStatistics.aspx?month=${startMonthString}`)} target="fContent">
                            {format(data.UniqueClickByMonth)}/{format(data.TotalClickByMonth)}
                        </a>
                    </td>
                    <td className="text-right">N/A</td>
                </tr>
                <tr>
                    <td className="font-weight-bold">{rowTitle.TrackingRatePC}</td>
                    <td className="text-right">
                        <a href={resolveClientUrl(`UserInfo/MediaStatistics_New.aspx?fdate=${startMonthString}&tdate=${endMonthString}&slDevice=0`)} target="fContent">
                            {format(data.UniqueClickPcLaptopByMonth)}/{format(data.TotalClickPcLaptopByMonth)}
                        </a>
                    </td>
                    <td className="text-right">N/A</td>
                </tr>
                <tr>
                    <td className="font-weight-bold">{rowTitle.TrackingRateMobile}</td>
                    <td className="text-right">
                        <a href={resolveClientUrl(`UserInfo/MediaStatistics_New.aspx?fdate=${startMonthString}&tdate=${endMonthString}&slDevice=1`)} target="fContent">
                            {format(data.UniqueClickMobileByMonth)}/{format(data.TotalClickMobileByMonth)}
                        </a>
                    </td>
                    <td className="text-right">N/A</td>
                </tr>
            </tbody>
        );
    }

    render() {
        const { monthDate, data, isShowLoader } = this.state;
        return data && (
            <div className="my-2">
                <Loader show={isShowLoader} />
                <Table id="statistic-data-table">
                    {this._renderHeader(monthDate)}
                    {this._renderData(monthDate, data)}
                </Table>
            </div>
        );
    }
}