import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { DatePicker } from '@9softstudio/react-datepicker';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '../../../../components/multiple-select';

import { getDateRange } from '../../../../modules/utils';
import { headerTitle, deviceTypes, globalResource, minDate, groupByCountry, groupByList } from '../libs/constants';
import { setSelectedPageDetailFilter, setParam, setShortCodeLanguageList, getPageList, getCountryList, setIsGroupByCountry } from '../actions';
import { htmlDecode } from '../../../../modules/htmlHelper';

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const { today, languageList } = this.props;
        const now = new Date(today);

        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        const shortCodeLanguageList = {};
        for (let languageItem of languageList) {
            const shortCode = languageItem.ShortedCode;
            shortCodeLanguageList[shortCode] = languageItem.LanguageName;
        }
        this.props.setShortCodeLanguageList(shortCodeLanguageList);

        this.state = {
            groupBy: groupByCountry,
            initialCountries: '',
            selectedCountries: '',
            languageCode: '',
            selectedPage: '',
            fromDate: `${now.getMonth() + 1}/01/${now.getFullYear()}`,
            toDate: this.nowString,
            deviceType: 0,
            pageList: null,
            countryList: null
        };
    }

    componentDidMount() {
        if (!this.state.pageList) {
            this.handleGetPageList();
        }

        if (!this.state.countryList) {
            this.handleCountryList();
        }
    }

    handleGetPageList = async () => {
        const pageList = await this.props.getPageList();

        this.setState({ pageList });
    }

    handleCountryList = async () => {
        const countryList = await this.props.getCountryList();
        const initialCountries = countryList.filter(r => r.Checked).map(r => r.RateId).join(',');

        this.setState({ initialCountries, selectedCountries: initialCountries, countryList });
    }

    handleChangeCountry = (item, selectedCountries) => {
        const countryList = this.getCountryList(selectedCountries);

        this.setState({ selectedCountries, countryList });
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handleChangeFromDate = (momentDate) => {
        this.setState(prevState => getDateRange(momentDate, prevState.toDate));
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ toDate: momentDate.format("MM/DD/YYYY") });
    }

    handleSearch = () => {
        const { groupBy, selectedPage, fromDate, toDate, deviceType } = this.state;
        const isGroupByCountry = groupBy === groupByCountry;
        const optionName = isGroupByCountry ? "selectedCountries" : "languageCode";
        const param = Object.assign({}, { selectedPage, [optionName]: this.state[optionName], fromDate, toDate, deviceType, affId: this.props.affId });

        this.props.setParam(param);
        this.props.setIsGroupByCountry(isGroupByCountry);
        this.props.setSelectedPageDetailFilter(null);
    }

    getCountryList = (selectedCountries) => {
        const { initialCountries, countryList } = this.state;

        if (selectedCountries === initialCountries) {
            return countryList ? countryList.slice() : [];
        }

        const selectedCountryCodes = selectedCountries.split(',');

        return countryList.map(r => {
            return Object.assign({}, r, { Checked: selectedCountryCodes.indexOf(r.CountryId.toString()) !== -1 })
        })
    }

    render() {
        const { shortCodeLanguageList } = this.props;
        const { groupBy, selectedCountries, selectedPage, languageCode, deviceType, fromDate, toDate, pageList, countryList } = this.state;
        const isGroupByCountry = groupBy === groupByCountry;

        return pageList && (
            <div className="filter-container py-0">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-3 my-2">
                        <Label className="mr-sm-2">{headerTitle.GroupBy}</Label>
                        <Input type="select" name="groupBy" value={groupBy} onChange={this.handleChange}>
                            {
                                groupByList.map((value, index) => {
                                    return (<option key={'groupBy-' + index} value={index + 1}>{value}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    {
                        isGroupByCountry && (
                            <FormGroup className="my-2 mr-sm-2">
                                <Label className="mr-sm-2">{headerTitle.Country}</Label>
                                <div className={!selectedCountries && "multiple-select-wrapper is-invalid"}>
                                    <MultipleSelect
                                        dataSource={countryList}
                                        keyField="CountryCode"
                                        valueField="CountryName"
                                        statusField="Checked"
                                        maxDisplayItemCount={1}
                                        noneSelectedLabel="Please Select"
                                        onChange={this.handleChangeCountry} />
                                </div>
                            </FormGroup>
                        )
                    }
                    <FormGroup className="mr-sm-2 my-2">
                        <Label for="selectedPage" className="mr-sm-2">{headerTitle.Page}</Label>
                        <Input type="select" name="selectedPage" id="selectedPage" value={selectedPage} onChange={this.handleChange}>
                            <option value="">{globalResource.All}</option>
                            {
                                pageList.map(page => {
                                    return (<option key={`page-${page.PageCode}`} value={page.PageCode}>{htmlDecode(page.PageName)}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    {
                        !isGroupByCountry && (
                            <FormGroup className="mr-sm-2 my-2">
                                <Label for="languageCode" className="mr-sm-2">{headerTitle.Language}</Label>
                                <Input type="select" name="languageCode" id="languageCode" value={languageCode} onChange={this.handleChange}>
                                    <option value="">{globalResource.All}</option>
                                    {
                                        Object.keys(shortCodeLanguageList).map(key => {
                                            return (<option key={`language-${key}`} value={key}>{shortCodeLanguageList[key]}</option>)
                                        })
                                    }
                                </Input>
                            </FormGroup>
                        )
                    }
                    <FormGroup className="mr-sm-2 my-2">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-2 my-2">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-2 my-2">
                        <Label for="deviceType" className="mr-sm-2">{headerTitle.Device}</Label>
                        <Input type="select" name="deviceType" id="deviceType" value={deviceType} onChange={this.handleChange}>
                            {
                                Object.keys(deviceTypes).map(key => {
                                    return (<option key={`deviceType-${deviceTypes[key]}`} value={deviceTypes[key]}>{key}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <Button outline className="my-2" color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, affCommonInfo, languageList, countryList, landingPageStatistics }) {
    const { affId } = affCommonInfo;
    const { shortCodeLanguageList } = landingPageStatistics;
    return Object.assign({}, {
        today,
        affId,
        countryList,
        languageList,
        shortCodeLanguageList
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedPageDetailFilter, setParam, setShortCodeLanguageList, getPageList, getCountryList, setIsGroupByCountry }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);