import React, { Component } from 'react';

import MonthPicker from '@9softstudio/react-monthpicker';
import { Table, FormGroup, Label } from 'reactstrap';
import { rowTitle } from '../modules/constants';
import { shortedMonthNames } from '../../../../modules/constants';
import { post } from '../../../../modules/ajaxRequest';
import { formatDecimal } from '../../../../modules/numberFormat';

const API_URL = "Portal/CustomerStatistics/GetCustomerStatistics";
export default class Content extends Component {
    constructor(props) {
        super(props);
        const { monthDate } = this.props;

        this.state = {
            monthDate: monthDate,
            data: null
        }
    }

    componentDidMount() {
        const { monthDate } = this.state;
        this.fetchData(monthDate);
    }

    fetchData = async (monthDate) => {
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
    }

    handleSelectMonth = (month, year) => {
        const monthDate = new Date(year, month - 1);
        this.setState ({
            monthDate: monthDate
        });

        this.fetchData(monthDate);
    }

    handleFormatMonthPicker = (month, year) => {
        return `${shortedMonthNames[month - 1]}-${year}`
    }

    _renderData(monthDate, data) {
        const DepositByMonth = formatDecimal(data.DepositByMonth);
        const DepositTotal = formatDecimal (data.DepositTotal);
        const TurnoverByMonth = formatDecimal (data.TurnoverByMonth);
        const TurnoverTotal = formatDecimal(data.TurnoverTotal);
        const WinlostByMonth = formatDecimal (data.WinlostByMonth);
        const WinlostTotal = formatDecimal (data.WinlostTotal);
        return (
            <div className="table-responsive my-2">
                <Table>
                    <thead>
                        <tr>
                            <th>{rowTitle.Statistics}</th>
                            <th>
                                <MonthPicker minMonth={12} minYear={2009}
                                selectedDropdownYear={monthDate.getFullYear()}
                                selectedMonth={monthDate.getMonth() + 1}
                                onSelect={this.handleSelectMonth}
                                onFormat={this.handleFormatMonthPicker}
                                iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                            </th>
                            <th>{rowTitle.UpToNow}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>{rowTitle.NewSignups}</th>
                            <td>{data.NewSignUpByMonth}/{data.NewFdmByMonth}</td>
                            <td>{data.NewSignUpTotal}/{data.NewFdmTotal}</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TotalActive}</th>
                            <td>{data.ActivePlayerByMonth}</td>
                            <td>N/A</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TotalQualified}</th>
                            <td>{data.QualifiedMemberByMonth}</td>
                            <td>N/A</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TotalDeposits}</th>
                            <td>${DepositByMonth}</td>
                            <td>${DepositTotal}</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.Turnover}</th>
                            <td>${TurnoverByMonth}</td>
                            <td>${TurnoverTotal}</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.Winlost}</th>
                            <td>${WinlostByMonth}</td>
                            <td>${WinlostTotal}</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TrackingRate}</th>
                            <td>{data.UniqueClickByMonth}/{data.TotalClickByMonth}</td>
                            <td>N/A</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TrackingRatePC}</th>
                            <td>{data.UniqueClickPcLaptopByMonth}/{data.TotalClickPcLaptopByMonth}</td>
                            <td>N/A</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TrackingRateMobile}</th>
                            <td>{data.UniqueClickMobileByMonth}/{data.TotalClickMobileByMonth}</td>
                            <td>N/A</td>
                        </tr>
                    </tbody>
                </Table>
            </div>
        );
    }

    render(){
        const { monthDate, data } = this.state;
        return data && this._renderData(monthDate, data);
    }
}