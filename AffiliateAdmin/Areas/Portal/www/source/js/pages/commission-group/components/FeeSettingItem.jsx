import React from 'react';

import { Col, FormGroup, Label, Input } from 'reactstrap';
import { formatDecimal } from '../../../modules/numberFormat';
import { commissionValueList } from '../modules/constant';

export default class FeeSettingItem extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { label, selectedComm, basedValueName, onChange } = this.props;
        return (
            <FormGroup row>
                <Label sm={4} className="font-weight-bold">{label}</Label>

                <Col sm={{ size: 2, offset: 1 }}>
                    <select className="form-control" onChange={onChange}
                        defaultValue={formatDecimal(selectedComm)}>
                        {
                            commissionValueList.map((item, i) => {
                                return (<option key={`feecomm-${i}`} value={item}>{item}</option>);
                            })
                        }
                    </select>
                </Col>
                <Col sm={1}>
                    <Input plaintext>x</Input>
                </Col>
                <Col sm={3}>
                    <Input plaintext>{basedValueName}</Input>
                </Col>
            </FormGroup>
        )
    }
}