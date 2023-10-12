import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import MultipleSelect from '../../../../components/multiple-select';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { statusList, sortByList, defaultSortBy, resource, processingStatusList } from '../libs/constants';
import { setReportTitle, setParam, setSelectedColumn, setIsClickSearch } from '../actions';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { claimResource } from '../../../../modules/claim';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.initialGroupId = this.props.groupId;

        this.state = {
            selectedCommGroup: this.initialGroupId,
            selectedTargetCountry: '',
            selectedLanguage: '',
            selectedStatus: props.param.Status || '',
            selectedProcessingStatus: '',
            userName: '',
            email: '',
            affId: '',
            selectedSortBy: defaultSortBy,
            isSortAscending: true
        }
    }

    componentDidMount = () => {
        if (!!this.initialGroupId || this.props.param.IsFromHome) {
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
        const { selectedCommGroup, selectedTargetCountry, selectedLanguage, selectedStatus, selectedProcessingStatus, userName, email, selectedSortBy, isSortAscending, affId } = this.state;

        const affIdInput = affId.trim();
        this.props.setReportTitle(this._buildReportTitle(selectedCommGroup, selectedTargetCountry, selectedLanguage, selectedStatus, selectedProcessingStatus, userName, email, affIdInput));
        const param = {
            GroupId: selectedCommGroup,
            TargetCountry: selectedTargetCountry,
            Language: selectedLanguage,
            Status: selectedStatus,
            ProcessingStatus: selectedProcessingStatus,
            UserName: userName,
            Email: email,
            SortBy: selectedSortBy,
            SortDirectionType: !isSortAscending,
            AffId: affIdInput,
            IsFromHome: false
        }
        this.props.setParam(param);
        this.props.setIsClickSearch(true);
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

    _buildReportTitle(selectedCommGroup, selectedTargetCountry, selectedLanguage, selectedStatus, selectedProcessingStatus, userName, email, affId) {
        const { commGroupList, targetCountryList, languageList, isShowCommFilter, isShowAccStatusFilter, isShowLangFilter, isShowProcessingStatusFilter, isShowTargetCountryFilter } = this.props;

        const space = resource.Space;
        const apostrophe = resource.Apostrophe;
        const targetCountryTitle = targetCountryList[selectedTargetCountry] || resource.AllTargetCountries;
        const commGroupTitle = commGroupList[selectedCommGroup] || resource.AllCommGroups;
        const languageTitle = languageList[selectedLanguage] || resource.AllLanguages;
        const statusTitle = statusList[selectedStatus] || resource.AllStatuses;
        const processingStatusTitle = processingStatusList[selectedProcessingStatus] || resource.AllProcessing;

        const titles = [resource.ReportName];
        isShowCommFilter && titles.push(commGroupTitle.toUpperCase());
        isShowTargetCountryFilter && titles.push(targetCountryTitle.toUpperCase());
        isShowLangFilter && titles.push(languageTitle.toUpperCase());
        isShowAccStatusFilter && titles.push(statusTitle.toUpperCase());
        isShowProcessingStatusFilter && titles.push(processingStatusTitle.toUpperCase())
        userName && titles.push(resource.UsernameContains + apostrophe + userName + apostrophe);
        email && titles.push(resource.EmailContains + apostrophe + email + apostrophe);
        affId && titles.push(resource.AffIdContains + apostrophe + affId + apostrophe);

        return titles.join(space);
    }

    render() {
        const { selectedCommGroup, selectedTargetCountry, selectedLanguage, selectedSortBy, selectedStatus, isSortAscending, selectedProcessingStatus } = this.state;
        const { columnList, languageList, targetCountryList, commGroupList, isShowEmailFilter, sortKeys, isShowAccStatusFilter, isShowCommFilter, isShowLangFilter, isShowTargetCountryFilter, isShowProcessingStatusFilter } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    {isShowCommFilter &&
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
                    }

                    {isShowTargetCountryFilter &&
                        <FormGroup className="my-2 mr-sm-2">
                            {
                                <Input type="select" name="selectedTargetCountry" id="TargetCountry" value={selectedTargetCountry} onChange={this.handleChange}>
                                    <option key="target-all" value="">{resource.AllTargetCountries}</option>
                                    {
                                        Object.keys(targetCountryList).map(target => {
                                            return (<option key={'target-' + target} value={target}>{targetCountryList[target]}</option>)
                                        })
                                    }
                                </Input>
                            }
                        </FormGroup>
                    }

                    {isShowLangFilter &&
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
                    }

                    {isShowAccStatusFilter &&
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
                    }

                    {isShowProcessingStatusFilter &&
                        <FormGroup className="my-2 mr-sm-2">
                            {
                                <Input type="select" name="selectedProcessingStatus" id="ProcessingStatus" value={selectedProcessingStatus} onChange={this.handleChange}>
                                    <option key="processing-status-all" value="">{resource.AllProcessing}</option>
                                    {
                                        Object.keys(processingStatusList).map(status => {
                                            return (<option key={'processing-status-' + status} value={status}>{processingStatusList[status]}</option>)
                                        })
                                    }
                                </Input>
                            }
                        </FormGroup>
                    }

                    <FormGroup className="my-2 mr-sm-2">
                        <Input type="text" name="userName" id="Username" placeholder="Username"
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChange} />
                    </FormGroup>

                    {isShowEmailFilter &&
                        <FormGroup className="my-2 mr-sm-2">
                            <Input type="email" name="email" id="Email" placeholder="Email"
                                onKeyPress={this.handlePressEnter}
                                onChange={this.handleChange} />
                        </FormGroup>
                    }

                    <FormGroup className="my-2 mr-sm-2">
                        <Input type="text" name="affId" id="AffId" placeholder="Affiliate Id"
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChange} />
                    </FormGroup>

                    <FormGroup className="my-2 mr-sm-2">
                        <Label for="SortBy" className="mr-sm-2">{resource.SortBy}</Label>
                        <Input type="select" name="selectedSortBy" id="SortBy" value={selectedSortBy} onChange={this.handleChange} >
                            {
                                sortKeys.map(item => {
                                    return (<option key={'sortBy-' + item.key} value={item.value}>{item.key}</option>)
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
                        <Label className="mr-sm-2">{resource.DisplayedColumns}</Label>
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

function mapStateToProps({ languageList, targetCountryList, commGroupList, param, groupId, columnList, claimsPrincipal }) {
    const isShowEmailFilter = claimsPrincipal.hasReadClaim(claimResource.AffEmail);
    const isShowAccStatusFilter = claimsPrincipal.hasReadClaim(claimResource.AffAccountStatus);
    const isShowCommFilter = claimsPrincipal.hasReadClaim(claimResource.AssignCommGroup);
    const isShowTargetCountryFilter = claimsPrincipal.hasReadClaim(claimResource.AffTargetCountry);
    const isShowLangFilter = claimsPrincipal.hasReadClaim(claimResource.LanguageAmendment);
    const isShowProcessingStatusFilter = claimsPrincipal.hasReadClaim(claimResource.ProcRequest);
    const sortKeys = Object.keys(sortByList)
        .filter(key => {
            const item = sortByList[key];
            return !item.renderClaim || claimsPrincipal.hasClaim(item.renderClaim);
        })
        .map(key => { return { key: key, value: sortByList[key].value } });
    return Object.assign({}, { languageList, targetCountryList, commGroupList, param, groupId, columnList, isShowEmailFilter, sortKeys, isShowCommFilter, isShowAccStatusFilter, isShowLangFilter, isShowTargetCountryFilter, isShowProcessingStatusFilter });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParam, setSelectedColumn, setIsClickSearch }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);