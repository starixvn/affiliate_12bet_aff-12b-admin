import React from 'react';

import { Form, FormGroup, Label, Input } from 'reactstrap';
import { format } from '../../../modules/numberFormat';
import { isValidNumber } from '../../../modules/utils';

export default class QmTurnOverSetting extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            qmTurnover: 0
        }
    }

    handleChange = (event) => {
        const value = event.target.value.replace(/,/g, '');
        const isValid = isValidNumber(value);
        const qmTurnover = Number(value);

        if (isValid) {
            this.setState({ qmTurnover });
            this.props.onChange(qmTurnover);
        }
    }

    render() {
        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">QM Turnover</h6></legend>
                <p>A member is considered a Qualified Member (QM) when his/her turnover in a month is equivalent to</p>
                <Form inline>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="QmTurnover" className="mr-sm-2">USD</Label>
                        <Input name="QmTurnover" id="QmTurnover" value={format(this.state.qmTurnover)} onChange={this.handleChange} />
                    </FormGroup>
                </Form>
            </fieldset>
        )
    }
}