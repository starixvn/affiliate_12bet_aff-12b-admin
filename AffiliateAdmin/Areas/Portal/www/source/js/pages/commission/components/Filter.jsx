import React, { Component } from "react";
import { connect } from "react-redux";

import { Button, Form, FormGroup, FormControl, ControlLabel } from 'react-bootstrap';

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
                    <FormGroup>
                        <ControlLabel>Group Name</ControlLabel>{' '}
                        <FormControl type="text" bsClass="search-box" />
                    </FormGroup>{' '}
                    <Button type="submit"><span id="divsearch"></span></Button>
                </Form>
            </div>
        );
    }
}

export default connect()(Filter);
