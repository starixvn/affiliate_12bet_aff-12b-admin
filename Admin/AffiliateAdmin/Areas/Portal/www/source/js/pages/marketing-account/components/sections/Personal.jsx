import React from 'react';
import { Form, FormGroup, Label, Input, Col } from 'reactstrap';
import RowField from './RowField';

export const Personal = (props) => {
    const { personalInfo, setPersonalInfo } = props;
    const onChange = (e) => {
        setPersonalInfo({
            ...personalInfo,
            [e.target.name]: e.target.value
        });
    }

    const _renderGenderForm = () => {
        return (
            <FormGroup row>
                <Label sm={2}>Gender</Label>
                <Col sm={4} className="gender-group">
                    <FormGroup check className="d-inline-block mr-3">
                        <Label check>
                            <Input name='gender' type="radio" checked={!personalInfo.gender} onChange={onChange} value={0}/>{' '} Male
                        </Label>
                    </FormGroup>
                    <FormGroup check className="d-inline-block">
                        <Label check>
                            <Input name='gender' type="radio" checked={personalInfo.gender} onChange={onChange} value={1}/>{' '} Female
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
            
                <RowField
                    label="First Name"
                    name="firstName"
                    value={personalInfo.firstName}
                    onChange={onChange}
                />
                <RowField
                    label="Last Name"
                    name="lastName"
                    value={personalInfo.lastName}
                    onChange={onChange}
                />
                {_renderGenderForm()}
                <RowField
                    label="Email Address"
                    name="email"
                    value={personalInfo.email}
                    onChange={onChange}
                />
                <RowField
                    label="Contact Number"
                    name="phone"
                    value={personalInfo.phone}
                    onChange={onChange}
                />
            </Form>
        </fieldset>
    );
};

export default React.memo(Personal);