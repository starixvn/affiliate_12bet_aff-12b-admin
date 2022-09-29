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
            initialCountries: '',
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

        if (this.props.param.isClickLink) {
            this.handleSearch(this.state);
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            const newState = Object.assign({}, { deviceType: param.deviceType, fromDate: param.fromDate, toDate: param.toDate });

            this.setState(newState);
            this.props.setSelectedSiteDetailFilter(null);

            if (param.isClickLink) {
                this.handleSearch(newState);
            }
        }
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

    handleSearch = (state) => {
        this.props.setParam(state);
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

function mapStateToProps({ today, clickStatistics }) {
    const { param } = clickStatistics;

    return Object.assign({}, {
        today,
        param
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedSiteDetailFilter, setParam, getCountryList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);