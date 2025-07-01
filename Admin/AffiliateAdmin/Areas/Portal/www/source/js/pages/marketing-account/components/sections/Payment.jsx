import React from 'react';
import { Form, FormGroup, Input, Label, Col } from 'reactstrap';
import { minimumPaymentText } from '../../../user-panel/user-account/modules/constants';
import { formatDecimal } from '../../../../modules/numberFormat';
import RowField from './RowField';

export const Payment = (props) => {
    const { paymentInfo, setPaymentInfo, paymentMethods } = props;
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
                    <Label htmlFor="preferredPayment" sm={3}>Preferred Payment Method</Label>
                    <Col sm={5} className="input-item">
                        <Input 
                            type="select" 
                            id="preferredPayment" 
                            name="preferredPayment" 
                            onChange={onChange} 
                            value={paymentInfo.preferredPayment}
                            disabled={!paymentMethods.length}
                        >
                            {paymentMethods && paymentMethods.map(item => {
                                return (<option key={item.Key} value={item.Key}>{item.Value}</option>)
                            })}
                        </Input>
                    </Col>
                    <span className="input-item">{minimumPaymentText + ' ' + formatDecimal(props.minimumPayment)}</span>
                </FormGroup>
                <RowField 
                    label="12Bet UserName"
                    labelProps={{ sm: 3 }}
                    name="playerUsername"
                    value={paymentInfo.playerUsername} 
                    onChange={onChange}
                />
            </Form>
        </fieldset>
    );
}

export default React.memo(Payment);