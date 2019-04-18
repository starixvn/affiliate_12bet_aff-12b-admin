import React, { Component } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

export class Additional extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="Address" sm={3}>Address</Label>
                    <Col sm={9}>
                        <Input type="text" id="Address" name="Address" value="Address" />
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}