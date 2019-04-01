import React, { Component } from 'react';
import { post } from '../../../../modules/ajaxRequest';
import { Modal, ModalHeader, ModalBody, Alert, Button, Form, Row, Col, FormGroup, Label, Input } from 'reactstrap';

import { resource, globalResource, statusList } from '../../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';

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
        this.setState({ [event.target.name]: event.target.value })
    }

    handleChecked = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handleGetPaymentRequestInfo = async (transId) => {
        const url = 'Portal/PaymentRequest/GetPaymentRequestDetail';
        const response = await post(url, { transId });

        if (response && response.status === 200) {
            const data = response.data;

            data && this.setState({ paymentRequestInfo: data, status: data.status, adminDesc: data.AdminDesc, affDesc: data.AffDesc });
        }
    }

    handleSave = async () => {
        const { paymentRequestInfo } = this.state;

        if (this.state.status && paymentRequestInfo.Status.toLowerCase() == 'pending') {
            const { status, adminDesc, affDesc } = this.state;
            const paymentInfo = Object.assign({}, {
                TransId: paymentRequestInfo.TransId, AffId: paymentRequestInfo.AffId, Amount: paymentRequestInfo.Amount,
                TransDate: paymentRequestInfo.TransDateText, Status: status, AdminDesc: adminDesc, AffDesc: affDesc
            });
            const url = 'Portal/PaymentRequest/UpdatePaymentRequest';
            const response = await post(url, paymentInfo);

            if (response && response.status === 200) {
                const data = response.data;

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
                    message: `Transaction ID ${paymentRequestInfo.TransId} has been updated failly!`,
                }, () => window.setTimeout(this.hideMessage, 3000));
            }
        } else {
            this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: resource.Message,
            }, () => window.setTimeout(this.hideMessage, 3000));
        }
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { transactionId, handleClosePopup } = this.props;
        const { paymentRequestInfo, adminDesc, affDesc, isSuccess, isShowMessage, message } = this.state;
        const isPending = paymentRequestInfo && paymentRequestInfo.Status && paymentRequestInfo.Status.toLowerCase() == 'pending';

        return paymentRequestInfo && (
            <Modal isOpen={transactionId} toggle={handleClosePopup} size="lg" backdrop='static'>
                <ModalHeader toggle={handleClosePopup}><span className="iconpopup"></span>Trxn.ID {transactionId}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <Form tag="form" className="payment-request-update-form">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
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
                                                    <Input name="status" type="radio" className="mr-2" value="Approved" onChange={this.handleChecked} />
                                                    {statusList.Approved}
                                                </Label>
                                            </FormGroup>
                                            <FormGroup check>
                                                <Label check>
                                                    <Input name="status" type="radio" className="mr-2" value="Rejected" onChange={this.handleChecked} />
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
                            <Row>
                                <Col sm={6}>
                                    <fieldset>
                                        <legend>
                                            <h6 className="font-weight-bold">{resource.AdminNote}</h6>
                                        </legend>
                                        <FormGroup className="mb-2">
                                            <Label for="adminDesc">{resource.VisibleAdmin}</Label>
                                            <Input type="textarea" name="adminDesc" rows="6" disabled={!isPending} value={adminDesc} onChange={this.handleChange} />
                                        </FormGroup>
                                    </fieldset>
                                </Col>
                                <Col sm={6}>
                                    <fieldset>
                                        <legend>
                                            <h6 className="font-weight-bold">{resource.AffiliateNote}</h6>
                                        </legend>
                                        <FormGroup className="mb-2">
                                            <Label for="affDesc">{resource.VisibleAffiliate}</Label>
                                            <Input type="textarea" name="affDesc" rows="6" disabled={!isPending} value={affDesc} onChange={this.handleChange} />
                                        </FormGroup>
                                    </fieldset>
                                </Col>
                            </Row>
                            <Button type="button" color="success" className="mr-3" disabled={!isPending} onClick={this.handleSave}>{globalResource.Update}</Button>
                            <Button type="button" color="info" onClick={handleClosePopup}>{globalResource.Cancel}</Button>
                        </Form>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}