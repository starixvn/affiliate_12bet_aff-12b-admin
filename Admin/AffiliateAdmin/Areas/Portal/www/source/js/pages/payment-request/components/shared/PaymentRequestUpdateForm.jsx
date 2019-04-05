import React, { Component } from 'react';
import { Modal, ModalHeader, ModalBody, Alert, Button, Form, Row, Col, FormGroup, Label, Input } from 'reactstrap';

import { resource, globalResource, statusList, errorList, pending } from '../../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';
import { getPaymentRequestDetail, updatePaymentRequest } from './service';

const pendingStatus = pending.toLowerCase();
const approvedStatus = statusList.Approved.toLowerCase();
const rejectedStatus = statusList.Rejected.toLowerCase();

export default class PaymentRequestUpdateForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            paymentRequestInfo: null,
            status: null,
            adminDesc: '',
            affDesc: '',
            isSuccess: true,
            isShowMessage: false,
            message: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { transactionId } = this.props;

        if (transactionId && transactionId !== prevProps.transactionId) {
            this.handleGetPaymentRequestInfo(transactionId);
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handleGetPaymentRequestInfo = async (transId) => {
        const data = await getPaymentRequestDetail({ transId });

        data && this.setState({ paymentRequestInfo: data, status: data.status, adminDesc: data.AdminDesc, affDesc: data.AffDesc });
    }

    handleSave = async () => {
        const { paymentRequestInfo, status } = this.state;

        if (status && paymentRequestInfo.Status.toLowerCase() === pendingStatus &&
            (status.toLowerCase() === approvedStatus || status.toLowerCase() === rejectedStatus)) {

            const { adminDesc, affDesc } = this.state;
            const paymentInfo = Object.assign({}, { TransId: paymentRequestInfo.TransId, Status: status, AdminDesc: adminDesc, AffDesc: affDesc });
            const data = await updatePaymentRequest(paymentInfo);

            if (data) {
                this.setState({
                    isShowMessage: true,
                    isSuccess: data.IsSuccessful,
                    message: data.IsSuccessful ? `Transaction ID ${paymentRequestInfo.TransId} has been updated successfully!` : data.Message,
                }, () => window.setTimeout(this.hideMessage, 3000));

                if (data.IsSuccessful) {
                    window.setTimeout(() => this.props.handleClosePopup(), 3000);
                    window.setTimeout(() => this.props.handleFetchData(), 3200);
                }
            }
            else {
                this.setState({
                    isSuccess: false,
                    isShowMessage: true,
                    message: errorList.UpdateFailed,
                }, () => window.setTimeout(this.hideMessage, 3000));
            }
        } else {
            this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: errorList.NotChoose,
            }, () => window.setTimeout(this.hideMessage, 3000));
        }
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    _renderAccountInformation(paymentRequestInfo) {
        return (
            <fieldset>
                <legend>
                    <h6 className="font-weight-bold">{resource.AccountInformation}</h6>
                </legend>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.UserName}</Label>
                    <Label sm={6}><strong>{paymentRequestInfo.AffName}</strong></Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.RequestedDate}</Label>
                    <Label sm={6}><strong>{paymentRequestInfo.TransDateText}</strong></Label>
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
        );
    }

    _renderPaymentInformation(paymentRequestInfo, isPending) {
        return (
            <fieldset>
                <legend>
                    <h6 className="font-weight-bold">{resource.PaymentInformation}</h6>
                </legend>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.PaymentMethod}</Label>
                    <Label sm={6}><strong>{paymentRequestInfo.PaymentMethod}</strong></Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.Amount}</Label>
                    <Label sm={6}><strong>{formatDecimalWithCurrencySymbol(paymentRequestInfo.Amount)}</strong></Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.Status}</Label>
                    <Label sm={6}><strong>{paymentRequestInfo.Status}</strong></Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.UserName12Bet}</Label>
                    <Label sm={6}><strong>{paymentRequestInfo.PlayerUsername}</strong></Label>
                </FormGroup>
                {
                    isPending ? (
                        <FormGroup row className="mb-2 ml-1">
                            <FormGroup className="mr-4" check>
                                <Label check>
                                    <Input name="status" type="radio" className="mr-2" value={statusList.Approved} onChange={this.handleChange} />
                                    {statusList.Approved}
                                </Label>
                            </FormGroup>
                            <FormGroup check>
                                <Label check>
                                    <Input name="status" type="radio" className="mr-2" value={statusList.Rejected} onChange={this.handleChange} />
                                    {statusList.Rejected}
                                </Label>
                            </FormGroup>
                        </FormGroup>
                    ) : (
                            <React.Fragment>
                                <FormGroup row className="mb-2">
                                    <Label sm={6}>{resource.CompletedDate}</Label>
                                    <Label sm={6}><strong>{paymentRequestInfo.CompletedDateText}</strong></Label>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={6}>{resource.UpdatedBy}</Label>
                                    <Label sm={6}>{paymentRequestInfo.Creator}</Label>
                                </FormGroup>
                            </React.Fragment>
                        )
                }
            </fieldset>
        );
    }

    _renderNote(isPending) {
        const { adminDesc, affDesc } = this.state;

        return (
            <Row>
                <Col sm={6} className="pr-1">
                    <fieldset>
                        <legend>
                            <h6 className="font-weight-bold">{resource.AdminNote}</h6>
                        </legend>
                        <FormGroup>
                            <Label for="adminDesc">{resource.VisibleAdmin}</Label>
                            <Input type="textarea" name="adminDesc" rows="6" disabled={!isPending} value={adminDesc} onChange={this.handleChange} />
                        </FormGroup>
                    </fieldset>
                </Col>
                <Col sm={6} className="pl-1">
                    <fieldset>
                        <legend>
                            <h6 className="font-weight-bold">{resource.AffiliateNote}</h6>
                        </legend>
                        <FormGroup>
                            <Label for="affDesc">{resource.VisibleAffiliate}</Label>
                            <Input type="textarea" name="affDesc" rows="6" disabled={!isPending} value={affDesc} onChange={this.handleChange} />
                        </FormGroup>
                    </fieldset>
                </Col>
            </Row>
        );
    }

    handleClosePopup = () => {
        this.props.handleClosePopup();
        this.setState({ status: null, adminDesc: '', affDesc: '' });
    }

    render() {
        const { transactionId } = this.props;
        const { paymentRequestInfo, isSuccess, isShowMessage, message } = this.state;
        const isPending = paymentRequestInfo && paymentRequestInfo.Status && paymentRequestInfo.Status.toLowerCase() === pendingStatus;

        return paymentRequestInfo && (
            <Modal isOpen={transactionId} toggle={this.handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this.handleClosePopup}><span className="iconpopup"></span>Trxn.ID {transactionId}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper p-0">
                        <Form tag="form" className="payment-request-update-form">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            {this._renderAccountInformation(paymentRequestInfo)}
                            {this._renderPaymentInformation(paymentRequestInfo, isPending)}
                            {this._renderNote(paymentRequestInfo, isPending)}
                            <Button type="button" color="success" className="mr-3" disabled={!isPending} onClick={this.handleSave}>{globalResource.Update}</Button>
                            <Button type="button" color="info" onClick={this.handleClosePopup}>{globalResource.Cancel}</Button>
                        </Form>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}