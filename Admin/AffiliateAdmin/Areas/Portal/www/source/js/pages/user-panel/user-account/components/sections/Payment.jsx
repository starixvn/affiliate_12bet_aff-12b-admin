import React, { Component } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

export class Payment extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="PaymentMethod" sm={3}>Preferred Payment Method</Label>
                    <Col sm={9}>
                        <Input type="select" id="PaymentMethod" name="PaymentMethod">
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                            <option>4</option>
                            <option>5</option>
                        </Input>
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}