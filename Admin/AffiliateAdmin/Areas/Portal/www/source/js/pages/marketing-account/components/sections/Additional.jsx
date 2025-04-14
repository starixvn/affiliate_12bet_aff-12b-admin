import React from 'react';
import { Form, FormGroup, Label, Input, Col } from 'reactstrap';

import { renderInputForm } from '../../libs/service';
import { countriesList } from '../../../user-panel/user-account/modules/constants';

export const Additional = (props) => {

    const { additionalInfo, setAdditionalInfo } = props;

    const _handleChange = (e) => {
        setAdditionalInfo({
            ...additionalInfo,
            [e.target.name]: e.target.value
        });
    }

    const _renderCountry = (country) => {
        return (
            <FormGroup row>
                <Label htmlFor="country" sm={2}>Country</Label>
                <Col sm={6}>
                    <Input type="select" id="country" name="country" value={country} onChange={_handleChange}>
                        <option value="">Select One</option>
                        {
                            Object.keys(countriesList).map((key) => {
                                return (<option key={'country-' + key} value={key}>{countriesList[key]}</option>)
                            })
                        }
                    </Input>
                </Col>
            </FormGroup>
        )
    }

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">Additional Information</h6></legend>
            <Form>
                {renderInputForm('address', 'Address', additionalInfo.address, false, false, _handleChange)}
                {renderInputForm('city', 'City', additionalInfo.city, false, false, _handleChange)}
                {renderInputForm('state', 'State', additionalInfo.state, false, false, _handleChange)}
                {renderInputForm('postcode', 'Postcode/Zip', additionalInfo.postcode, false, false, _handleChange)}
                {_renderCountry(additionalInfo.country)}
                {renderInputForm('question', 'Security Question', additionalInfo.question, true, false)}
                {renderInputForm('answer', 'Answer', additionalInfo.answer, true, false)}
            </Form>
        </fieldset>
    );
}

export default React.memo(Additional);
