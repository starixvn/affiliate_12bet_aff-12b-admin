import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import MultipleSelect from '../../../../components/multiple-select';

import { minDate, resource, rateList, allRates } from '../libs/constants';
import { handleAfterChangeFromDate, handleAfterChangeToDate } from '../../../../modules/utils';
import { setParam } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const now = new Date(today);

        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = {
            affName: '',
            fromDate: `${now.getMonth() + 1}/${1}/${now.getFullYear()}`,
            toDate: this.nowString,
            selectedRates: allRates
        }
    }

    handleChange = (event) => {
        const target = event.target;

        this.setState({ [target.name]: value });
    }

    handleChangeFromDate = (momentDate) => {
        this.setState(prevState => handleAfterChangeFromDate(momentDate, prevState.toDate));
    }

    handleChangeToDate = (momentDate) => {
        this.setState(prevState => handleAfterChangeToDate(momentDate, prevState.fromDate));
    }

    handleChangeRate = (item, selectedRates) => {

        console.log(selectedRates);

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

        const param = Object.assign({}, {
            AffName: affName,
            RateIds: this._isCheckedAllRates() ? allRates : selectedRates,
            FromDate: fromDate,
            ToDate: toDate,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        });

        this.props.setParam(param);
    }

    _isCheckedAllRates = () => {
        const { selectedRates } = this.state;

        return selectedRates === allRates || selectedRates.split(',').length === rateList.length;
    }

    getRateList = () => {
        const { selectedRates } = this.state;
        if (selectedRates === allRates) {
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
                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {resource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, pagingOption }) {
    return Object.assign({}, { today, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);