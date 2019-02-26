import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import { logTypes, headerText } from '../modules/constants';
import { setSelectedSiteUrl, setParam } from '../actions';
import { globalResource } from '../../../../modules/globalResource';

const minDate = new Date(2009, 11, 1);

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const { today, param } = this.props;

        const now = new Date(today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;
        now.setMonth(now.getMonth() - 6);

        this.state = {
            logType: 0,
            fromDate: `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`,
            toDate: this.nowString,
        }
    }

    componentDidMount() {
        this.handleSetParam();
    }

    handleSetParam() {
        const { logType, fromDate, toDate } = this.state;
        const newParam = Object.assign({}, this.props.param, { LogType: logType, FromDate: fromDate, ToDate: toDate });
        this.props.setParam(newParam);
    }

    handleChangeLogType = (event) => {
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
        this.handleSetParam();
    }

    render() {
        const { logType, fromDate, toDate } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="logType" className="mr-sm-2">{headerText.Type}</Label>
                        <Input type="select" name="logType" id="logType" value={logType} onChange={this.handleChangeLogType}>
                            <option value="0">{globalResource.All}</option>
                            {
                                Object.keys(logTypes).map(index => {
                                    return (<option key={`logType-${logTypes[index]}`} value={logTypes[index]}>{index}</option>)
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
                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, param }) {
    return Object.assign({}, { today, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);