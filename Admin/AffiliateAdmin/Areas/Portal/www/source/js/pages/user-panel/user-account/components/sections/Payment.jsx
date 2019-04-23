import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

import { capitalizeFirstChar } from '../../modules/service';
import { setUserAccountInfo, setSelectedPaymentMethod } from '../../actions';

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
    }

    handleChangePaymentMethod = (event) => {
        this.props.setSelectedPaymentMethod(event.target.value);
    }

    handleChange = (event) => {
        const target = event.target;
        const name = capitalizeFirstChar(target.name);

        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { [name]: target.value })
        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    _renderFormByMethod(userAccountInfo) {
        return {
            [playerMethod]: this._renderPaymentForm('playerUsername', '12BET User Name', userAccountInfo.PlayerUsername, this.handleChange),
            [netellerMethod]: this._renderPaymentForm('netellerAccountID', 'Neteller Account ID', userAccountInfo.NetellerAccountID, this.handleChange),
            [moneybookersMethod]: this._renderPaymentForm('moneybookersEmail', 'Moneybookers Email', userAccountInfo.MoneybookersEmail, this.handleChange),
            [wireMethod]: this._renderWireForm(userAccountInfo)
        }
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

    _renderWireForm(userAccountInfo) {
        return (
            <React.Fragment>
                {this._renderPaymentForm('bankName', 'Bank Name', userAccountInfo.BankName, this.handleChange)}
                {this._renderPaymentForm('bankAddress', 'Bank Address', userAccountInfo.BankAddress, this.handleChange)}
                {this._renderPaymentForm('bankCode', 'Bank ABA/Swift/Sort Code', userAccountInfo.BankCode, this.handleChange)}
                {this._renderPaymentForm('bankAccountName', 'Bank Account Name', userAccountInfo.BankAccountName, this.handleChange)}
                {this._renderPaymentForm('bankAccountNumber', 'Bank Account Number', userAccountInfo.BankAccountNumber, this.handleChange)}
            </React.Fragment>
        )
    }

    render() {
        const { userAccountInfo, selectedPaymentMethod } = this.props;
        const renderFormByMethod = userAccountInfo && this._renderFormByMethod(userAccountInfo);

        return userAccountInfo && (
            <Form tag="div">
                <FormGroup row>
                    <Label for="selectedPaymentMethod" sm={3} xl={2}>Preferred Payment Method</Label>
                    <Col sm={5} xl={6}>
                        <Input type="select" id="selectedPaymentMethod" name="selectedPaymentMethod" onChange={this.handleChangePaymentMethod} value={selectedPaymentMethod}>
                            {paymentMethodList.map(item => {
                                return (<option key={item.key} value={item.key}>{item.value}</option>)
                            })}
                        </Input>
                    </Col>
                </FormGroup>

                {renderFormByMethod[selectedPaymentMethod]}
            </Form>
        )
    }
}

function mapStateToProps({ userAccountInfo, selectedPaymentMethod }) {
    return Object.assign({}, { userAccountInfo, selectedPaymentMethod });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setUserAccountInfo, setSelectedPaymentMethod }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Payment);