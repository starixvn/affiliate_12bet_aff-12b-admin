import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { headerText } from '../modules/constants';

import { setReportTitle, setParam } from '../actions';


export class Filter extends Component {
    constructor(props) {
        super(props);

        this.state = {
            selectedCommonGroup: '',
            selectedLanguage: '',
            selectedStatus: '',
            userName: '',
            email: '',
            selectedSortBy: '',
            isSortAscending: true
        }
    }

    handleChangeSearchBy = (event) => {
        this.setState({ selectedCommonGroup : event.target.value });
    }

    handleChangeLanguage = (event) => {
        this.setState({ selectedLanguage : event.target.value });
    }

    handleChangeStatus = (event) => {
        this.setState({ selectedSortBy : event.target.value });
    }

    handleChangeUsername = (event) => {

    }

    handleChangeEmail = (event) => {

    }

    handleChangeSortAscending = (event) => {

    }

    handleClickSearch = (event) => {

    }

    render() {
        const { selectedCommonGroup, selectedLanguage, selectedSortBy } = this.state;
        const { languageList } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="SearchBy" className="mr-sm-2">Search By</Label>
                        <Input type="select" name="SearchBy" id="SearchBy" value="selectedCommonGroup" onChange={this.handleChangeSearchBy} >
                            <option key="commonGroup-all" value="">All comm groups</option>

                        </Input>
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                    {
                        <Input type="select" name="Languge" id="Language" value={selectedLanguage} onChange={this.handleChangeLanguage}>
                            <option key="lang-all" value="">All Languages</option>
                            {
                                Object.keys(languageList).map(lang => {
                                    return (<option key={'lang-' + lang} value={lang}>{languageList[lang]}</option>)
                                })
                            }
                        </Input>
                    }
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                    {
                        <Input type="select" name="Status" id="Status" value="selectedStatus" onChange={this.handleChangeStatus}>
                            <option key="status-all" value="">All Statuses</option>

                        </Input>
                    }
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Input type="text" name="Username" id="Username" placeholder="Username"  onChange={this.handleChangeUsername} />
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Input type="email" name="Email" id="Email" placeholder="Email"  onChange={this.handleChangeEmail} />
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="SortBy" className="mr-sm-2">Sort By</Label>
                        <Input type="select" name="SortBy" id="SortBy" value="sortBy" onChange={this.handleChangeSortBy} >
                            <option key="Username" value="">Username</option>

                        </Input>
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                       <Label check>
                           A-Z
                           <Input className="ml-1" type="checkbox" checked="checked" onChange={this.handleChangeSortAscending} />
                       </Label>
                    </FormGroup>

                    <Button outline color="primary" type="button" onClick={this.handleClickSearch}>
                        <span className="oi oi-magnifying-glass"></span> Search
                    </Button>
</Form>
            </div>
        );
    }
    }

    function mapStateToProps({ pagingOption, languageList, param }) {
        return Object.assign({}, { pagingOption, languageList, param });
    }

    function mapDispatchToProps(dispatch) {
        return bindActionCreators({ setReportTitle, setParam }, dispatch);
    }

    export default connect(mapStateToProps, mapDispatchToProps)(Filter);