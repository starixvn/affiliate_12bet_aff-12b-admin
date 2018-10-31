import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { statusList, sortByList, defaultSearchBy, defaultSortBy, resource } from '../modules/constants';

import { setReportTitle, setParam } from '../actions';


export class Filter extends Component {
    constructor(props) {
        super(props);

        this.state = {
            selectedCommGroup: defaultSearchBy,
            selectedLanguage: '',
            selectedStatus: '',
            userName: '',
            email: '',
            selectedSortBy: defaultSortBy,
            isSortAscending: true
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handleChangeSortAscending = () => {
        this.setState((prevState) => ({ isSortAscending: !prevState.isSortAscending }));
    }

    handleClickSearch = () => {
        const { selectedCommGroup, selectedLanguage, selectedStatus, userName, email, selectedSortBy, isSortAscending } = this.state;

        this.props.setReportTitle(this._buildReportTitle(selectedCommGroup, selectedLanguage, selectedStatus, userName, email));
        const param = {
            GroupId: selectedCommGroup,
            Language: selectedLanguage,
            Status: selectedStatus,
            UserName: userName,
            Email: email,
            SortBy: selectedSortBy,
            SortDirectionType: !isSortAscending
        }
        this.props.setParam(param);
    }

    _buildReportTitle(selectedCommGroup, selectedLanguage, selectedStatus, userName, email) {
        const { commGroupList, languageList } = this.props;

        const space = resource.Space;
        const apostrophe = resource.Apostrophe;
        const commGroupTitle = commGroupList[selectedCommGroup] || resource.AllCommGroups;
        const languageTitle = languageList[selectedLanguage] || resource.AllLanguages;
        const statusTitle = statusList[selectedStatus] || resource.AllStatuses;

        const userNameTitle = userName && space + resource.UsernameContains + apostrophe + userName + apostrophe;
        const emailTitle = email && space + resource.EmailContains + apostrophe + email + apostrophe;
        return `${resource.ReportName}${space}${commGroupTitle.toUpperCase()}${space}${languageTitle.toUpperCase()}${space}${statusTitle.toUpperCase()}${userNameTitle} ${emailTitle}`;
    }

    render() {
        const { selectedCommGroup, selectedLanguage, selectedSortBy, selectedStatus, isSortAscending } = this.state;
        const { commGroupList, languageList } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="SearchBy" className="mr-sm-2">{resource.SearchBy}</Label>
                        <Input type="select" name="selectedCommGroup" id="SearchBy" value={selectedCommGroup} onChange={this.handleChange} >
                            <option key="commGroup-all" value="">{resource.AllCommGroups}</option>
                            {
                                Object.keys(commGroupList).map(commGroup => {
                                    return (<option key={'commGroup-' + commGroup} value={commGroup}>{commGroupList[commGroup]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        {
                            <Input type="select" name="selectedLanguage" id="Language" value={selectedLanguage} onChange={this.handleChange}>
                                <option key="lang-all" value="">{resource.AllLanguages}</option>
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
                            <Input type="select" name="selectedStatus" id="Status" value={selectedStatus} onChange={this.handleChange}>
                                <option key="status-all" value="">{resource.AllStatuses}</option>
                                {
                                    Object.keys(statusList).map(status => {
                                        return (<option key={'status-' + status} value={status}>{statusList[status]}</option>)
                                    })
                                }
                            </Input>
                        }
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Input type="text" name="userName" id="Username" placeholder="Username" onChange={this.handleChange} />
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Input type="email" name="email" id="Email" placeholder="Email" onChange={this.handleChange} />
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="SortBy" className="mr-sm-2">{resource.SortBy}</Label>
                        <Input type="select" name="selectedSortBy" id="SortBy" value={selectedSortBy} onChange={this.handleChange} >
                            {
                                Object.keys(sortByList).map(sortBy => {
                                    return (<option key={'sortBy-' + sortBy} value={sortByList[sortBy]}>{sortBy}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label check>
                            {resource.AZ}
                            <Input className="ml-1" type="checkbox" checked={isSortAscending} onChange={this.handleChangeSortAscending} />
                        </Label>
                    </FormGroup>

                    <Button outline color="primary" type="button" onClick={this.handleClickSearch} >
                        <span className="oi oi-magnifying-glass"></span> {resource.Search}
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ languageList, commGroupList, param }) {
    return Object.assign({}, { languageList, commGroupList, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);