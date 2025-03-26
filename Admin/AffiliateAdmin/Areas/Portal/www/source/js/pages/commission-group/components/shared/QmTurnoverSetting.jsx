import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../actions';

import { Form, FormGroup, Label, Input } from 'reactstrap';
import { format } from '../../../../modules/numberFormat';
import { isValidNumber } from '../../../../modules/utils';
import { money } from '../../../../modules/constants';

export class QmTurnoverSetting extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        const textValue = value.replace(/,/g, '');
        const numberValue = Number(textValue);
        const isValid = isValidNumber(numberValue);

        if (isValid && numberValue <= money.AllowedMaxValue) {
            this.props.setSelectedGroup({ [name]: numberValue });
        }
    }

    render() {
        const { QmTurnover } = this.props.selectedGroup;

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">QP Turnover</h6></legend>
                <p>A member is considered a Qualified Player (QP) when his/her turnover in a month is equivalent to</p>
                <Form inline>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="QmTurnover" className="mr-sm-2">USD</Label>
                        <Input name="QmTurnover" id="QmTurnover" value={format(QmTurnover)}
                            onChange={this.handleChange} />
                    </FormGroup>
                </Form>
            </fieldset>
        )
    }
}

function mapStateToProp({ selectedGroup }) {
    return { selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(QmTurnoverSetting);