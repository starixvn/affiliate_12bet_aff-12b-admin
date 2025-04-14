import React from 'react';
import { Form, FormGroup, Label, Input, Col } from 'reactstrap';

import { renderInputForm } from '../../libs/service';

export const Personal = (props) => {

    const { personalInfo, setPersonalInfo } = props;

    const _handleChange = (e) => {
        setPersonalInfo({
            ...personalInfo,
            [e.target.name]: e.target.value
        });
    }

    const _handleChangeGender = (isMale) => {
        setPersonalInfo({
            ...personalInfo,
            gender: isMale
        });
    }

    const _renderGenderForm = (isMale) => {
        return (
            <FormGroup row>
                <Label sm={2}>Gender</Label>
                <Col sm={4} className="gender-group">
                    <FormGroup check className="d-inline-block mr-3">
                        <Label check>
                            <Input type="radio" checked={isMale !== null && isMale} onChange={() => _handleChangeGender(true)} />{' '} Male
                        </Label>
                    </FormGroup>
                    <FormGroup check className="d-inline-block">
                        <Label check>
                            <Input type="radio" checked={isMale !== null && !isMale} onChange={() => _handleChangeGender(false)} />{' '} Female
                        </Label>
                    </FormGroup>
                </Col>
            </FormGroup>
        )
    }

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">Personal Information</h6></legend>
            <Form>
                {renderInputForm('firstName', 'First Name', personalInfo.firstName, false, false, _handleChange)}
                {renderInputForm('lastName', 'Last Name', personalInfo.lastName, false, false, _handleChange)}
                {_renderGenderForm(personalInfo.gender)}
                {renderInputForm('email', 'Email Address', personalInfo.email, false, false, _handleChange)}
                {renderInputForm('phone', 'Contact Number', personalInfo.phone, false, false, _handleChange)}
            </Form>
        </fieldset>
    );
};

export default React.memo(Personal);