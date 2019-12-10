import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { DatePicker } from '@9softstudio/react-datepicker';
import { Form, FormGroup, Label, Button } from 'reactstrap';

import { resource, minDate } from '../libs/constants';
import { setDate, fetchData } from '../actions/index';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const now = new Date(today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = {
            date: this.nowString
        }
    }

    _handleChangeDate = (momentDate) => {
        this.setState({ date: momentDate.format("MM/DD/YYYY") });
    }

    _handleSearch = () => {
        const { date } = this.state;

        this.props.setDate(date);
        this.props.fetchData(date);
    }

    render() {
        const { date } = this.state;

        return (
            <div className="filter-container mt-2">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-3">
                        <Label className="mr-2">{resource.Date}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={date}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this._handleChangeDate} />
                    </FormGroup>

                    <Button outline color="primary" type="button" onClick={this._handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {resource.Search}
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ today }) {
    return Object.assign({}, { today });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setDate, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);