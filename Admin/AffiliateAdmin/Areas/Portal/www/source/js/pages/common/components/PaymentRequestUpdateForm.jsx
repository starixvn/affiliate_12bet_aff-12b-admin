import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Modal, ModalHeader, ModalBody, Alert, Button, Form, Row, Col, FormGroup, Label, Input } from 'reactstrap';

import { resource, globalResource, statusList, errorList, pending } from '../modules/constants/paymentRequestUpdate';
import { formatDecimalWithCurrencySymbol } from '../../../modules/numberFormat';
import { getPaymentRequestDetail, updatePaymentRequest } from '../actions/paymentRequestUpdate';
import { htmlDecode } from '../../../modules/htmlHelper';

const pendingStatus = pending.toLowerCase();
const approvedStatus = statusList.Approved.toLowerCase();
const rejectedStatus = statusList.Rejected.toLowerCase();

export class PaymentRequestUpdateForm extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            paymentRequestInfo: null,
            originalStatus: '',
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
        this.setState({ paymentRequestInfo: Object.assign({}, this.state.paymentRequestInfo, { [event.target.name]: event.target.value }) });
    }

    handleGetPaymentRequestInfo = async (transId) => {
        const data = await this.props.getPaymentRequestDetail({ transId });

        data && this.setState({ paymentRequestInfo: data, originalStatus: htmlDecode(data.Status) });
    }

    handleSave = async () => {
        const { paymentRequestInfo, originalStatus } = this.state;
        const selectedStatus = paymentRequestInfo.Status;

        if (originalStatus && originalStatus.toLowerCase() === pendingStatus &&
            selectedStatus && (selectedStatus.toLowerCase() === approvedStatus || selectedStatus.toLowerCase() === rejectedStatus)) {

            const paymentInfo = Object.assign({}, {
                AffId: paymentRequestInfo.AffId,
                TransId: paymentRequestInfo.TransId,
                IsApproved: selectedStatus.toLowerCase() === approvedStatus,
                AdminDesc: paymentRequestInfo.AdminDesc,
                AffDesc: paymentRequestInfo.AffDesc
            });
            const result = await this.props.updatePaymentRequest(paymentInfo);

            if (result) {
                this.setState({
                    isShowMessage: true,
                    isSuccess: result.IsSuccessful,
                    message: result.IsSuccessful ? `Transaction ID ${paymentRequestInfo.TransId} has been updated successfully!` : result.Message,
                }, () => window.setTimeout(this.hideMessage, 3000));

                if (result.IsSuccessful) {
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
                    <Label sm={6}>{htmlDecode(paymentRequestInfo.Email)}</Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.FirstName}</Label>
                    <Label sm={6}>{htmlDecode(paymentRequestInfo.FirstName)}</Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.LastName}</Label>
                    <Label sm={6}>{htmlDecode(paymentRequestInfo.LastName)}</Label>
                </FormGroup>
            </fieldset>
        );
    }

    _renderPaymentCompletionInfo(paymentRequestInfo) {
        return (
            <React.Fragment>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.CompletedDate}</Label>
                    <Label sm={6}><strong>{paymentRequestInfo.CompletedDateText}</strong></Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.UpdatedBy}</Label>
                    <Label sm={6}>{htmlDecode(paymentRequestInfo.Creator)}</Label>
                </FormGroup>
            </React.Fragment>
        );
    }

    _renderOptionalStatus() {
        return (
            <FormGroup row className="mb-2 ml-1">
                <FormGroup className="mr-4" check>
                    <Label check>
                        <Input name="Status" type="radio" className="mr-2" value={statusList.Approved} onChange={this.handleChange} />
                        {statusList.Approved}
                    </Label>
                </FormGroup>
                <FormGroup check>
                    <Label check>
                        <Input name="Status" type="radio" className="mr-2" value={statusList.Rejected} onChange={this.handleChange} />
                        {statusList.Rejected}
                    </Label>
                </FormGroup>
            </FormGroup>
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
                    <Label sm={6}><strong>{htmlDecode(paymentRequestInfo.PaymentMethod)}</strong></Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.Amount}</Label>
                    <Label sm={6}><strong>{formatDecimalWithCurrencySymbol(paymentRequestInfo.Amount)}</strong></Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.Status}</Label>
                    <Label sm={6}><strong>{htmlDecode(paymentRequestInfo.Status)}</strong></Label>
                </FormGroup>
                <FormGroup row className="mb-2">
                    <Label sm={6}>{resource.UserName12Bet}</Label>
                    <Label sm={6}><strong>{htmlDecode(paymentRequestInfo.PlayerUsername)}</strong></Label>
                </FormGroup>
                {
                    isPending ? this._renderOptionalStatus() : this._renderPaymentCompletionInfo(paymentRequestInfo)
                }
            </fieldset>
        );
    }

    _renderNote(paymentRequestInfo, isPending) {
        const { AdminDesc, AffDesc } = paymentRequestInfo;

        return (
            <Row>
                <Col sm={6} className="pr-1">
                    <fieldset>
                        <legend>
                            <h6 className="font-weight-bold">{resource.AdminNote}</h6>
                        </legend>
                        <FormGroup>
                            <Label for="AdminDesc">{resource.VisibleAdmin}</Label>
                            <Input type="textarea" name="AdminDesc" rows="6" disabled={!isPending} value={htmlDecode(AdminDesc)} onChange={this.handleChange} />
                        </FormGroup>
                    </fieldset>
                </Col>
                <Col sm={6} className="pl-1">
                    <fieldset>
                        <legend>
                            <h6 className="font-weight-bold">{resource.AffiliateNote}</h6>
                        </legend>
                        <FormGroup>
                            <Label for="AffDesc">{resource.VisibleAffiliate}</Label>
                            <Input type="textarea" name="AffDesc" rows="6" disabled={!isPending} value={htmlDecode(AffDesc)} onChange={this.handleChange} />
                        </FormGroup>
                    </fieldset>
                </Col>
            </Row>
        );
    }

    handleClosePopup = () => {
        this.props.handleClosePopup();
        this.setState({ paymentRequestInfo: null });
    }

    render() {
        const { transactionId } = this.props;
        const { paymentRequestInfo, originalStatus, isSuccess, isShowMessage, message } = this.state;
        const isPending = originalStatus && originalStatus.toLowerCase() === pendingStatus;

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

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getPaymentRequestDetail, updatePaymentRequest }, dispatch);
}

export default connect(null, mapDispatchToProps)(PaymentRequestUpdateForm);