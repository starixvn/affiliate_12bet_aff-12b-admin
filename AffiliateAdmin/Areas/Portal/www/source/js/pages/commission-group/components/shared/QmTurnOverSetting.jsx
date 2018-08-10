import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../actions';

import { Form, FormGroup, Label, Input } from 'reactstrap';
import { format } from '../../../../modules/numberFormat';
import { isValidNumber } from '../../../../modules/utils';

export class QmTurnOverSetting extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChange = (event) => {
        const value = event.target.value.replace(/,/g, '');
        const isValid = isValidNumber(value);
        const QmTurnover = Number(value);

        if (isValid) {
            this.props.setSelectedGroup({ QmTurnover });
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
                        <Input name="QmTurnover" id="QmTurnover" value={format(this.props.selectedGroup.QmTurnover)}
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

export default connect(mapStateToProp, mapDispatchToProps)(QmTurnOverSetting);