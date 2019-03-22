import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert, Row, Col, Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { toggleLoader } from '../../actions';
import { resource, statusList, globalResource } from '../../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';

export class PaymentRequestDetail extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isApproved: null,
            adminNote: '',
            affiliateNote: '',
            isSuccess: true,
            isShowMessage: false,
            message: ''
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    handleChecked = (event) => {
        this.setState({ [event.target.name]: event.target.checked });
    }

    handleSave = async () => {
        this.props.toggleLoader(true);
        this.setState({
            isShowMessage: true
        });

        const amountValidation = this._validateAmount();

        if (paymentRequestInfo.Status.toLowerCase() == 'pending' && this.state.isApproved != null) {
            const { amount, description } = this.state
            const adjustmentInfo = Object.assign({}, { AffId: this.props.affInfo.AffId, Amount: amount.replace(/,/g, ''), Description: this.state.description });
            const data = await this.props.handleSubmit(adjustmentInfo);

            this.setState({
                isSuccess: data.IsSuccessful,
                message: data.IsSuccessful ? "Update is successful!" : data.Message,
            }, () => window.setTimeout(this.hideMessage, 3000));

            this.getBalanceBroughtForward();
        } else {
            this.setState({
                isSuccess: false,
                message: resource.Message,
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
        const { isApproved, adminNote, affiliateNote, isSuccess, isShowMessage, message } = this.state;
        const isPending = paymentRequestInfo.Status.toLowerCase() == 'pending';

        return (
            <Form tag="form" className="payment-request-detail-form">
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
                <fieldset>
                    <legend>
                        <h6 className="font-weight-bold">{resource.AccountInformation}</h6>
                    </legend>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.UserName}</Label>
                        <Label sm={6}>{paymentRequestInfo.AffName}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.RequestedDate}</Label>
                        <Label sm={6}>{paymentRequestInfo.TransDateText}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.EmailAddress}</Label>
                        <Label sm={6}>{paymentRequestInfo.Email}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.FirstName}</Label>
                        <Label sm={6}>{paymentRequestInfo.FirstName}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.LastName}</Label>
                        <Label sm={6}>{paymentRequestInfo.LastName}</Label>
                    </FormGroup>
                </fieldset>
                <fieldset>
                    <legend>
                        <h6 className="font-weight-bold">{resource.PaymentInformation}</h6>
                    </legend>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.PaymentMethod}</Label>
                        <Label sm={6}>{paymentRequestInfo.PaymentMethod}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.Amount}</Label>
                        <Label sm={6}>{formatDecimalWithCurrencySymbol(paymentRequestInfo.Amount)}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.Status}</Label>
                        <Label sm={6}>{paymentRequestInfo.Status}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.UserName12Bet}</Label>
                        <Label sm={6}>{paymentRequestInfo.PlayerUsername}</Label>
                    </FormGroup>
                    {
                        isPending ? (
                        <React.Fragment>
                            <FormGroup className="mr-2">
                                <FormGroup check>
                                    <Label check>
                                        <Input name="isApproved" className="ml-1 mr-2" type="checkbox" checked={isApproved} onChange={this.handleChecked} />
                                        {statusList.Approved}
                                    </Label>
                                </FormGroup>
                            </FormGroup>
                            <FormGroup>
                                <FormGroup check>
                                    <Label check>
                                        <Input name="isApproved" className="ml-1 mr-2" type="checkbox" checked={!isApproved} onChange={this.handleChecked} />
                                        {statusList.Rejected}
                                    </Label>
                                </FormGroup>
                            </FormGroup>
                        </React.Fragment>
                        ) : (
                        <React.Fragment>
                            <FormGroup row className="mb-2">
                                <Label sm={6}>{resource.CompletedDate}</Label>
                                <Label sm={6}>{paymentRequestInfo.CompletedDateText}</Label>
                            </FormGroup>
                            <FormGroup row className="mb-2">
                                <Label sm={6}>{resource.UpdatedBy}</Label>
                                <Label sm={6}>{paymentRequestInfo.Creator}</Label>
                            </FormGroup>
                        </React.Fragment>
                        )
                    }
                </fieldset>
                <Row>
                    <Col sm={6}>
                    <fieldset>
                        <legend>
                            <h6 className="font-weight-bold">{resource.AdminNote}</h6>
                        </legend>
                        <FormGroup className="mb-2">
                            <Label for="adminNote">{resource.VisibleOnly}</Label>
                            <Input type="textarea" name="adminNote" rows="8" value={this.state.adminNote} onChange={this.handleChange} />
                        </FormGroup>
                    </fieldset>
                    </Col>
                    <Col sm={6}>
                    <fieldset>
                        <legend>
                            <h6 className="font-weight-bold">{resource.AffiliateNote}</h6>
                        </legend>
                        <FormGroup className="mb-2">
                            <Label for="affiliateNote">{resource.VisibleAlso}</Label>
                            <Input type="textarea" name="affiliateNote" rows="8" value={this.state.affiliateNote} onChange={this.handleChange} />
                        </FormGroup>
                    </fieldset>
                    </Col>
                </Row>
                <Button type="button" color="success" className="mr-3" disabled={isPending} onClick={this.handleSave}>{globalResource.Update}</Button>
                <Button type="button" color="info" onClick={() => this.props.setShowPopup(false)}>{globalResource.Cancel}</Button>
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