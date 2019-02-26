import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import { pageList, deviceTypes, headerTitle } from '../modules/constants';
import { setShowDetail, setParam } from '../actions';
import { globalResource } from '../../../../modules/globalResource';

const minDate = new Date(2009, 11, 1);

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const now = new Date(this.props.today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = {
            page: '',
            language: '',
            fromDate: `${now.getMonth() + 1}/01/${now.getFullYear()}`,
            toDate: this.nowString,
            deviceType: 0
        };
    }

    componentDidMount() {
        this.handleSetParam();
    }

    handleSetParam() {
        const { page, language, fromDate, toDate, deviceType } = this.state
        const newParam = Object.assign({}, this.props.param, { Page: page, Language: language, FromDate: fromDate, ToDate: toDate, DeviceType: deviceType });
        this.props.setParam(newParam);
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handleChangeFromDate = (momentDate) => {
        const selectedDateString = momentDate.format("MM/DD/YYYY");
        const fromDate = new Date(selectedDateString);
        const toDate = new Date(this.state.toDate);

        const isOutsideRange = fromDate.getTime() > toDate.getTime();
        const newToDate = isOutsideRange ? selectedDateString : this.state.toDate;

        this.setState({ fromDate: selectedDateString, toDate: newToDate });
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ toDate: momentDate.format("MM/DD/YYYY") });
    }

    handleSearch = () => {
        this.handleSetParam();
        this.props.setShowDetail(false);
    }

    render() {
        const { page, language, deviceType, fromDate, toDate } = this.state;
        const { languageList } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="page" className="mr-sm-2">{headerTitle.Page}</Label>
                        <Input type="select" name="page" id="page" value={page} onChange={this.handleChange}>
                            <option value="">{globalResource.All}</option>
                            {
                                pageList.map(index => {
                                    return (<option key={`page-${index}`} value={index}>{index}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="language" className="mr-sm-2">{headerTitle.Language}</Label>
                        <Input type="select" name="language" id="language" value={language} onChange={this.handleChange}>
                            <option value="">{globalResource.All}</option>
                            {
                                languageList.map(language => {
                                    return (<option key={`language-${language.LanguageCode}`} value={language.LanguageName}>{language.LanguageName}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="deviceType" className="mr-sm-2">{headerTitle.Device}</Label>
                        <Input type="select" name="deviceType" id="deviceType" value={deviceType} onChange={this.handleChange}>
                            {
                                Object.keys(deviceTypes).map(index => {
                                    return (<option key={`deviceType-${index}`} value={index}>{deviceTypes[index]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, param, languageList }) {
    return Object.assign({}, { today, param, languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowDetail, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);