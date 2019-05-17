import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import { headerTitle } from '../modules/constants';
import { setSelectedPage, setParam, setSelectedLanguage } from '../actions';
import { globalResource } from '../../../../modules/globalResource';
import { deviceTypes } from '../../../customer/user-panel/modules/constants';

const minDate = new Date(2009, 11, 1);

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const now = new Date(this.props.today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = {
            selectedPage: '',
            languageCode: '',
            fromDate: `${now.getMonth() + 1}/01/${now.getFullYear()}`,
            toDate: this.nowString,
            deviceType: 0
        };
    }

    componentDidMount() {
        this.handleSetParam();
    }

    handleSetParam() {
        const { selectedPage, languageCode, fromDate, toDate, deviceType } = this.state
        const newParam = Object.assign({}, this.props.param, { SelectedPage: selectedPage, LanguageCode: languageCode, FromDate: fromDate, ToDate: toDate, DeviceType: deviceType });
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
        this.props.setSelectedPage(null);
    }

    render() {
        const { selectedPage, languageCode, deviceType, fromDate, toDate } = this.state;
        const { languageList, pageList } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="selectedPage" className="mr-sm-2">{headerTitle.Page}</Label>
                        <Input type="select" name="selectedPage" id="selectedPage" value={selectedPage} onChange={this.handleChange}>
                            <option value="">{globalResource.All}</option>
                            {
                                pageList.map(page => {
                                    return (<option key={`page-${page.PageCode}`} value={page.PageCode}>{page.PageName}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="languageCode" className="mr-sm-2">{headerTitle.Language}</Label>
                        <Input type="select" name="languageCode" id="languageCode" value={languageCode} onChange={this.handleChange}>
                            <option value="">{globalResource.All}</option>
                            {
                                Object.keys(languageList).map(languageCode => {
                                    return (<option key={`language-${languageCode}`} value={languageCode}>{languageList[languageCode]}</option>)
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
                                Object.keys(deviceTypes).map(key => {
                                    return (<option key={`deviceType-${deviceTypes[key]}`} value={deviceTypes[key]}>{key}</option>)
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

function mapStateToProps({ userPanel }) {
    const { today, param, languageList, pageList } = userPanel;
    return Object.assign({}, { today, param, languageList, pageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedPage, setParam, setSelectedLanguage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);