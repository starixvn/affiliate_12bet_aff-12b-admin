import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import { globalResource } from '../modules/constants';
import { setParam } from '../actions';

const minDate = new Date(2009, 11, 1);

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const now = new Date(today);

        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        const sixMonthAgo = new Date(today);
        sixMonthAgo.setMonth(sixMonthAgo.getMonth() - 6);

        this.state = {
            isGetAll: false,
            fromDate: `${sixMonthAgo.getMonth() + 1}/${sixMonthAgo.getDate()}/${sixMonthAgo.getFullYear()}`,
            toDate: this.nowString,
        }
    }

    componentDidMount() {
        this.handleSetParam();
    }

    handleSetParam() {
        const { isGetAll, fromDate, toDate } = this.state;
        const newParam = Object.assign({}, this.props.param, { IsGetAll: isGetAll,
            FromDate: isGetAll ? `${minDate.getMonth() + 1}/${minDate.getDate()}/${minDate.getFullYear()}` : fromDate,
            ToDate: isGetAll ? this.nowString: toDate });
        this.props.setParam(newParam);
    }

    handleChecked = (event) => {
        this.setState({ [event.target.name]: event.target.checked });
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
        const { isGetAll, fromDate, toDate } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate} isEnable={!isGetAll}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate} isEnable={!isGetAll}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <FormGroup check>
                            <Label check>
                                {globalResource.GetAll}
                                <Input name="isGetAll" className="ml-1 mr-2" type="checkbox" checked={isGetAll} onChange={this.handleChecked} />
                            </Label>
                        </FormGroup>
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