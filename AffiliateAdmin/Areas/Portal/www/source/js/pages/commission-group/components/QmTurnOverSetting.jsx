import React from 'react';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

const QmTurnOverSetting = (props) => {
    const handleChange = (event) => {
        this.props.handleChange(Number(event.target.value));
    }

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">QM Turnover</h6></legend>
            <p>A member is considered a Qualified Member (QM) when his/her turnover in a month is equivalent to</p>
            <Form inline>
                <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                    <Label for="QmTurnover" className="mr-sm-2">USD</Label>
                    <Input name="QmTurnover" id="QmTurnover" defaultValue="" onChange={handleChange} />
                </FormGroup>
            </Form>
        </fieldset>
    )
}

export default QmTurnOverSetting;