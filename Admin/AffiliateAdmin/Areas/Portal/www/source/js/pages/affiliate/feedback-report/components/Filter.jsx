import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import MultipleSelect from '../../../../components/multiple-select';

import { minDate, resource } from '../libs/constants';
import { formatUnit, handleAfterChangeFromDate, handleAfterChangeToDate } from '../../../../modules/utils';
import { setParam, setReportTitle } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today, rateList } = this.props;
        const now = new Date(today);

        this.nowString = `${formatUnit(now.getMonth() + 1)}/${formatUnit(now.getDate())}/${now.getFullYear()}`;

        const initialRates = rateList.filter(r => r.Checked).map(r => r.RateId).join(',');

        this.state = {
            affName: '',
            fromDate: `${formatUnit(now.getMonth() + 1)}/01/${now.getFullYear()}`,
            toDate: this.nowString,
            selectedRates: initialRates
        }

        this.allRates = initialRates;
    }

    handleChange = (event) => {
        const target = event.target;

        this.setState({ [target.name]: target.value });
    }

    handleChangeFromDate = (momentDate) => {
        this.setState(prevState => handleAfterChangeFromDate(momentDate, prevState.toDate));
    }

    handleChangeToDate = (momentDate) => {
        this.setState(prevState => handleAfterChangeToDate(momentDate, prevState.fromDate));
    }

    handleChangeRate = (item, selectedRates) => {
        this.setState({ selectedRates });
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this.handleSearch();
        }
    }

    handleSearch = () => {
        const { affName, fromDate, toDate, selectedRates } = this.state;
        const { pagingOption } = this.props;

        this.props.setReportTitle(this._buildReportTitle(affName, selectedRates, fromDate, toDate));
        const param = Object.assign({}, {
            AffName: affName,
            RateIds: selectedRates,
            FromDate: fromDate,
            ToDate: toDate,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        });

        this.props.setParam(param);
    }

    _buildReportTitle(affName, selectedRates, fromDate, toDate) {
        const userNameTitle = `${affName && '- Affiliate contains \'' + affName + '\''}`;
        const rate = selectedRates == this.allRates ? 'All Rates' : this._handleGetRateName(selectedRates);

        const fromToTitle = `FROM ${fromDate} TO ${toDate}`;
        const reportName = 'FEEDBACK REPORT ';

        return `${reportName}${userNameTitle} ${'- ' + rate} - ${fromToTitle}`;
    }

    _handleGetRateName(selectedRates) {
        return this.props.rateList.filter(r => selectedRates.includes(r.RateId)).map(r => r.RateName).join(', ');
    }

    getRateList = () => {
        const { selectedRates } = this.state;
        const { rateList } = this.props;

        if (selectedRates === this.allRates) {
            return rateList.slice();
        }

        const selectedRateIds = selectedRates.split(',');

        return rateList.map(r => {
            return Object.assign({}, r, { Checked: selectedRateIds.indexOf(r.RateId.toString()) !== -1 })
        })
    }

    render() {
        const rates = this.getRateList();
        const { affName, fromDate, toDate} = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-3 my-2">
                        <Label className="mr-sm-2">{resource.Affiliate}</Label>
                        <Input type="text" name="affName" value={affName} onKeyPress={this.handlePressEnter} onChange={this.handleChange} />
                    </FormGroup>
                    <FormGroup className="my-2 mr-sm-2">
                        <Label className="mr-sm-2">{resource.Rate}</Label>
                        <MultipleSelect
                            dataSource={rates}
                            keyField="RateId"
                            valueField="RateName"
                            statusField="Checked"
                            maxDisplayItemCount={1}
                            noneSelectedLabel="Please Select"
                            onChange={this.handleChangeRate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-3 my-2">
                        <Label className="mr-sm-2">{resource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-3 my-2">
                        <Label className="mr-sm-2">{resource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>
                    <FormGroup className="my-2 mr-sm-2">
                        <Button outline color="primary" type="button" onClick={this.handleSearch}>
                            <span className="oi oi-magnifying-glass"></span> {resource.Search}
                        </Button>
                    </FormGroup>
                    <FormGroup className="my-2 mr-sm-2 feedback-note">
                        <Label>'None' when the user only sends a message and does not choose an emoji.</Label>
                    </FormGroup>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, rateList, pagingOption }) {
    return Object.assign({}, { today, rateList, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setReportTitle }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);