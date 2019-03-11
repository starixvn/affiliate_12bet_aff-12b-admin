import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert, Col, Button, Form, FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { toggleLoader, getBalanceBroughtForward, handleSubmit } from '../actions';
import { resource, regExp } from '../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';

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

        const data = await this.props.getBalanceBroughtForward(this.props.affInfor);

        this.setState({ balanceBf: data });
        this.props.toggleLoader(false);
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _validateAmount = () => {
        const amount = this.state.amount.replace(/,/g, '');
        const regexCheckAmount = new RegExp(regExp.CheckAmount);
        const result = {
            isValid: regexCheckAmount.test(amount) && parseFloat(amount) != 0,
            message: ''
        }

        if (!result.isValid) {
            result.message = amount == '' ? resource.Required : resource.Invalid;
        }

        return result;
    }

    handleSubmit = async () => {
        this.props.toggleLoader(true);
        this.setState({
            isShowMessage: true
        });

        const amountValidation = this._validateAmount();

        if (amountValidation.isValid) {
            const { amount } = this.state
            const adjustmentInfo = Object.assign({}, { AffId: this.props.affInfor.AffId, Amount: amount.replace(/,/g, ''), Description: this.state.description });
            const data = await this.props.handleSubmit(adjustmentInfo);

            this.setState({
                isSuccess: data.IsSuccessful,
                message: data.IsSuccessful ? resource.Success : data.Message,
                amount: data.IsSuccessful ? '' : amount
            }, () => window.setTimeout(this.handleHideMessage, 3000));

            this.getBalanceBroughtForward();
        } else {
            this.setState({
                isSuccess: false,
                message: amountValidation.message,
            }, () => window.setTimeout(this.handleHideMessage, 3000));
        }

        this.props.toggleLoader(false);
    }

    handleHideMessage = () => {
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
        const { affInfor } = this.props;
        const { balanceBf, amount, description, isShowMessage, isSuccess, message } = this.state;

        return (
            <div className="adjustment-form-container">
                <div class="aff-name">{affInfor.AffName}</div>
                <div class="clear"></div>
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
                <Form tag="form" className="mb-3" autocomplete="off">
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.BalanceBf}</Label>
                        <Label sm={8}>{formatDecimalWithCurrencySymbol(balanceBf)}</Label>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="amount" className="required" sm={4}>{resource.Amount}</Label>
                        <Col sm={8}>
                            <Input type="text" name="amount" id="amount" className="text-right"
                                value={amount} onChange={this.handleChange} onKeyPress={this.handlePressEnter} />
                        </Col>
                    </FormGroup>
                    <FormGroup row className="mb-3">
                        <Label for="description" sm={4} className="d-flex align-items-center">{resource.Description}</Label>
                        <Col sm={8}>
                            <Input type="textarea" name="description" id="description" rows="6"
                                value={description} onChange={this.handleChange} />
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label sm={4}></Label>
                        <Col sm={8}>
                            <Button type="button" color="success" onClick={this.handleSubmit}>{resource.Submit}</Button>
                        </Col>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ affInfor }) {
    return Object.assign({}, { affInfor });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, getBalanceBroughtForward, handleSubmit }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);