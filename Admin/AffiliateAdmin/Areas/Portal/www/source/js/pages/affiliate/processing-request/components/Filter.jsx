import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { statusList, resource } from '../libs/constants';
import { setReportTitle, setParam, setIsClickSearch, fetchData } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            userName: '',
            selectedStatus: '',
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            this.handleClickSearch();
        }
    }

    handleClickSearch = () => {
        const { pagingOption } = this.props;
        const { selectedStatus, userName } = this.state;

        this.props.setReportTitle(this._buildReportTitle(selectedStatus, userName));
        const param = Object.assign({},{ UserName: userName, Status: selectedStatus}, { PageIndex: 1, PageSize: pagingOption.PageSize })
        this.props.setParam(param);
        this.props.setIsClickSearch(true);
        this.props.fetchData(param);
    }

    _buildReportTitle(selectedStatus, userName) {
        const space = resource.Space;
        const apostrophe = resource.Apostrophe;
        const statusTitle = !!statusList[selectedStatus] ? selectedStatus : resource.AllStatuses;

        const titles = [resource.ReportName];
        userName && titles.push(resource.UsernameContains + apostrophe + userName + apostrophe);
        titles.push(statusTitle.toUpperCase());

        return titles.join(space);
    }

    render() {
        const { selectedStatus } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="my-2 mr-sm-2">
                        <Input type="text" name="userName" id="Username" placeholder="Username"
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChange} />
                    </FormGroup>


                    <FormGroup className="my-2 mr-sm-2">
                        <Label for="selectedStatus" className="mr-sm-2">{resource.SortBy}</Label>
                        <Input type="select" name="selectedStatus" id="selectedStatus" value={selectedStatus} onChange={this.handleChange} >
                            <option key={'status-all'} value=''>All Statuses</option>
                            {
                                Object.keys(statusList).map(key => {
                                    return (<option key={'sortBy-' + key} value={key}>{key}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <Button className="my-2" outline color="primary" type="button" onClick={this.handleClickSearch} >
                        <span className="oi oi-magnifying-glass"></span> {resource.Search}
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ param, pagingOption }) {
    return { param, pagingOption };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParam, setIsClickSearch, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);