import React, { Component } from 'react';

import MonthPicker from '@9softstudio/react-monthpicker';
import { Table, FormGroup, Label } from 'reactstrap';
import { rowTitle } from '../modules/constants';
import { shortedMonthNames } from '../../../../modules/constants';

export default class Content extends Component {
    constructor(props) {
        super(props);
        const { monthDate } = this.props;

        this.state = {
            monthDate
        }
    }

    handleSelectMonth = (month, year) => {
        this.setState({
            monthDate: new Date(year, month - 1)
        })
    }

    handleFormatMonthPicker = (month, year) => {
        return `${shortedMonthNames[month - 1]}-${year}`
    }

    handleChangeMonthPicker = (event) => {
        event.preventDefault();

        const { monthDate } = this.state;

        const param = {
            MonthDate: `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`
        }
    }

    render(){
        const { monthDate } = this.state;
        return(
            <div className="table-responsive table-striped my-2">
                <Table className="tableFilter">
                    <thead className="theadFilter">
                        <tr className="trFilter">
                            <th>
                                <span id="togtop" title="Toggle Statistics">&nbsp;</span>
                                Statistics
                            </th>
                            <th className="thFilter">
                                <MonthPicker minMonth={12} minYear={2009}
                                selectedDropdownYear={monthDate.getFullYear()}
                                selectedMonth={monthDate.getMonth() + 1}
                                onSelect={this.handleSelectMonth}
                                onFormat={this.handleFormatMonthPicker}
                                iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                            </th>
                            <th>Up To Now</th>
                        </tr>
                    </thead>
                    <tbody className="tbodyFilter">
                        <tr>
                            <th>{rowTitle.NewSignups}</th>
                            <td>0/0</td>
                            <td>3,7142/1,829</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TotalActive}</th>
                            <td>1</td>
                            <td>N/A</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TotalQualified}</th>
                            <td>1</td>
                            <td>N/A</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TotalDeposits}</th>
                            <td>$0.00</td>
                            <td>$0.00</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.Turnover}</th>
                            <td>$422.86</td>
                            <td>$52,985.42</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.Winlost}</th>
                            <td>$105.72</td>
                            <td>-$25,880.05</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TrackingRate}</th>
                            <td>0/0</td>
                            <td>N/A</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TrackingRatePC}</th>
                            <td>10/20</td>
                            <td>N/A</td>
                        </tr>
                        <tr>
                            <th>{rowTitle.TrackingRateMobile}</th>
                            <td>0/0</td>
                            <td>N/A</td>
                        </tr>
                    </tbody>
                </Table>
            </div>
        );
    }
}