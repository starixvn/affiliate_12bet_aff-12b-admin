import React, { Component } from "react";
import { connect } from "react-redux";

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
                    <FormGroup>
                        <Label>Group Name</Label>{' '}
                        <Input type="text" className="search-box" />
                    </FormGroup>{' '}
                    <Button outline type="submit" size="sm">Search</Button>
                </Form>
            </div>
        );
    }
}

export default connect()(Filter);
