import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import { memberTypes } from '../modules/constants';
import { setParam } from '../actions';

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        this.now = new Date(this.props.currentDate);
    }

    render() {
        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="Affiliate" className="mr-sm-2">Member</Label>
                        <Input type="select" name="MemberType" id="MemberType">
                            <option key="0" value="0">Select</option>
                            {
                                Object.keys(memberTypes).map(k => {
                                    return (<option key={`memberType-${k}`}>{memberTypes[k]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">From</Label>
                        <DatePicker minDate={new Date(2009, 11, 1)} shownDate={this.now}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">To</Label>
                        <DatePicker minDate={new Date(2009, 11, 1)} shownDate={this.now}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <FormGroup check>
                            <Label check>
                                Get All
                            <Input className="ml-1 mr-2" type="checkbox" />
                            </Label>
                        </FormGroup>
                    </FormGroup>
                    <Button outline color="primary" type="button">
                        <span className="oi oi-magnifying-glass"></span> Search
                </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ currentDate }) {
    return Object.assign({}, { currentDate });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);