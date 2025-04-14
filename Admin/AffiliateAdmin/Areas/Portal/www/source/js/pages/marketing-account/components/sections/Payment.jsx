import React from 'react';
import { Form, FormGroup, Input, Label, Col } from 'reactstrap';

import { playerMethod, moneybookersMethod, wireMethod, minimumPaymentText } from '../../../user-panel/user-account/modules/constants';
import { formatDecimal } from '../../../../modules/numberFormat';

export const Payment = (props) => {

    const { paymentInfo, setPaymentInfo } = props;

    const _handleChange = (e) => {
        setPaymentInfo({
            ...paymentInfo,
            [e.target.name]: e.target.value
        });
    }

    const _renderFormByMethod = (paymentInfo) => {
        return {
            [playerMethod]: _renderPaymentForm('playerUsername', '12B User Name', paymentInfo.playerUsername, false, _handleChange),
            [moneybookersMethod]: _renderPaymentForm('moneybookersEmail', 'Moneybookers Email', paymentInfo.moneybookersEmail, false, _handleChange),
            [wireMethod]: _renderWireForm(paymentInfo)
        }
    }

    const _renderPaymentForm = (name, label, inputValue, isRequired, changeHandler) => {
        return (
            <FormGroup row>
                <Label for={name} sm={2} className={isRequired ? 'required' : ''}>{label}</Label>
                <Col sm={6} className="input-item">
                    <Input type="text" id={name} name={name} value={inputValue || ''} onChange={changeHandler} />
                </Col>
            </FormGroup>
        )
    }

    const _renderWireForm = (paymentInfo) => {
        return (
            <React.Fragment>
                {_renderPaymentForm('bankName', 'Wire Name', paymentInfo.bankName, false, _handleChange)}
                {_renderPaymentForm('bankAddress', 'Wire Address', paymentInfo.bankAddress, false, _handleChange)}
                {_renderPaymentForm('bankCode', 'Wire ABA/Swift/Sort Code', paymentInfo.bankCode, false, _handleChange)}
                {_renderPaymentForm('bankAccountName', 'Wire Account Name', paymentInfo.bankAccountName, false, _handleChange)}
                {_renderPaymentForm('bankAccountNumber', 'Wire Account Number', paymentInfo.bankAccountNumber, false, _handleChange)}
            </React.Fragment>
        )
    }

    const renderFormByMethod = paymentInfo && _renderFormByMethod(paymentInfo);

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">Payment Information</h6></legend>
            <Form tag="div">
                <FormGroup row>
                    <Label htmlFor="preferredPayment" sm={2}>Preferred Payment Method</Label>
                    <Col sm={6} className="input-item">
                        <Input type="select" id="preferredPayment" name="preferredPayment" onChange={_handleChange} value={paymentInfo.preferredPayment}>
                            {props.paymentMethodList && props.paymentMethodList.map(item => {
                                return (<option key={item.Key} value={item.Key}>{item.Value}</option>)
                            })}
                        </Input>
                    </Col>
                    <span className="input-item">{minimumPaymentText + ' ' + formatDecimal(props.minimumPayment)}</span>
                </FormGroup>
                {renderFormByMethod[paymentInfo.preferredPayment]}
            </Form>
        </fieldset>
    );
}

export default React.memo(Payment);