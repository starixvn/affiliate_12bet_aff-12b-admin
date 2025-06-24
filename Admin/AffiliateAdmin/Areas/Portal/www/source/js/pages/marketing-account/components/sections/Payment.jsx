import React from 'react';
import { Form, FormGroup, Input, Label, Col } from 'reactstrap';
import { minimumPaymentText } from '../../../user-panel/user-account/modules/constants';
import { formatDecimal } from '../../../../modules/numberFormat';

export const Payment = (props) => {
    const { paymentInfo, setPaymentInfo } = props;

    const onChange = (e) => {
        setPaymentInfo({
            ...paymentInfo,
            [e.target.name]: e.target.value
        });
    }

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">Payment Information</h6></legend>
            <Form tag="div">
                <FormGroup row>
                    <Label htmlFor="preferredPayment" sm={2}>Preferred Payment Method</Label>
                    <Col sm={6} className="input-item">
                        <Input type="select" id="preferredPayment" name="preferredPayment" onChange={onChange} value={paymentInfo.preferredPayment}>
                            <option value={paymentInfo.preferredPayment}>{paymentInfo.preferredPayment}</option>
                        </Input>
                    </Col>
                    <span className="input-item">{minimumPaymentText + ' ' + formatDecimal(props.minimumPayment)}</span>
                </FormGroup>
            </Form>
        </fieldset>
    );
}

export default React.memo(Payment);