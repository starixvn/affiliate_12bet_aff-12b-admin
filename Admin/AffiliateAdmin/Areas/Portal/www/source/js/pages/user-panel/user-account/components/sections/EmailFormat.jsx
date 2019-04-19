import React, { PureComponent } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

export class EmailFormat extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form tag="div">
                <FormGroup row>
                    <Col sm={5}>
                        <FormGroup check className="d-inline-block mr-3">
                            <Label check>
                                <Input type="radio" name="emailFormat" />{' '} Plain Text Format
                            </Label>
                        </FormGroup>
                        <FormGroup check className="d-inline-block">
                            <Label check>
                                <Input type="radio" name="emailFormat" />{' '} Html Format
                            </Label>
                        </FormGroup>
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}