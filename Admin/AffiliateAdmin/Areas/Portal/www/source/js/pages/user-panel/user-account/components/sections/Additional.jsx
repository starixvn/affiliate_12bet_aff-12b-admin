import React, { PureComponent } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';
import { renderInputForm } from '../../modules/service';

export class Additional extends PureComponent {
    constructor(props) {
        super(props);
    }

    _renderCountry() {
        return (
            <FormGroup row>
                <Label for="country" sm={3} xl={2}>Country</Label>
                <Col sm={9} xl={10}>
                    <Input type="select" id="country" name="country">
                        <option>1</option>
                        <option>2</option>
                        <option>3</option>
                        <option>4</option>
                        <option>5</option>
                    </Input>
                </Col>
            </FormGroup>
        )
    }

    render() {
        return (
            <Form tag="div">
                {renderInputForm('address', 'Address', 'Address Value')}
                {renderInputForm('city', 'City', 'City Value')}
                {renderInputForm('state', 'State', 'State Value')}
                {renderInputForm('postCode', 'Postcode/Zip', 'Postcode/Zip Value')}
                {this._renderCountry()}
                {renderInputForm('securityQuestion', 'Security Question', 'What?', true)}
                {renderInputForm('answer', 'Answer', 'What?', true)}
            </Form>
        )
    }
}