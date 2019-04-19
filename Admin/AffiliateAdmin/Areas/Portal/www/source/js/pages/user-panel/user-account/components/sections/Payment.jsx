import React, { PureComponent } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

const playerMethod = 'Player';
const netellerMethod = 'Neteller';
const moneybookersMethod = 'Moneybookers';
const wireMethod = 'Wire';

const paymentMethodList = [
    { key: playerMethod, value: '12BET player account' },
    { key: netellerMethod, value: 'Neteller' },
    { key: moneybookersMethod, value: 'Moneybookers' },
    { key: wireMethod, value: 'Bank Transfer' }
]

export class Payment extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            selectedPaymentMethod: playerMethod
        }

        this.formByMethod = {
            [playerMethod]: this._renderPaymentForm('playerUsername', '12BET User Name', 'qatest01'),
            [netellerMethod]: this._renderPaymentForm('neteller', 'Neteller Account ID', 'Neteller value'),
            [moneybookersMethod]: this._renderPaymentForm('moneybookers', 'Moneybookers Email', 'Moneybookers value'),
            [wireMethod]: this._renderWireForm()
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    _renderPaymentForm(name, label, inputValue, changeHandler) {
        return (
            <FormGroup row>
                <Label for={name} sm={3} xl={2}>{label}</Label>
                <Col sm={5} xl={6}>
                    <Input type="text" id={name} name={name} value={inputValue} onChange={changeHandler} />
                </Col>
            </FormGroup>
        )
    }

    _renderWireForm() {
        return (
            <React.Fragment>
                {this._renderPaymentForm('bankName', 'Bank Name', 'Bank Name value')}
                {this._renderPaymentForm('bankAddress', 'Bank Address', 'Bank Address value')}
                {this._renderPaymentForm('bankCode', 'Bank ABA/Swift/Sort Code', 'Bank ABA/Swift/Sort Code value')}
                {this._renderPaymentForm('bankAccountName', 'Bank Account Name', 'Bank Account Name value')}
                {this._renderPaymentForm('bankAccountNumber', 'Bank Account Number', 'Bank Account Number value')}
            </React.Fragment>
        )
    }

    render() {
        const {selectedPaymentMethod} = this.state;

        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="selectedPaymentMethod" sm={3} xl={2}>Preferred Payment Method</Label>
                    <Col sm={5} xl={6}>
                        <Input type="select" id="selectedPaymentMethod" name="selectedPaymentMethod" onChange={this.handleChange} value={selectedPaymentMethod}>
                            {paymentMethodList.map(item => {
                                return (<option key={item.key} value={item.key}>{item.value}</option>)
                            })}
                        </Input>
                    </Col>
                </FormGroup>

                {this.formByMethod[selectedPaymentMethod]}
            </Form>
        )
    }
}