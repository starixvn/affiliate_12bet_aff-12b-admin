import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import { resource, searchByList, statusList, globalResource } from '../modules/constants';
import { getNewFilterDate } from '../../../modules/newFilterDate';
import { setParam } from '../actions';

const minDate = new Date(2009, 11, 1);
const searchByUserName = 0;

export class Filter extends Component {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const now = new Date(today);

        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;
        const yesterday = new Date(today);
        yesterday.setDate(yesterday.getDate() - 1);

        this.state = {
            searchBy: searchByUserName,
            userName: null,
            status: null,
            fromDate: `${yesterday.getMonth() + 1}/${yesterday.getDate()}/${yesterday.getFullYear()}`,
            toDate: this.nowString,
            isGetAll: false
        }
    }

    componentDidMount() {
        this.handleSetParam();
    }

    handleSetParam() {
        const { searchBy, userName, status, fromDate, toDate, isGetAll } = this.state;
        const isSearchByUserName = parseInt(searchBy) === searchByUserName;

        const newParam = Object.assign({}, this.props.param, {
            Username: isSearchByUserName ? userName : null,
            IsGetAll: isSearchByUserName ? true : isGetAll,
            Status: isSearchByUserName ? null : status,
            FromDate: isSearchByUserName ? null : fromDate,
            ToDate: isSearchByUserName ? null : toDate
        });

        this.props.setParam(newParam);
    }

    handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;

        this.setState({ [target.name]: value });
    }

    handleChangeFromDate = (momentDate) => {
        this.setState(getNewFilterDate(momentDate, this.state.toDate));
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ toDate: momentDate.format("MM/DD/YYYY") });
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this.handleSetParam();
        }
    }

    handleSearch = () => {
        this.handleSetParam();
    }

    _renderSearchByOthers() {
        const { status, fromDate, toDate, isGetAll } = this.state;

        return (
            <React.Fragment>
                <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                    <Label for="status" className="mr-sm-2">{resource.Status}</Label>
                    <Input type="select" name="status" value={status} onChange={this.handleChange} disabled={isGetAll}>
                        <option value="">{globalResource.All}</option>
                        {
                            Object.keys(statusList).map(status => {
                                return (<option key={`status-${status}`} value={status}>{statusList[status]}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                    <Label className="mr-sm-2">{globalResource.From}</Label>
                    <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate} isEnable={!isGetAll}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                        onChange={this.handleChangeFromDate} />
                </FormGroup>
                <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                    <Label className="mr-sm-2">{globalResource.To}</Label>
                    <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate} isEnable={!isGetAll}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                        onChange={this.handleChangeToDate} />
                </FormGroup>
                <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                    <FormGroup check>
                        <Label check>
                            {globalResource.GetAll}
                            <Input name="isGetAll" className="ml-1 mr-2" type="checkbox" checked={isGetAll} onChange={this.handleChange} />
                        </Label>
                    </FormGroup>
                </FormGroup>
            </React.Fragment>
        );
    }

    render() {
        const { searchBy, userName } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                        <Label className="mr-2">{resource.SearchBy}</Label>
                        <Input type="select" name="searchBy" value={searchBy} onChange={this.handleChange}>
                            {
                                Object.keys(searchByList).map(key => {
                                    return (<option key={'searchBy-' + key} value={key}>{searchByList[key]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    {parseInt(searchBy) === searchByUserName ? (
                        <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                            <Label className="mr-2">{resource.UserName}</Label>
                            <Input type="text" name="userName" value={userName} onKeyPress={this.handlePressEnter} onChange={this.handleChange} />
                        </FormGroup>
                    ) : this._renderSearchByOthers()
                    }
                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, param }) {
    return Object.assign({}, { today, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);