import React, { Component } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

export class EmailFormat extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form tag="div">
                <FormGroup check>
                    <Label check>
                        <Input type="radio" name="radio1" />{' '}
                        Male
                    </Label>
                </FormGroup>
                <FormGroup check>
                    <Label check>
                        <Input type="radio" name="radio2" />{' '}
                        Female
                    </Label>
                </FormGroup>
            </Form>
        )
    }
}