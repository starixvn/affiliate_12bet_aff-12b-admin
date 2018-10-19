import React, { Component } from 'react';

import Loader from '../../../../components/Loader';
import MonthPicker from '@9softstudio/react-monthpicker';
import { Table } from 'reactstrap';
import { rowTitle, resource } from '../modules/constants';
import { shortedMonthNames, minMonthDate } from '../../../../modules/constants';
import { post } from '../../../../modules/ajaxRequest';
import { resolveClientUrl } from '../../../../modules/utils';
import { formatDecimalWithCurrencySymbol, format } from '../../../../modules/numberFormat';

const API_URL = "Portal/CustomerStatistics/GetCustomerStatistics";
export default class Content extends Component {
    constructor(props) {
        super(props);
        const { today } = this.props;

        this.state = {
            monthDate: new Date(today),
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
                                <a href={`${resolveClientUrl('Portal/UserPanel/PlayerDetail/Index?isFromStatistics=true')}&monthDate=${startMonthString}`} target="fContent">
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
                        (<a href={resolveClientUrl(`UserInfo/MediaStatistics.aspx?month=${startMonthString}`)} target="fContent">
                            {format(data.UniqueClickByMonth)}/{format(data.TotalClickByMonth)}
                        </a>),
                        resource.NA)
                }
                {
                    this._buildRow(rowTitle.TrackingRatePC,
                        (<a href={resolveClientUrl(`UserInfo/MediaStatistics_New.aspx?fdate=${startMonthString}&tdate=${endMonthString}&slDevice=0`)} target="fContent">
                            {format(data.UniqueClickPcLaptopByMonth)}/{format(data.TotalClickPcLaptopByMonth)}
                        </a>),
                        resource.NA)
                }
                {
                    this._buildRow(rowTitle.TrackingRateMobile,
                        (<a href={resolveClientUrl(`UserInfo/MediaStatistics_New.aspx?fdate=${startMonthString}&tdate=${endMonthString}&slDevice=1`)} target="fContent">
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