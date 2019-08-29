import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { DatePicker } from '@9softstudio/react-datepicker';
import { Form, FormGroup, Label, Button } from 'reactstrap';

import { resource, minDate } from '../lib/constants';
import { setSelectedCurrency, setParam } from '../actions/index';

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

        const param = Object.assign({}, { date });

        this.props.setParam(param);
    }

    render() {
        const { date } = this.state;

        return (
            <div className="filter-container">
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
    return bindActionCreators({ setSelectedCurrency, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);