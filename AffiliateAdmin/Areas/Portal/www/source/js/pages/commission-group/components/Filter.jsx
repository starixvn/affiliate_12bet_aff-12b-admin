import React, { Component } from "react";
import { connect } from "react-redux";

import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../actions';

import { Button, Form, FormGroup, Input, Label } from 'reactstrap';

class Filter extends Component {
    constructor(props) {
        super(props);
    }

    handleClick = (event) => {
        event.preventDefault();
        console.log("submit");
    }

    render() {
        return (
            <div className="filter-container">
                <Form inline onSubmit={this.handleClick} style={{ backgroundColor: 'transparent' }}>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">Group Name</Label>{' '}
                        <Input type="text" className="mr-sm-2 search-box" />
                    </FormGroup>
                    <Button outline color="primary" type="button"><span className="oi oi-magnifying-glass"></span> Search</Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ gridData }) {
    return Object.assign({}, { gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);
