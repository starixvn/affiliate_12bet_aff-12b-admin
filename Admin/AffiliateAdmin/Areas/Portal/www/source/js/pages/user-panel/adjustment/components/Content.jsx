import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert, Col, Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { toggleLoader, getBalanceBroughtForward, handleSubmit } from '../actions';
import { message, resource, regExp, minOfMoney, maxOfMoney } from '../modules/constants';
import { formatDecimalWithDigitAndCurrencySymbol } from '../../../../modules/numberFormat';

const regexCheckAmount = new RegExp(regExp.CheckAmount);

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            balanceBf: 0,
            amount: '',
            description: '',
            isShowMessage: false,
            isSuccess: true,
            message: '',
            isError: false
        }
    }

    componentDidMount() {
        this.getBalanceBroughtForward();
    }

    async getBalanceBroughtForward() {
        this.props.toggleLoader(true);

        const data = await this.props.getBalanceBroughtForward(this.props.affInfo);

        this.setState({ balanceBf: data });
        this.props.toggleLoader(false);
    }

    handleChange = (event) => {
        const value = event.target.value;
        const amount = value.replace('$', '').replace(/,/g, '');

        if(amount === '-' || amount === '') {
            this.setState({ [event.target.name]: amount, isError: false });
            return;
        }

        if(amount === '-0') {
            this.setState({ [event.target.name]: '-$0', isError: false });
            return;
        }

        if(this._isAmountValid(amount)) {
            this.setState({ [event.target.name]: value.includes('.') ? value : formatDecimalWithDigitAndCurrencySymbol(parseFloat(amount)), isError: false })
        } else {
            this.setState({ isError: true });
        }
    }

    _isAmountValid = (amount) => {
        const absBalanceBf = Math.abs(this.state.balanceBf);

        return regexCheckAmount.test(amount) && parseFloat(amount) - absBalanceBf >= minOfMoney && parseFloat(amount) + absBalanceBf <= maxOfMoney;
    }

    _validateAmount = () => {
        const amount = this.state.amount.replace('$', '').replace(/,/g, '');
        const result = {
            isValid: this._isAmountValid(amount) && parseFloat(amount) != 0,
            message: ''
        }

        if (!result.isValid) {
            result.message = amount === '' ? message.Required : message.Invalid;
        }

        return result;
    }

    handleSubmit = async () => {
        this.props.toggleLoader(true);

        const amountValidation = this._validateAmount();

        if (amountValidation.isValid) {
            const { amount, description } = this.state;
            const adjustmentInfo = Object.assign({}, { AffId: this.props.affInfo.AffId, Amount: amount.replace('$', '').replace(/,/g, ''), Description: description });
            const data = await this.props.handleSubmit(adjustmentInfo);

            this.setState({
                isError: false,
                isShowMessage: true,
                isSuccess: data.IsSuccessful,
                message: data.IsSuccessful ? message.Success : data.Message,
                amount: data.IsSuccessful ? '' : amount,
                description: data.IsSuccessful ? '' : description
            }, () => window.setTimeout(this.hideMessage, 3000));

            this.getBalanceBroughtForward();
        } else {
            this.setState({
                isError: true,
                isSuccess: false,
                isShowMessage: true,
                message: amountValidation.message,
            }, () => window.setTimeout(this.hideMessage, 3000));
        }

        this.props.toggleLoader(false);
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this.handleSubmit();
        }
    }

    render() {
        const { affInfo } = this.props;
        const { balanceBf, amount, description, isShowMessage, isSuccess, message, isError } = this.state;

        return (
            <Form tag="form" className="adjustment-form">
                <fieldset>
                    <legend>
                        <h5 className="font-weight-bold">{affInfo.AffName}</h5>
                    </legend>
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.BalanceBf}</Label>
                        <Label sm={8}>{formatDecimalWithDigitAndCurrencySymbol(balanceBf)}</Label>
                    </FormGroup>
                    <FormGroup row className={isError ? "mb-2 has-error" : "mb-2"}>
                        <Label for="amount" sm={4} className="required">{resource.Amount}</Label>
                        <Col sm={8}>
                            <Input type="text" name="amount" id="amount" className="text-right h-75"
                            value={amount} onChange={this.handleChange} onKeyPress={this.handlePressEnter} />
                        </Col>
                    </FormGroup>
                    <FormGroup row className="mb-3">
                        <Label for="description" sm={4} className="align-self-center">{resource.Description}</Label>
                        <Col sm={8}>
                            <Input type="textarea" name="description" id="description" rows="6"
                                value={description} onChange={this.handleChange} />
                        </Col>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}></Label>
                        <Col sm={8}>
                            <Button type="button" color="success" onClick={this.handleSubmit}>{resource.Submit}</Button>
                        </Col>
                    </FormGroup>
                </fieldset>
            </Form>
        );
    }
}

function mapStateToProps({ affInfo }) {
    return Object.assign({}, { affInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, getBalanceBroughtForward, handleSubmit }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);