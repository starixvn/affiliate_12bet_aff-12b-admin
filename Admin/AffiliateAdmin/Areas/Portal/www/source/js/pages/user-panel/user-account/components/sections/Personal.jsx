import React, { PureComponent } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

import { renderInputForm } from '../../modules/service';

export class Personal extends PureComponent {
    constructor(props) {
        super(props);
    }

    _renderGenderForm() {
        return (
            <FormGroup row>
                <Label for={name} sm={3} xl={2}>Gender</Label>
                <Col sm={9} xl={10}>
                    <FormGroup check className="d-inline-block mr-3">
                        <Label check>
                            <Input type="radio" name="gender" />{' '} Male
                            </Label>
                    </FormGroup>
                    <FormGroup check className="d-inline-block">
                        <Label check>
                            <Input type="radio" name="gender" />{' '} Female
                            </Label>
                    </FormGroup>
                </Col>
            </FormGroup>
        )
    }

    render() {
        return (
            <Form tag="div">
                {renderInputForm('username', 'User Name', 'Some plain text/ static value', true)}
                {renderInputForm('firstName', 'First Name', 'firstName')}
                {renderInputForm('lastName', 'Last Name', 'lastName')}
                {this._renderGenderForm()}
                {renderInputForm('email', 'Email Address', 'aaa@yahoo.com')}
                {renderInputForm('contactNumber', 'Contact Number', '123769')}
                {renderInputForm('contactApp', 'MSN/YM/QQ', 'Skype')}
            </Form>
        )
    }
}