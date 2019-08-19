import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import MultipleSelect from '../../../components/multiple-select';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { statusList, sortByList, defaultSortBy, resource } from '../modules/constants';
import { setReportTitle, setParam, setSelectedColumn } from '../actions';
import { htmlDecode } from '../../../modules/htmlHelper';

export class Filter extends Component {
    constructor(props) {
        super(props);

        this.initialGroupId = this.props.groupId;

        this.state = {
            selectedCommGroup: this.initialGroupId,
            selectedLanguage: '',
            selectedStatus: props.param.Status || '',
            userName: '',
            email: '',
            affId: '',
            selectedSortBy: defaultSortBy,
            isSortAscending: true
        }
    }

    componentDidMount() {
        if (this.initialGroupId !== 0 || this.props.param.IsFromHome) {
            this.handleClickSearch();
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handleChangeSortAscending = () => {
        this.setState((prevState) => ({ isSortAscending: !prevState.isSortAscending }));
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            this.handleClickSearch();
        }
    }

    handleClickSearch = () => {
        const { selectedCommGroup, selectedLanguage, selectedStatus, userName, email, selectedSortBy, isSortAscending, affId } = this.state;

        const affIdInput = affId.trim();
        this.props.setReportTitle(this._buildReportTitle(selectedCommGroup, selectedLanguage, selectedStatus, userName, email, affIdInput));
        const param = {
            GroupId: selectedCommGroup,
            Language: selectedLanguage,
            Status: selectedStatus,
            UserName: userName,
            Email: email,
            SortBy: selectedSortBy,
            SortDirectionType: !isSortAscending,
            AffId: affIdInput
        }
        this.props.setParam(param);
    }

    handleChangeColumn = (item, selectedColumn) => {
        const { columnList } = this.props;
        const selectedColumnList = selectedColumn.split(',').filter(p => !!p);

        const newColumnList = columnList.map(item => {
            const { columnKey, columnName } = item;
            const checked = selectedColumnList.indexOf(columnKey) !== -1;

            return {
                columnKey,
                columnName,
                checked
            };
        })

        this.props.setSelectedColumn(newColumnList);
    }

    _buildReportTitle(selectedCommGroup, selectedLanguage, selectedStatus, userName, email, affId) {
        const { commGroupList, languageList } = this.props;

        const space = resource.Space;
        const apostrophe = resource.Apostrophe;
        const commGroupTitle = commGroupList[selectedCommGroup] || resource.AllCommGroups;
        const languageTitle = languageList[selectedLanguage] || resource.AllLanguages;
        const statusTitle = statusList[selectedStatus] || resource.AllStatuses;

        const userNameTitle = userName && space + resource.UsernameContains + apostrophe + userName + apostrophe;
        const emailTitle = email && space + resource.EmailContains + apostrophe + email + apostrophe;
        const affIdTitle = affId && space + resource.AffIdContains + apostrophe + affId + apostrophe;
        return `${resource.ReportName}${space}${commGroupTitle.toUpperCase()}${space}${languageTitle.toUpperCase()}${space}${statusTitle.toUpperCase()}${userNameTitle}${emailTitle}${affIdTitle}`;
    }

    render() {
        const { selectedCommGroup, selectedLanguage, selectedSortBy, selectedStatus, isSortAscending } = this.state;
        const { commGroupList, languageList, columnList } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="my-2 mr-sm-2">
                        <Label for="SearchBy" className="mr-sm-2">{resource.SearchBy}</Label>
                        <Input type="select" name="selectedCommGroup" id="SearchBy" value={selectedCommGroup} onChange={this.handleChange} >
                            <option key="commGroup-all" value={0}>{resource.AllCommGroups}</option>
                            {
                                Object.keys(commGroupList).map(commGroup => {
                                    return (<option key={'commGroup-' + commGroup} value={commGroup}>{htmlDecode(commGroupList[commGroup])}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <FormGroup className="my-2 mr-sm-2">
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

                    <FormGroup className="my-2 mr-sm-2">
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

                    <FormGroup className="my-2 mr-sm-2">
                        <Input type="text" name="userName" id="Username" placeholder="Username"
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChange} />
                    </FormGroup>

                    <FormGroup className="my-2 mr-sm-2">
                        <Input type="email" name="email" id="Email" placeholder="Email"
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChange} />
                    </FormGroup>

                    <FormGroup className="my-2 mr-sm-2">
                        <Input type="text" name="affId" id="AffId" placeholder="Affiliate Id"
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChange} />
                    </FormGroup>

                    <FormGroup className="my-2 mr-sm-2">
                        <Label for="SortBy" className="mr-sm-2">{resource.SortBy}</Label>
                        <Input type="select" name="selectedSortBy" id="SortBy" value={selectedSortBy} onChange={this.handleChange} >
                            {
                                Object.keys(sortByList).map(sortBy => {
                                    return (<option key={'sortBy-' + sortBy} value={sortByList[sortBy]}>{sortBy}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <FormGroup className="my-2 mr-sm-2">
                        <Label check>
                            {resource.AZ}
                            <Input className="ml-1" type="checkbox" checked={isSortAscending} onChange={this.handleChangeSortAscending} />
                        </Label>
                    </FormGroup>

                    <FormGroup className="my-2 mr-sm-2">
                        <Label for="SortBy" className="mr-sm-2">{resource.DisplayedColumns}</Label>
                        <MultipleSelect
                            dataSource={columnList}
                            keyField="columnKey"
                            valueField="columnName"
                            statusField="checked"
                            maxDisplayItemCount={1}
                            noneSelectedLabel="Please Select"
                            onChange={this.handleChangeColumn} />
                    </FormGroup>

                    <Button className="my-2" outline color="primary" type="button" onClick={this.handleClickSearch} >
                        <span className="oi oi-magnifying-glass"></span> {resource.Search}
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ languageList, commGroupList, param, groupId, columnList }) {
    return Object.assign({}, { languageList, commGroupList, param, groupId, columnList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParam, setSelectedColumn }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);