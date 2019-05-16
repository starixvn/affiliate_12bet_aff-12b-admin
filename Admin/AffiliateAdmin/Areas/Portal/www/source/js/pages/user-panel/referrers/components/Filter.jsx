import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import { deviceTypes, headerText } from '../modules/constants';
import { setSelectedSiteUrl, setParam } from '../actions';
import { globalResource } from '../../../../modules/globalResource';

const minDate = new Date(2009, 11, 1);

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today, param } = this.props;
        const { deviceType, fromDate, toDate } = param;

        const now = new Date(today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;
        const firstDayOfCurrentMonth = `${now.getMonth() + 1}/01/${now.getFullYear()}`;

        this.state = {
            deviceType: deviceType || 0,
            fromDate: fromDate || firstDayOfCurrentMonth,
            toDate: toDate || this.nowString
        }
    }

    componentDidMount() {
        this.handleSearch();
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.setState({ deviceType: param.deviceType, fromDate: param.fromDate, toDate: param.toDate });
            this.props.setSelectedSiteUrl(null);
        }
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

        this.setState({
            fromDate: selectedDateString,
            toDate: newToDate
        });
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ toDate: momentDate.format("MM/DD/YYYY") });
    }

    handleSearch = () => {
        this.props.setParam(this.state);
        this.props.setSelectedSiteUrl(null);
    }

    render() {
        const { deviceType, fromDate, toDate } = this.state;

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
                        <Label for="deviceType" className="mr-sm-2">{headerText.Device}</Label>
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

function mapStateToProps({ today, userPanel }) {
    const { param } = userPanel;
    return Object.assign({}, {
        today,
        param
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedSiteUrl, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);