import React from 'react';

import { Form, FormGroup, Label, Input } from 'reactstrap';

export default class QmTurnOverSetting extends React.Component {
    constructor(props) {
        super(props)
    }

    handleChange = (event) => {
        this.props.onChange(Number(event.target.value));
    }

    render() {
        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">QM Turnover</h6></legend>
                <p>A member is considered a Qualified Member (QM) when his/her turnover in a month is equivalent to</p>
                <Form inline>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="QmTurnover" className="mr-sm-2">USD</Label>
                        <Input name="QmTurnover" id="QmTurnover" defaultValue="" onChange={this.handleChange} />
                    </FormGroup>
                </Form>
            </fieldset>
        )
    }
}