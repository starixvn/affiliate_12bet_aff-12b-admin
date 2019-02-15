import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import { deviceTypes, headerText } from '../modules/constants';
import { setDeviceType, setFromDate, setToDate, setSelectedSiteUrl, setParam } from '../actions';
import { globalResource } from '../../../../modules/globalResource';

const minDate = new Date(2009, 11, 1);

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const now = new Date(this.props.today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = now;
    }

    componentDidMount() {
        const toDate = new Date(this.props.toDate);

        if (toDate.getTime() > this.state.getTime()) {
            this.props.setToDate(this.nowString);
        }
    }

    handleChangeDevice = (event) => {
        this.props.setDeviceType(event.target.value);
    }

    handleChangeFromDate = (momentDate) => {
        const selectedDateString = momentDate.format("MM/DD/YYYY");
        const fromDate = new Date(selectedDateString);
        const toDate = new Date(this.props.toDate);

        const isOutsideRange = fromDate.getTime() > toDate.getTime();
        const newToDate = isOutsideRange ? selectedDateString : this.props.toDate;

        this.props.setFromDate(selectedDateString);
        this.props.setToDate(newToDate);
    }

    handleChangeToDate = (momentDate) => {
        this.props.setToDate(momentDate.format("MM/DD/YYYY"));
    }

    handleSearch = () => {
        const { deviceType, fromDate, toDate, param } = this.props;
        const newParam = Object.assign({}, param, { DeviceType: deviceType, FromDate: fromDate, ToDate: toDate });
        this.props.setParam(newParam);
        this.props.setSelectedSiteUrl('');
    }

    render() {
        const { deviceType, fromDate, toDate } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
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
                        <Label for="Affiliate" className="mr-sm-2">{headerText.Device}</Label>
                        <Input type="select" name="Device" id="Device" value={deviceType} onChange={this.handleChangeDevice}>
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

function mapStateToProps({ today, deviceType, fromDate, toDate, param }) {
    return Object.assign({}, { today, deviceType, fromDate, toDate, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setDeviceType, setFromDate, setToDate, setSelectedSiteUrl, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);