import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import MultipleSelect from '../../../../components/multiple-select';

import { setSelectedSiteDetailFilter, setParam, getCountryList } from '../actions';
import { getDateRange } from '../../../../modules/utils';
import { headerText, globalResource, deviceTypes, pcLaptop, minDate } from '../libs/constants';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today, param } = this.props;
        const { deviceType, fromDate, toDate } = param;

        const now = new Date(today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;
        const firstDayOfCurrentMonth = `${now.getMonth() + 1}/01/${now.getFullYear()}`;

        this.state = {
            initialSelectedCountries: '',
            initialCountryList: null,
            selectedCountries: '',
            countryList: null,
            deviceType: param.isClickLink ? deviceType : deviceTypes[pcLaptop],
            fromDate: param.isClickLink ? fromDate : firstDayOfCurrentMonth,
            toDate: param.isClickLink ? toDate : this.nowString
        }
    }

    componentDidMount() {
        if (!this.state.countryList) {
            this.handleCountryList();
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param.isClickLink && param !== prevProps.param) {
            const { initialSelectedCountries, initialCountryList } = this.state;
            const newState = Object.assign(this.state, { deviceType: param.deviceType, fromDate: param.fromDate, toDate: param.toDate, selectedCountries: initialSelectedCountries, countryList: initialCountryList });

            this.setState(newState);
            this.handleSearch(newState);
        }
    }

    handleCountryList = async () => {
        const countryList = await this.props.getCountryList();
        const initialSelectedCountries = countryList.filter(r => r.Checked).map(r => r.CountryCode).join(',');

        this.setState((prevState) => {
            if (this.props.param.isClickLink) {
                this.handleSearch({ ...prevState, selectedCountries: initialSelectedCountries });
            }

            return { initialSelectedCountries, selectedCountries: initialSelectedCountries, countryList, initialCountryList: countryList };
        });
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

    handleSearch = (state) => {
        const { selectedCountries, deviceType, fromDate, toDate } = state;
        const param = Object.assign({}, { selectedCountries, deviceType, fromDate, toDate, affId: this.props.affId });

        this.props.setParam(param);
        this.props.setSelectedSiteDetailFilter(null);
    }

    getCountryList = (selectedCountries) => {
        const { initialSelectedCountries, countryList, initialCountryList } = this.state;

        if (selectedCountries === initialSelectedCountries) {
            return initialCountryList ? initialCountryList.slice() : [];
        }

        const selectedCountryCodes = selectedCountries.split(',');

        return countryList.map(r => {
            return Object.assign({}, r, { Checked: selectedCountryCodes.indexOf(r.CountryCode.toString()) !== -1 })
        })
    }

    render() {
        const { deviceType, fromDate, toDate, countryList, selectedCountries } = this.state;

        return countryList && (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-2">
                        <Label className="mr-sm-2">{headerText.Country}</Label>
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
                    <FormGroup className="mr-sm-2">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-2">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-2">
                        <Label for="deviceType" className="mr-sm-2">{headerText.Device}</Label>
                        <Input type="select" name="deviceType" id="deviceType" value={deviceType} onChange={this.handleChange}>
                            {
                                Object.keys(deviceTypes).map(key => {
                                    return (<option key={`deviceType-${deviceTypes[key]}`} value={deviceTypes[key]}>{key}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={() => this.handleSearch(this.state)}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, affCommonInfo, clickStatistics }) {
    const { affId } = affCommonInfo;
    const { param } = clickStatistics;

    return Object.assign({}, {
        today,
        affId,
        param
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedSiteDetailFilter, setParam, getCountryList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);