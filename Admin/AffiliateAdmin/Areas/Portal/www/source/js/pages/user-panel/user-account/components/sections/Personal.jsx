import React, { Component } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

export class Personal extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="Username" sm={3}>User Name</Label>
                    <Col sm={9}>
                        <Input plaintext value="User Name" />
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}