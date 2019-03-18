import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert, Col, Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { toggleLoader } from '../../actions';
import { resource } from '../../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';

export class PaymentRequestDetail extends Component {
    constructor(props) {
        super(props);

        this.state = {
            status: null,
            adminNote: '',
            affiliateNote: ''
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    handleSubmit = async () => {
        this.props.toggleLoader(true);
        this.setState({
            isShowMessage: true
        });

        const amountValidation = this._validateAmount();

        if (amountValidation.isValid) {
            const { amount, description } = this.state
            const adjustmentInfo = Object.assign({}, { AffId: this.props.affInfo.AffId, Amount: amount.replace(/,/g, ''), Description: this.state.description });
            const data = await this.props.handleSubmit(adjustmentInfo);

            this.setState({
                isSuccess: data.IsSuccessful,
                message: data.IsSuccessful ? resource.Success : data.Message,
                amount: data.IsSuccessful ? '' : amount,
                description: data.IsSuccessful ? '' : description
            }, () => window.setTimeout(this.hideMessage, 3000));

            this.getBalanceBroughtForward();
        } else {
            this.setState({
                isSuccess: false,
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

    render() {
        const { paymentRequestInfo } = this.props;
        const { status, adminNote, affiliateNote } = this.state;

        return (
            <Form tag="form" className="payment-request-detail-form">
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
                <fieldset>
                    <legend>
                        <h5 className="font-weight-bold">{resource.AccountInformation}</h5>
                    </legend>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.UserName}</Label>
                        <Label sm={8}>{paymentRequestInfo.AffName}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.RequestedDate}</Label>
                        <Label sm={8}>{paymentRequestInfo.TransDateText}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.EmailAddress}</Label>
                        <Label sm={8}>{paymentRequestInfo.Email}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.FirstName}</Label>
                        <Label sm={8}>{paymentRequestInfo.FirstName}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.LastName}</Label>
                        <Label sm={8}>{paymentRequestInfo.LastName}</Label>
                    </FormGroup>
                </fieldset>
                <fieldset>
                    <legend>
                        <h5 className="font-weight-bold">{resource.PaymentInformation}</h5>
                    </legend>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.PaymentMethod}</Label>
                        <Label sm={8}>{paymentRequestInfo.PaymentMethod}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.Amount}</Label>
                        <Label sm={8}>{formatDecimalWithCurrencySymbol(paymentRequestInfo.Amount)}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.Status}</Label>
                        <Label sm={8}>{paymentRequestInfo.Status}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.UserName12Bet}</Label>
                        <Label sm={8}>{paymentRequestInfo.PlayerUsername}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.CompletedDate}</Label>
                        <Label sm={8}>{paymentRequestInfo.CompletedDateText}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={4}>{resource.UpdateBy}</Label>
                        <Label sm={8}>{paymentRequestInfo.Creator}</Label>
                    </FormGroup>
                </fieldset>
            </Form>
        );
    }
}

function mapStateToProps({ paymentRequestInfo }) {
    return Object.assign({}, { paymentRequestInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PaymentRequestDetail);