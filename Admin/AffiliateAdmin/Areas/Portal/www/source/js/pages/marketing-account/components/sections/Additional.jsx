import React from 'react';
import { Form } from 'reactstrap';
import { countriesList } from '../../../user-panel/user-account/modules/constants';
import RowField from './RowField';

export const Additional = (props) => {
    const { additionalInfo, setAdditionalInfo } = props;

    const contriesOption = Object.keys(countriesList).map((key) => {
        return {
            label: countriesList[key],
            value: key
        }
    })

    const onChange = (e) => {
        setAdditionalInfo({
            ...additionalInfo,
            [e.target.name]: e.target.value
        });
    }

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">Additional Information</h6></legend>
            <Form>
                <RowField
                    label="Address"
                    name="address"
                    value={additionalInfo.address}
                    onChange={onChange}
                />
                <RowField
                    label="City"
                    name="city"
                    value={additionalInfo.city}
                    onChange={onChange}
                />
                <RowField
                    label="State"
                    name="state"
                    value={additionalInfo.state}
                    onChange={onChange}
                />
                <RowField
                    label="Postcode/Zip"
                    name="postal"
                    value={additionalInfo.postal}
                    onChange={onChange}
                />
                <RowField
                    label="Country"
                    name="country"
                    value={additionalInfo.country}
                    onChange={onChange}
                    selectOptions={contriesOption}
                />
                <RowField
                    label="Security Question"
                    name="question"
                    value={additionalInfo.question}
                    onChange={onChange}
                    colComponent={
                        <p className="form-control-plaintext">{additionalInfo.question}</p>
                    }
                />
                <RowField
                    label="Answer"
                    name="answer"
                    value={additionalInfo.answer}
                    onChange={onChange}
                    colComponent={
                        <p className="form-control-plaintext">{additionalInfo.answer}</p>
                    }
                />
                
            </Form>
        </fieldset>
    );
}

export default React.memo(Additional);
