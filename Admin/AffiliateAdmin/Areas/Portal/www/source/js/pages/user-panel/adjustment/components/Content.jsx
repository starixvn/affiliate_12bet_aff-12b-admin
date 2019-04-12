import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert, Col, Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { toggleLoader, getBalanceBroughtForward, handleSubmit } from '../actions';
import { message, resource, regExp } from '../modules/constants';
import { formatDecimalWithCurrencySymbol, format } from '../../../../modules/numberFormat';
import { money } from '../../../../modules/constants';

const regexCheckAmount = new RegExp(regExp.checkAmount);
const regexValidAmount = new RegExp(regExp.validAmount);
const regexShouldFormatAmount = new RegExp(regExp.shouldFormatAmount);

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            balanceBf: 0,
            amount: '',
            description: '',
            isShowMessage: false,
            isSuccess: true,
            message: ''
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
        this.setState({ [event.target.name]: event.target.value });
    }

    handleChangeAmount = (event) => {
        const value = event.target.value;
        const amount = value.replace(/,/g, '');

        if (regexCheckAmount.test(amount)) {
            if (regexShouldFormatAmount.test(amount) && Number(amount) !== 0 && Number(amount) > Number.MIN_SAFE_INTEGER && Number(amount) < Number.MAX_SAFE_INTEGER) {
                this.setState({ [event.target.name]: format(Number(amount)) })
            } else {
                this.setState({ [event.target.name]: value });
            }
        }
    }

    _isValidAmount = (amount) => {
        const absBalanceBf = Math.abs(this.state.balanceBf);

        return regexValidAmount.test(amount) && Number(amount) - absBalanceBf > money.MinValue && Number(amount) + absBalanceBf < money.MaxValue;
    }

    _validateAmount = () => {
        const amount = this.state.amount.replace(/,/g, '');
        const result = {
            isValid: this._isValidAmount(amount) && Number(amount) != 0,
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
            const amountNumber = Number(amount.replace(/,/g, ''));
            const adjustmentInfo = Object.assign({}, { AffId: this.props.affInfo.AffId, Amount: amountNumber, Description: description });
            const data = await this.props.handleSubmit(adjustmentInfo);

            this.setState({
                isShowMessage: true,
                isSuccess: data.IsSuccessful,
                message: data.IsSuccessful ? message.Success : data.Message,
                amount: data.IsSuccessful ? '' : amount,
                description: data.IsSuccessful ? '' : description
            }, () => window.setTimeout(this.hideMessage, 3000));

            this.getBalanceBroughtForward();
        } else {
            this.setState({
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
        const { balanceBf, amount, description, isShowMessage, isSuccess, message } = this.state;
        const isValidAmount = this._isValidAmount(amount.replace(/,/g, '')) || amount === '';

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
                        <Label sm={8}>{formatDecimalWithCurrencySymbol(balanceBf)}</Label>
                    </FormGroup>
                    <FormGroup row className={isValidAmount ? "mb-2" : "mb-2 has-error"}>
                        <Label for="amount" sm={4} className="required">{resource.Amount}</Label>
                        <Col sm={8}>
                            <Input type="text" name="amount" id="amount" className="text-right h-75"
                                value={amount} onChange={this.handleChangeAmount} onKeyPress={this.handlePressEnter} />
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
                            <Button type="button" color="success" disabled={!isValidAmount} onClick={this.handleSubmit}>{resource.Submit}</Button>
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