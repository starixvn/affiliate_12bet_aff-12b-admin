import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import { searchByList, resource, minDate, all } from '../libs/constants';
import { getDateRange } from '../../../modules/utils';
import { setParam } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const now = new Date(today);

        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = {
            searchBy: all,
            isGetAll: true,
            fromDate: this.nowString,
            toDate: this.nowString,
        }
    }

    handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;

        this.setState({ [target.name]: value });
    }

    handleChangeFromDate = (momentDate) => {
        this.setState(getDateRange(momentDate, this.state.toDate));
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ toDate: momentDate.format("MM/DD/YYYY") });
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this.handleSearch();
        }
    }

    handleSearch = () => {
        const { searchBy, fromDate, toDate, isGetAll } = this.state;
        const { pagingOption } = this.props;

        const param = Object.assign({}, {
            Important: searchBy,
            IsGetAll: isGetAll,
            FromDate: fromDate,
            ToDate: toDate,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        });

        this.props.setParam(param);
    }

    render() {
        const { searchBy, fromDate, toDate, isGetAll } = this.state;

        return (
            <div className="filter-container mx-1 mt-3">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-3">
                        <Label className="mr-2">{resource.SearchBy}</Label>
                        <Input type="select" name="searchBy" value={searchBy} onChange={this.handleChange}>
                            <option key={'searchBy-all'} value={all}>{resource.All}</option>
                            {
                                Object.keys(searchByList).map(key => {
                                    return (<option key={'searchBy-' + key} value={key}>{searchByList[key]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mx-sm-3">
                        <FormGroup check>
                            <Label check>
                                {resource.AllDate}
                                <Input name="isGetAll" className="ml-1" type="checkbox" checked={isGetAll} onChange={this.handleChange} />
                            </Label>
                        </FormGroup>
                    </FormGroup>
                    <FormGroup className="mr-sm-3">
                        <Label className="mr-sm-2">{resource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate} isEnable={!isGetAll}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-3">
                        <Label className="mr-sm-2">{resource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate} isEnable={!isGetAll}
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